using System;

namespace Ambiance.Services
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
        Action OnFinishedPlaying { get; set; }
        void SetAudioVolume(float level);
    }
}
