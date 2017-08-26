using System.Collections.ObjectModel;
using Ambiance.Services;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerListViewModel : BaseViewModel
    {
        private readonly IAudioPlayerService _audioPlayerService = DependencyService.Get<IAudioPlayerService>();
        public ObservableCollection<AudioPlayerViewModel> Items { get; } = new ObservableCollection<AudioPlayerViewModel>();

        private string _pageTitle = "";
        public string PageTitle
        {
            get => _pageTitle;
            set => SetField(ref _pageTitle, value);
        }

        public AudioPlayerListViewModel()
        {
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("Galway.mp3")));
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("rain.mp3")));
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("cafe.mp3")));

            PageTitle = "AMBIANCE";
        }
    }
}