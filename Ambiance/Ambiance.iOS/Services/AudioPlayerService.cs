using System;
using Ambiance.iOS.Services;
using AVFoundation;
using Foundation;
using Xamarin.Forms;
using Ambiance.Services;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace Ambiance.iOS.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private AVAudioPlayer _audioPlayer;
        public Action OnFinishedPlaying { get; set; }

        public AudioPlayerService()
        {
            var avSession = AVAudioSession.SharedInstance();
            avSession.SetCategory(AVAudioSessionCategory.Playback);

            avSession.SetActive(true, out var activationError);

            if (activationError != null)
                Console.WriteLine(
                    "Could not activate audio session {0}",
                    activationError.LocalizedDescription);
        }

        public void Play(string pathToAudioFile)
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.FinishedPlaying -= Player_FinishedPlaying;
                _audioPlayer.Stop();
            }
            
            _audioPlayer = AVAudioPlayer.FromUrl(NSUrl.FromFilename(pathToAudioFile));
            _audioPlayer.FinishedPlaying += Player_FinishedPlaying;
            _audioPlayer.Volume = 0.50f;
            _audioPlayer.Play();
        }

        private void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Pause()
        {
            _audioPlayer?.Pause();
        }

        public void Play()
        {
            _audioPlayer?.Play();
        }

        public void SetAudioVolume(float level)
        {
            if (_audioPlayer == null) return;
            _audioPlayer.Volume = level;
        }
    }
}
