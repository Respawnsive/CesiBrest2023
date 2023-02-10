using Foundation;
using Kaamelott.Interfaces;
using Kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using System.IO;
using System.Text.Json;
using Xamarin.Forms;
using Kaamelott.iOS.Services;

[assembly: Dependency(typeof(iOSDataService))]
namespace Kaamelott.iOS.Services
{
    public class iOSDataService : IDataService
    {
        public List<Saample> GetSaamplesFromLocal()
        {
            var databruts = File.ReadAllText("Sounds/sounds.json");

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