using AVFoundation;
using Foundation;
using Kaamelott.Interfaces;
using Kaamelott.iOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSAudioService))]
namespace Kaamelott.iOS.Services
{
    public class iOSAudioService : IAudioService
    {
        /// <summary>
        /// Joue le MP3 sur android
        /// </summary>
        /// <param name="filename">représente le nom du fichier (dans Assets/MP3)</param>
        public void PlayMP3(string filename)
        {
            NSUrl fileUrl = new NSUrl("Sounds/" + filename);
            AVAudioPlayer player = new AVAudioPlayer(fileUrl, "mp3", out NSError err);
            player.Volume = 1;
            player.FinishedPlaying += (s, e) =>
            {
                player = null;
            };
            player.NumberOfLoops = 0;
            player.Play();
        }
    }
}