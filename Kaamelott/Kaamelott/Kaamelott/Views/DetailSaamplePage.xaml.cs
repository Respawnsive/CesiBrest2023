using Kaamelott.Models;
using Kaamelott.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kaamelott.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailSaamplePage : ContentPage
    {
        public DetailSaamplePage(Saample param)
        {
            InitializeComponent();
            BindingContext = new DetailSaampleViewModel(param, DisplayAlert);
            btn_mp3.Clicked += Btn_mp3_Clicked;
        }

        private void Btn_mp3_Clicked(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.WhenAll(
                    img_char.FadeTo(0.2, 1000, Easing.Linear),
                    img_char.RotateTo(360, 1000),
                    img_char.TranslateTo(100, 100, 1000));
                await Task.WhenAll(
                    img_char.FadeTo(1, 1000, Easing.Linear),
                    img_char.RotateTo(0, 1000),
                    img_char.TranslateTo(0, 0, 1000));
            });
        }
    }
}