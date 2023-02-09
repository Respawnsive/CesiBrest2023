using Kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kaamelott.ViewModels
{
    public  class ListSamplesViewModel
    {
        public ListSamplesViewModel()
        {
            ListSaample = new ObservableCollection<Saample>();
            ListSaample.Add(new Saample
            {
                Title = "Interprète",
                Character = "Arthur - Le Roi Burgonde",
                Episode = "Livre II, 03 - Le Dialogue de Paix",
                File = "interprete.mp3",
                Imagefile = "arthurleroiburgonde.png"
            });
            ListSaample.Add(new Saample
            {
                Title = "JE NE MANGE PAS DE GRAINES",
                Character = "Le Maître d’armes",
                Episode = "Livre II, 26 - Corpore sano",
                File = "je_ne_mange_pas_de_graines.mp3",
                Imagefile = "lemaitredarmes.png"
            });

            ClickSaampleCommand = new Command(ExecuteSaample, CanExecuteSaample);
        }

        private ObservableCollection<Saample> listSaample;
        public ObservableCollection<Saample> ListSaample
        {
            get
            {
                return listSaample;
            }
            set
            {
                listSaample = value;
            }
        }

        private Saample selectedSaample;
        public Saample SelectedSaample
        {
            get { return selectedSaample; }
            set { selectedSaample = value; }
        }


        public ICommand ClickSaampleCommand { get; set; }

        private void ExecuteSaample()
        {
            //Logique

        }

        private bool CanExecuteSaample()
        {
            return SelectedSaample != null;
        }
    }
}
