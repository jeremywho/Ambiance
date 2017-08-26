using System;
using System.Collections.Generic;
using Android.Media;
using Ambiance.Droid.Services;
using Ambiance.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace Ambiance.Droid.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private Dictionary<string,MediaPlayer> _mediaPlayers = new Dictionary<string, MediaPlayer>();

        public Action OnFinishedPlaying { get; set; }

        public void Play(string pathToSoundName)
        {
            if (_mediaPlayers.ContainsKey(pathToSoundName))
            {
                _mediaPlayers[pathToSoundName].Completion -= MediaPlayer_Completion;
                _mediaPlayers[pathToSoundName].Stop();
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
                if (!_mediaPlayers.ContainsKey(pathToSoundName))
                {
                    _mediaPlayers[pathToSoundName] = new MediaPlayer();
                    _mediaPlayers[pathToSoundName].Prepared += (sender, args) =>
                    {
                        _mediaPlayers[pathToSoundName].Start();
                        _mediaPlayers[pathToSoundName].Completion += MediaPlayer_Completion;
                    };
                }

                _mediaPlayers[pathToSoundName].Reset();
                _mediaPlayers[pathToSoundName].SetVolume(0.5f, 0.5f);

                _mediaPlayers[pathToSoundName].SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                _mediaPlayers[pathToSoundName].PrepareAsync();
            }
        }

        void MediaPlayer_Completion(object sender, EventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Pause(string pathToAudioFile)
        {
            _mediaPlayers[pathToAudioFile]?.Pause();
        }

        public void SetAudioVolume(string pathToAudioFile, float level)
        {
            if (!_mediaPlayers.ContainsKey(pathToAudioFile)) return;
            _mediaPlayers[pathToAudioFile]?.SetVolume(level,level);
        }
    }
}
