using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kaamelott.Droid.Services;
using Kaamelott.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidAudioService))]
namespace Kaamelott.Droid.Services
{
    public class AndroidAudioService : IAudioService
    {
        public void PlayMP3(string filename)
        {
            //Comment jouer un MP3 sur android (natif android)
            var filedesc = Android.App.Application.Context.Assets.OpenFd("mp3/" + filename);
            var player = new MediaPlayer();
            player.Prepared += (sender, arg) =>
            {
                player.Start();
            };
            player.SetDataSource(filedesc);
            player.Prepare();
        }
    }
}