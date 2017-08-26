using System;

namespace Ambiance.Services
{
    public interface IAudioPlayer
    {
        void Play();
        void Pause();
        Action OnFinishedPlaying { get; set; }
        void SetAudioVolume(float level);
    }
}