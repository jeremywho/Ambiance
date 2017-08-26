using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            PageTitle = "AMBIANCE";
            //OnPropertyChanged("Items");
        }
    }
}