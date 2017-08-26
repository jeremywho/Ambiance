using System;
using System.Collections.Generic;
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
        Dictionary<string, AVAudioPlayer> _audioPlayers = new Dictionary<string, AVAudioPlayer>();
        //private AVAudioPlayer _audioPlayer;
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
            if (_audioPlayers.ContainsKey(pathToAudioFile))
            {
                _audioPlayers[pathToAudioFile].FinishedPlaying -= Player_FinishedPlaying;
                _audioPlayers[pathToAudioFile].Stop();
            }

            string localUrl = pathToAudioFile;
            _audioPlayers[pathToAudioFile] = AVAudioPlayer.FromUrl(NSUrl.FromFilename(localUrl));
            _audioPlayers[pathToAudioFile].FinishedPlaying += Player_FinishedPlaying;
            _audioPlayers[pathToAudioFile].Play();
        }

        private void Player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Pause(string pathToAudioFile)
        {
            _audioPlayers[pathToAudioFile]?.Pause();
        }

        //public void Play(string pathToAudioFile)
        //{
        //    _audioPlayers[pathToAudioFile]?.Play();
        //}

        public void SetAudioVolume(string pathToAudioFile, float level)
        {
            if (_audioPlayers.ContainsKey(pathToAudioFile)) return;
            _audioPlayers[pathToAudioFile].Volume = level;
        }
    }
}
