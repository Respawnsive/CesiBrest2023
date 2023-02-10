using Kaamelott.Models;
using Kaamelott.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kaamelott
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ListSamplesPage();

            //MainPage = new DetailSaamplePage(new Saample
            //{
            //    Title = "Interprète",
            //    Character = "Arthur - Le Roi Burgonde",
            //    Episode = "Livre II, 03 - Le Dialogue de Paix",
            //    File = "interprete.mp3",
            //    Imagefile = "arthurleroiburgonde.png"
            //}); 
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
