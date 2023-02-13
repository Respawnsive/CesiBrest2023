using Kaamelott.Interfaces;
using Kaamelott.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using Acr.UserDialogs;

namespace Kaamelott.ViewModels
{
    public class DetailSaampleViewModel : ReactiveObject
    {
        public DetailSaampleViewModel(Saample param)
        {
            CurrentSample = param;

            PlayMP3Command = new Command(PlayMP3, CanPlayMP3);
            PlayTTSCommand = new Command(async() => await PlayTTS(), CanPlayTTS);
            ShareCommand = new Command(async () => await ShareSaample(), CanShareSaample);
        }

        [Reactive]
        public Saample CurrentSample { get; set; }

        #region Commands & execution

        public ICommand PlayMP3Command { get; set; }

        private void PlayMP3()
        {
            try
            {
                var audioService = DependencyService.Get<IAudioService>();
                audioService.PlayMP3(CurrentSample.File);

                Analytics.TrackEvent("MP3 joué : " + CurrentSample.File);
            }
            catch(Exception ex)
            {
                //Logguer ou gérer l'erreur (affichant un message ?)
                Crashes.TrackError(ex);

            }
        }

        private bool CanPlayMP3()
        {
            return !String.IsNullOrWhiteSpace(CurrentSample.File);
        }


        public ICommand PlayTTSCommand { get; set; }

        private async Task PlayTTS()
        {
            try
            {
                var listLocal = await TextToSpeech.GetLocalesAsync();
                var Targetlocal = listLocal.FirstOrDefault();
                var canadian = listLocal.Where(x => x.Country == "CA").FirstOrDefault();

                var options = new SpeechOptions();
                options.Pitch = 2;
                options.Volume = 1;
                if (canadian != null)
                    options.Locale = canadian;
                else
                    options.Locale = Targetlocal;

                await TextToSpeech.SpeakAsync(CurrentSample.Title);

                Analytics.TrackEvent("TTS joué : " + CurrentSample.Title);
            }
            catch(Exception ex)
            {
                //Logguer ou gérer l'erreur (affichant un message ?)
                Crashes.TrackError(ex);
                await UserDialogs.Instance.AlertAsync("Ce téléphone ne supporte pas le TextToSpeech. Installez GooglePlayServices !",
                    "Erreur TTS", "Ok");
            }
        }

        private bool CanPlayTTS()
        {
            return !String.IsNullOrWhiteSpace(CurrentSample.Title);
        }


        public ICommand ShareCommand { get; set; }

        private async Task ShareSaample()
        {
            ShareTextRequest request = new ShareTextRequest(CurrentSample.Title, "Partage d'un sample de Kaamlott");
            request.Uri = "http://google.fr";
            await Share.RequestAsync(request);
        }

        private bool CanShareSaample()
        {
            return !String.IsNullOrWhiteSpace(CurrentSample.Title);
        }

        #endregion


    }
}
