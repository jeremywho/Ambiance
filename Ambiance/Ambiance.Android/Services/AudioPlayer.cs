using System;
using Ambiance.Services;
using Android.Media;
using Xamarin.Forms;

namespace Ambiance.Droid.Services
{
    public class AudioPlayer : IAudioPlayer
    {
        private readonly MediaPlayer _audioPlayer = new MediaPlayer();
        public Action OnFinishedPlaying { get; set; }

        public AudioPlayer(string audioFilePath)
        {
            var afd = Forms.Context.Assets.OpenFd(audioFilePath);
            if (afd == null) throw new NullReferenceException("Can't find audio file.");

            _audioPlayer.Prepared += (sender, args) =>
            {
                _audioPlayer.Completion += AudioPlayer_Completion;
            };

            _audioPlayer.Reset();
            _audioPlayer.SetVolume(0.5f, 0.5f);

            _audioPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
            _audioPlayer.PrepareAsync();
        }

        private void AudioPlayer_Completion(object sender, EventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Play()
        {
            _audioPlayer?.Start();
        }

        public void Pause()
        {
            _audioPlayer?.Pause();
        }

        public void SetAudioVolume(float level)
        {
            _audioPlayer?.SetVolume(level, level);
        }
    }
}