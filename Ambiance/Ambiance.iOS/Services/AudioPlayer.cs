using System;
using Ambiance.iOS.Services;
using AVFoundation;
using Xamarin.Forms;
using Ambiance.Services;
using Foundation;

[assembly: Dependency(typeof(AudioPlayer))]
namespace Ambiance.iOS.Services
{
    public class AudioPlayer : IAudioPlayer
    {
        private readonly AVAudioPlayer _audioPlayer;
        public AudioPlayer(string audioFilePath)
        {
            _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(audioFilePath));
            _audioPlayer.FinishedPlaying += Player_FinishedPlaying;
            //_audioPlayer.Play();
        }

        public void Play()
        {
            _audioPlayer?.Play();
        }

        private void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            //TODO: restart player so it keeps looping?
            // is there a better way? can we just set it to loop to begin with?
            OnFinishedPlaying?.Invoke();
        }

        public void Pause()
        {
            _audioPlayer?.Pause();
        }

        public Action OnFinishedPlaying { get; set; }

        public void SetAudioVolume(float level)
        {
            if (_audioPlayer == null) return;
            _audioPlayer.Volume = level;
        }
    }
}