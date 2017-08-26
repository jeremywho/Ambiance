using System;
using System.Collections.Generic;
using Ambiance.iOS.Services;
using AVFoundation;
using Xamarin.Forms;
using Ambiance.Services;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace Ambiance.iOS.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private readonly Dictionary<string, IAudioPlayer> _audioPlayers = new Dictionary<string, IAudioPlayer>();
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

        public IAudioPlayer GetAudioPlayer(string audioFilePath)
        {
            if (!_audioPlayers.ContainsKey(audioFilePath))
                _audioPlayers[audioFilePath] = new AudioPlayer(audioFilePath);

            return _audioPlayers[audioFilePath];
        }

        public void RemoveAudioPlayer(string audioFilePath)
        {
            // TODO: Implement when we want to add/remove audio players
        }

        public void PauseAll()
        {
        }

        public void ResumeAll()
        {

        }
    }
}
