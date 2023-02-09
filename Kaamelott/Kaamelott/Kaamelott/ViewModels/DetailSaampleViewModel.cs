using Kaamelott.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaamelott.ViewModels
{
    public class DetailSaampleViewModel
    {
        public DetailSaampleViewModel(Saample param)
        {
            CurrentSample = param;
        }

        private Saample currentSample;

        public Saample CurrentSample
        {
            get { return currentSample; }
            set { currentSample = value; }
        }

    }
}
