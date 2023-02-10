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

namespace Kaamelott.ViewModels
{
    public  class ListSamplesViewModel : ReactiveObject
    {
        public ListSamplesViewModel()
        {
            ListSaample = new ObservableCollection<Saample>();

            ClickSaampleCommand = new Command(ExecuteSaample, CanExecuteSaample);
            LoadSamples();
        }

        [Reactive]
        public ObservableCollection<Saample> ListSaample { get; set; }

        [Reactive]
        public Saample SelectedSaample { get; set; }


        public ICommand ClickSaampleCommand { get; set; }

        private void ExecuteSaample()
        {
            //Logique

        }

        private bool CanExecuteSaample()
        {
            return SelectedSaample != null;
        }

        private void LoadSamples()
        {
            var dataService = DependencyService.Get<IDataService>();
            var listsamples = dataService.GetSaamplesFromLocal();
            ListSaample = new ObservableCollection<Saample>(listsamples);
        }
    }
}
