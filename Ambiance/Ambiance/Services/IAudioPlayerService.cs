namespace Ambiance.Services
{
    public interface IAudioPlayerService
    {
        IAudioPlayer GetAudioPlayer(string audioFilePath);
        void RemoveAudioPlayer(string audioFilePath);
        void PauseAll();
        void ResumeAll();
    }
}
