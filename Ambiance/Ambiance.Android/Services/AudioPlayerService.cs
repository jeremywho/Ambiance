using System;
using System.Collections.Generic;
using Ambiance.Droid.Services;
using Ambiance.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace Ambiance.Droid.Services
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private readonly Dictionary<string, IAudioPlayer> _audioPlayers = new Dictionary<string, IAudioPlayer>();

        public IAudioPlayer GetAudioPlayer(string audioFilePath)
        {
            if (!_audioPlayers.ContainsKey(audioFilePath))
                _audioPlayers[audioFilePath] = new AudioPlayer(audioFilePath);

            return _audioPlayers[audioFilePath];
        }

        public void RemoveAudioPlayer(string audioFilePath)
        {
            throw new NotImplementedException();
        }

        public void PauseAll()
        {
            throw new NotImplementedException();
        }

        public void ResumeAll()
        {
            throw new NotImplementedException();
        }
    }
}
