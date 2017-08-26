using System.Collections.ObjectModel;
using Ambiance.Services;

namespace Ambiance.ViewModels
{
    public class AudioPlayerListViewModel : BaseViewModel
    {
        private readonly IAudioPlayerService _audioPlayer;
        public ObservableCollection<AudioPlayerViewModel> Items { get; } = new ObservableCollection<AudioPlayerViewModel>();

        private string _pageTitle = "";
        public string PageTitle
        {
            get => _pageTitle;
            set { _pageTitle = value; OnPropertyChanged(); } 
        }
        public AudioPlayerListViewModel(IAudioPlayerService audioPlayer)
        {
            _audioPlayer = audioPlayer;
            Items.Add(new AudioPlayerViewModel(_audioPlayer) { PathToAudioFile = "Galway.mp3" });
            Items.Add(new AudioPlayerViewModel(_audioPlayer) { PathToAudioFile = "rain.mp3" });
            Items.Add(new AudioPlayerViewModel(_audioPlayer) { PathToAudioFile = "cafe.mp3" });

            PageTitle = "AMBIANCE";
        }
    }
}