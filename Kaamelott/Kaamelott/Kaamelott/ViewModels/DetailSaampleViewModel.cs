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

namespace Kaamelott.ViewModels
{
    public class DetailSaampleViewModel : ReactiveObject
    {
        public DetailSaampleViewModel(Saample param)
        {
            CurrentSample = param;

            PlayMP3Command = new Command(PlayMP3, CanPlayMP3);
            PlayTTSCommand = new Command(async() => await PlayTTS(), CanPlayTTS);
        }

        [Reactive]
        public Saample CurrentSample { get; set; }

        #region Commands & execution

        public ICommand PlayMP3Command { get; set; }

        private void PlayMP3()
        {
            var audioService = DependencyService.Get<IAudioService>();
            audioService.PlayMP3(CurrentSample.File);
        }

        private bool CanPlayMP3()
        {
            return !String.IsNullOrWhiteSpace(CurrentSample.File);
        }


        public ICommand PlayTTSCommand { get; set; }

        private async Task PlayTTS()
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
        }

        private bool CanPlayTTS()
        {
            return !String.IsNullOrWhiteSpace(CurrentSample.Title);
        }

        #endregion


    }
}
