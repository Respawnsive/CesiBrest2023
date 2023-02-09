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
            BindingContext = new DetailSaampleViewModel(param);
        }
    }
}