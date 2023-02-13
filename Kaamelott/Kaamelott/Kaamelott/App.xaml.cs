using Kaamelott.Models;
using Kaamelott.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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

            AppCenter.Start("android=9d649487-80a3-40b5-ab41-e738d4855295", typeof(Analytics), typeof(Crashes));

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
