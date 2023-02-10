using Kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaamelott.Interfaces
{
    public interface IDataService
    {
        List<Saample> GetSaamplesFromLocal();

        //List<Saample> GetSaamplesFromAPI();
    }
}
