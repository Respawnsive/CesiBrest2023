using Kaamelott.Models;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kaamelott.Services
{
    public class ApiService
    {
        private string EndpointURL = "https://storagekaamelot.blob.core.windows.net/kaamlotcontainer/datas.json";

        public async Task<List<Saample>> GetSaamplesFromApiAsync()
        {
            List<Saample> datas = new List<Saample>();

            //Implémentation appel API
            try
            {
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(EndpointURL);
                //await Task.Delay(5000);

                datas = JsonSerializer.Deserialize<List<Saample>>(result);
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return datas;
        }
    }
}
