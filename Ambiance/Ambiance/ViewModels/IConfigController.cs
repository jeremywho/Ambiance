namespace Ambiance.ViewModels
{
    public interface IConfigController
    {
        void AddTrack(AudioPlayerViewModel player);
        void RemoveTrack(AudioPlayerViewModel player);
        bool TrackIsAdded(AudioPlayerViewModel viewModel);
    }
}