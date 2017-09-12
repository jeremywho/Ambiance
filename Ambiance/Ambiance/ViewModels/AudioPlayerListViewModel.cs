using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Ambiance.Services;
using Ambiance.Views;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerListViewModel : BaseViewModel, IConfigController
    {
        public bool ShowPlay { get; set; } = true;
        public bool ShowPause => !ShowPlay;

        public bool CanDelete = true;
        public ICommand DeleteCommand { get; }
        public ICommand AddTrackCommand { get; }
        public ICommand PlayPauseAllCommand { get; }
        public ICommand OpenPickerCommand { get; }
        public ICommand ClosePickerCommand { get; }

        private readonly IAudioPlayerService _audioPlayerService = DependencyService.Get<IAudioPlayerService>();
        public ObservableCollection<AudioPlayerViewModel> Items { get; } = new ObservableCollection<AudioPlayerViewModel>();
        public ObservableCollection<AudioPlayerViewModel> AvailableItems { get; } = new ObservableCollection<AudioPlayerViewModel>();

        private string _pageTitle = "";
        public string PageTitle
        {
            get => _pageTitle;
            set => SetField(ref _pageTitle, value);
        }

        private string _configTitle = "";
        public string ConfigTitle
        {
            get => _configTitle;
            set => SetField(ref _configTitle, value);
        }

        private void Delete(AudioPlayerViewModel player)
        {
            Items.Remove(player);
        }

        public void AddTrack(AudioPlayerViewModel player)
        {
            var newPlayer = new AudioPlayerViewModel(player);

            // are we currently playing?
            if(ShowPause)
                newPlayer.Play();

            Items.Add(newPlayer);

        }

        public void RemoveTrack(AudioPlayerViewModel player)
        {
            var track = Items.FirstOrDefault(i => i.TrackName == player.TrackName);
            Items.Remove(track);
        }

        public bool TrackIsAdded(AudioPlayerViewModel viewModel)
        {
            return Items.Any(i => i.TrackName == viewModel.TrackName);
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

        public void ShowPicker()
        {
            var navPage = Application.Current.MainPage as NavigationPage;
            var sp = new SoundPicker {BindingContext = this};
            navPage?.Navigation?.PushModalAsync(sp);
        }

        public void ClosePicker()
        {
            var nav = Application.Current.MainPage as NavigationPage;
            nav?.Navigation?.PopModalAsync();
        }

        public AudioPlayerListViewModel()
        {
            AvailableItems.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("Galway.mp3"), this, "Irish", "\uf001", Color.AliceBlue));
            AvailableItems.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("rain.mp3"), this, "Rain", "\uf0e9", Color.AliceBlue));
            AvailableItems.Add(new AudioPlayerViewModel(_audioPlayerService.GetAudioPlayer("cafe.mp3"), this, "Cafe", "\uf0f4", Color.AliceBlue));

            foreach (var audioPlayerViewModel in AvailableItems)
            {
                Items.Add(audioPlayerViewModel);
            }

            ConfigTitle = "Rainy Cafe";
            PageTitle = "AMBIANCE";

            DeleteCommand = new Command<AudioPlayerViewModel>(Delete);
            AddTrackCommand = new Command<AudioPlayerViewModel>(AddTrack);
            PlayPauseAllCommand = new Command(PlayPauseAll);
            OpenPickerCommand = new Command(ShowPicker);
            ClosePickerCommand = new Command(ClosePicker);
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