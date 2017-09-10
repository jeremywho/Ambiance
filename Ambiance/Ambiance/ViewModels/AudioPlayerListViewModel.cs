using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ambiance.Services;
using Ambiance.Views;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerListViewModel : BaseViewModel
    {
        public bool ShowPlay { get; set; } = true;
        public bool ShowPause => !ShowPlay;
        public bool CanDelete = true;
        public ICommand DeleteCommand { get; }
        public ICommand PlayPauseAllCommand { get; }
        public ICommand AddTrackCommand { get; }
        private readonly IAudioPlayerService _audioPlayerService = DependencyService.Get<IAudioPlayerService>();
        public ObservableCollection<AudioPlayerViewModel> Items { get; } = new ObservableCollection<AudioPlayerViewModel>();

        private string _pageTitle = "";
        public string PageTitle
        {
            get => _pageTitle;
            set => SetField(ref _pageTitle, value);
        }

        private void Delete(AudioPlayerViewModel player)
        {
            Items.Remove(player);
        }

        private void PlayPauseAll()
        {
            if (ShowPlay)
            {
                Items.ForEach(i => i.Play());
            }
            else
            {
                Items.ForEach(i => i.Pause());
            }

            ShowPlay = !ShowPlay;

            OnPropertyChanged(nameof(ShowPlay));
            OnPropertyChanged(nameof(ShowPause));
        }

        public void AddTrack()
        {
            var navPage = Application.Current.MainPage as NavigationPage;
            navPage?.Navigation?.PushModalAsync(new SoundPicker());
        }

        public AudioPlayerListViewModel()
        {
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("Galway.mp3"), "Irish", "irish.png"));
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("rain.mp3"), "Rain", "rain.png"));
            Items.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("cafe.mp3"), "Cafe", "cafe.png"));

            PageTitle = "AMBIANCE";

            DeleteCommand = new Command<AudioPlayerViewModel>(Delete);
            PlayPauseAllCommand = new Command(PlayPauseAll);
            AddTrackCommand = new Command(AddTrack);
        }
    }

    public static class Ext
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }
}