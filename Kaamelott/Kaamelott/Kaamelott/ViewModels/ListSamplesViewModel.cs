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
using Xamarin.Forms.PlatformConfiguration;
using Kaamelott.Views;

namespace Kaamelott.ViewModels
{
    public  class ListSamplesViewModel : ReactiveObject
    {
        private List<Saample> AllSamples;
        private INavigation NavigationService;

        public ListSamplesViewModel(INavigation navigationService)
        {
            NavigationService = navigationService;
            ListSaample = new ObservableCollection<Saample>();

            ClickSaampleCommand = new Command(ExecuteSaample, CanExecuteSaample);

            ClearFilterCommand = new Command(ClearFilters, CanClearFilters);

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
            //Naviguer vers la page 2
            NavigationService.PushAsync(new DetailSaamplePage(SelectedSaample));
            SelectedSaample = null;
        }

        private bool CanExecuteSaample()
        {
            return SelectedSaample != null;
        }


        public ICommand ClearFilterCommand { get; set; }

        private void ClearFilters()
        {
            SearchedText = null;
            SelectedFilterCharacter = null;
        }

        private bool CanClearFilters()
        {
            if (!String.IsNullOrWhiteSpace(SearchedText) || SelectedFilterCharacter != null)
            {
                return true;
            }
            return false;
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

            ((Command)ClearFilterCommand).ChangeCanExecute();

        }

        #endregion
    }
}
