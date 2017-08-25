﻿using System;
using Android.Media;
using Ambiance.Droid.Services;
using Ambiance.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace Ambiance.Droid.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private MediaPlayer _mediaPlayer;

        public Action OnFinishedPlaying { get; set; }

        public void Play(string pathToSoundName)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Completion -= MediaPlayer_Completion;
                _mediaPlayer.Stop();
            }

            var fullPath = pathToSoundName;

            Android.Content.Res.AssetFileDescriptor afd = null;

            try
            {
                afd = Forms.Context.Assets.OpenFd(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error openfd: " + ex);
            }
            if (afd != null)
            {
                System.Diagnostics.Debug.WriteLine("Length " + afd.Length);
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.Prepared += (sender, args) =>
                    {
                        _mediaPlayer.Start();
                        _mediaPlayer.Completion += MediaPlayer_Completion;
                    };
                }

                _mediaPlayer.Reset();
                _mediaPlayer.SetVolume(0.5f, 0.5f);

                _mediaPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                _mediaPlayer.PrepareAsync();
            }
        }

        void MediaPlayer_Completion(object sender, EventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Pause()
        {
            _mediaPlayer?.Pause();
        }

        public void Play()
        {
            _mediaPlayer?.Start();
        }

        public void SetAudioVolume(float level)
        {
            _mediaPlayer?.SetVolume(level,level);
        }
    }
}
