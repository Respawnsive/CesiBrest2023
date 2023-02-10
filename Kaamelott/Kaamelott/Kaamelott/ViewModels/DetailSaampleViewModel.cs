using Kaamelott.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaamelott.ViewModels
{
    public class DetailSaampleViewModel : ReactiveObject
    {
        public DetailSaampleViewModel(Saample param)
        {
            CurrentSample = param;
        }

        [Reactive]
        public Saample CurrentSample { get; set; }

        ////Logique
        //var audioService = DependencyService.Get<IAudioService>();
        //audioService.PlayMP3(SelectedSaample.File);

    }
}
