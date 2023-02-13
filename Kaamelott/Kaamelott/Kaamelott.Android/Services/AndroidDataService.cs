using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kaamelott.Interfaces;
using Kaamelott.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xamarin.Forms;
using Kaamelott.Droid.Services;
using System.Threading;

[assembly:Dependency(typeof(AndroidDataService))]
namespace Kaamelott.Droid.Services
{
    public class AndroidDataService : IDataService
    {
        public List<Saample> GetSaamplesFromLocal()
        {
            //On va chercher nativement le fichier (stream)
            var stream = Android.App.Application.Context.Assets.Open("sounds.json");
            //Lire le fichier
            StreamReader SR = new StreamReader(stream);
            string databruts = SR.ReadToEnd();
            SR.Close();

            //Fake long loading
            //Thread.Sleep(5000);

            //Caster le contenu string en List<Saample>
            var result = JsonSerializer.Deserialize<List<Saample>>(databruts);
            if (result != null)
            {
                return result;
            }
            else
                return new List<Saample>();
        }
    }
}