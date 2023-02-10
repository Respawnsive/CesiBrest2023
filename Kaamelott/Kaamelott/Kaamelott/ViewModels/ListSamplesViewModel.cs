using Kaamelott.Interfaces;
using Kaamelott.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Kaamelott.ViewModels
{
    public  class ListSamplesViewModel : ReactiveObject
    {
        private List<Saample> AllSamples;

        public ListSamplesViewModel()
        {
            ListSaample = new ObservableCollection<Saample>();

            ClickSaampleCommand = new Command(ExecuteSaample, CanExecuteSaample);
            LoadSamples();

            this.WhenAnyValue(x => x.SearchedText)
                .Subscribe(x => FilterSaamples(x, SelectedFilterCharacter));

            this.WhenAnyValue(x => x.SelectedFilterCharacter)
                .Subscribe(x => FilterSaamples(SearchedText, x));
        }

        #region Bindable Properties (Reactive)

        [Reactive]
        public ObservableCollection<Saample> ListSaample { get; set; }

        [Reactive]
        public Saample SelectedSaample { get; set; }

        [Reactive]
        public string SearchedText { get; set; }

        [Reactive]
        public List<string> ListCharacters { get; set; }

        [Reactive]
        public string SelectedFilterCharacter { get; set; }

        #endregion


        #region Commands & execution

        public ICommand ClickSaampleCommand { get; set; }

        private void ExecuteSaample()
        {
            //Logique
            var audioService = DependencyService.Get<IAudioService>();
            audioService.PlayMP3(SelectedSaample.File);
        }

        private bool CanExecuteSaample()
        {
            return SelectedSaample != null;
        }

        #endregion


        #region private methods

        /// <summary>
        /// Charge les données depuis le fichier local
        /// </summary>
        private void LoadSamples()
        {
            var dataService = DependencyService.Get<IDataService>();
            var listsamples = dataService.GetSaamplesFromLocal();
            AllSamples = listsamples;
            ListCharacters = AllSamples.Select(x => x.Character)
                                        .Distinct()
                                        .OrderBy(x => x)
                                        .ToList();
            //ListCharacters = 
            FilterSaamples("", "");
        }

        private void FilterSaamples(string searchedText, string searchedCharacter)
        {
            //logique de filtre
            var filteredsamples = AllSamples;

            if (!String.IsNullOrWhiteSpace(searchedText) && String.IsNullOrWhiteSpace(searchedCharacter))
            {
                //Filtrer avec Linq
                filteredsamples = AllSamples.Where(x => x.Title.ToLower().Contains(searchedText.ToLower())).ToList();
            }
            else if (String.IsNullOrWhiteSpace(searchedText) && !String.IsNullOrWhiteSpace(searchedCharacter))
            {
                filteredsamples = AllSamples.Where(x => x.Character == searchedCharacter).ToList();
            }
            else if (!String.IsNullOrWhiteSpace(searchedText) && !String.IsNullOrWhiteSpace(searchedCharacter))
            {
                filteredsamples = AllSamples.Where(x => x.Title.ToLower().Contains(searchedText.ToLower()) 
                                                    && x.Character == searchedCharacter).ToList();
            }

            ListSaample = new ObservableCollection<Saample>(filteredsamples);

        }

        #endregion
    }
}
