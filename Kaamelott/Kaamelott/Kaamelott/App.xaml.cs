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

            MainPage = new NavigationPage(new ListSamplesPage());
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
