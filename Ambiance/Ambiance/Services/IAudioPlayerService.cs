using System;

namespace Ambiance.Services
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Pause(string pathToAudioFile);
        Action OnFinishedPlaying { get; set; }
        void SetAudioVolume(string pathToAudioFile,float level);
    }
}
