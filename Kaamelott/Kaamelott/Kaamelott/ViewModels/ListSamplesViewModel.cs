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
using Kaamelott.Services;
using Microsoft.AppCenter.Crashes;

namespace Kaamelott.ViewModels
{
    public  class ListSamplesViewModel : ReactiveObject
    {
        private List<Saample> AllSamples = new List<Saample>();
        private INavigation NavigationService;

        public ListSamplesViewModel(INavigation navigationService)
        {
            try
            {
                NavigationService = navigationService;
                ListSaample = new ObservableCollection<Saample>();

                RefreshCommand = new Command(LoadSamples);

                RefreshCommandAsync = new Command(async() => await LoadSamplesAsync());

                ClickSaampleCommand = new Command(ExecuteSaample, CanExecuteSaample);

                ClearFilterCommand = new Command(ClearFilters, CanClearFilters);

                //LoadSamples();

                this.WhenAnyValue(x => x.SearchedText)
                    .Subscribe(x => FilterSaamples(x, SelectedFilterCharacter, SelectedFilterEpisode));

                this.WhenAnyValue(x => x.SelectedFilterCharacter)
                    .Subscribe(x => FilterSaamples(SearchedText, x, SelectedFilterEpisode));

                this.WhenAnyValue(x => x.SelectedFilterEpisode)
                    .Subscribe(x => FilterSaamples(SearchedText, SelectedFilterCharacter, x));
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
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

        [Reactive]
        public List<string> ListEpisodes { get; set; }

        [Reactive]
        public string SelectedFilterEpisode { get; set; }

        [Reactive]
        public bool IsBusy { get; set; }

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
            SelectedFilterEpisode = null;
        }

        private bool CanClearFilters()
        {
            if (!String.IsNullOrWhiteSpace(SearchedText) 
                || SelectedFilterCharacter != null
                || SelectedFilterEpisode != null)
            {
                return true;
            }
            return false;
        }


        public ICommand RefreshCommand { get; set; }

        public ICommand RefreshCommandAsync { get; set; }


        #endregion


        #region private methods

        /// <summary>
        /// Charge les données depuis le fichier local
        /// </summary>
        private void LoadSamples()
        {
            IsBusy = true;

            //Données locales
            var dataService = DependencyService.Get<IDataService>();
            var listsamples = dataService.GetSaamplesFromLocal();
            AllSamples = listsamples;
            ListCharacters = AllSamples.Select(x => x.Character)
                                        .Distinct()
                                        .OrderBy(x => x)
                                        .ToList();
            ListEpisodes = AllSamples.Select(x => x.Episode)
                            .Distinct()
                            .OrderBy(x => x)
                            .ToList();
            //ListCharacters = 
            FilterSaamples("", "", "");

            IsBusy = false;
        }

        private async Task LoadSamplesAsync()
        {
            //Données API
            
            IsBusy = true;

            ApiService apiClient = new ApiService();
            var listsamples = await apiClient.GetSaamplesFromApiAsync();

            //
            AllSamples = listsamples;
            ListCharacters = AllSamples.Select(x => x.Character)
                                        .Distinct()
                                        .OrderBy(x => x)
                                        .ToList();
            ListEpisodes = AllSamples.Select(x => x.Episode)
                            .Distinct()
                            .OrderBy(x => x)
                            .ToList();
            //ListCharacters = 
            FilterSaamples("", "", "");

            IsBusy = false;
        }


        private void FilterSaamples(string searchedText, string searchedCharacter, string searchedEpisode)
        {
            try
            {
                //logique de filtre
                var filteredsamples = AllSamples;

                //Filtrer avec Linq
                if (!String.IsNullOrWhiteSpace(searchedText))
                {
                    filteredsamples = filteredsamples.Where(x => x.Title.ToLower().Contains(searchedText.ToLower())).ToList();
                }

                if (!String.IsNullOrWhiteSpace(searchedCharacter))
                {
                    filteredsamples = filteredsamples.Where(x => x.Character == searchedCharacter).ToList();
                }

                if (!String.IsNullOrWhiteSpace(searchedEpisode))
                {
                    filteredsamples = filteredsamples.Where(x => x.Episode == searchedEpisode).ToList();
                }


                ListSaample = new ObservableCollection<Saample>(filteredsamples);

                ((Command)ClearFilterCommand)?.ChangeCanExecute();
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion
    }
}
