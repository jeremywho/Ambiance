using System;
using System.Windows.Input;
using Ambiance.Services;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IConfigController _configController;
        private bool _isStopped;
        private int _volume = 50;
        public string TrackName { get; }
        public string TrackIcon { get; }
        public Color TrackColor { get; }
        public ICommand AddTrackCommand { get; }

        public bool ShowAdd => !_configController.TrackIsAdded(this);
        public bool ShowRemove => !ShowAdd;

        private string _pathToAudioFile;

        public string PathToAudioFile
        {
            get => _pathToAudioFile;
            set => SetField(ref _pathToAudioFile, value);
        }

        public IAudioPlayer AudioPlayer => _audioPlayer;

        public AudioPlayerViewModel(AudioPlayerViewModel viewModel)
        {
            TrackName = viewModel.TrackName;
            TrackIcon = viewModel.TrackIcon;
            TrackColor = viewModel.TrackColor;

            AddTrackCallback = viewModel.AddTrackCallback;
            RemoveTrackCallback = viewModel.RemoveTrackCallback;
            TrackIsAdded = viewModel.TrackIsAdded;

            _audioPlayer = viewModel.AudioPlayer;
            _audioPlayer.OnFinishedPlaying = () =>
            {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;

            AddTrackCommand = new Command(AddTrackToCurrentList);

            // fire and forget
            //Task.Run(() =>
            //{
            // do this to fire up the jitter so we get a more responsive feel when hitting play for the first time
            _audioPlayer.Play();
            _audioPlayer.Pause();
        }

        public AudioPlayerViewModel(IAudioPlayer audioPlayer, IConfigController configController, string trackName, string trackIcon, Color trackColor)
        {
            TrackName = trackName;
            TrackIcon = trackIcon;
            TrackColor = trackColor;

            AddTrackCallback = configController.AddTrack;
            RemoveTrackCallback = configController.RemoveTrack;
            TrackIsAdded = configController.TrackIsAdded;

            _audioPlayer = audioPlayer;
            _configController = configController;
            _audioPlayer.OnFinishedPlaying = () =>
            {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;

            AddTrackCommand = new Command(AddTrackToCurrentList);

            // do this to fire up the jitter so we get a more responsive feel when hitting play for the first time
            _audioPlayer.Play();
            _audioPlayer.Pause();
        }

        public Func<AudioPlayerViewModel,bool> TrackIsAdded { get; }
        public Action<AudioPlayerViewModel> AddTrackCallback;
        public Action<AudioPlayerViewModel> RemoveTrackCallback;

        public void AddTrackToCurrentList()
        {
            if (TrackIsAdded.Invoke(this))
            {
                _audioPlayer.Pause();
                RemoveTrackCallback?.Invoke(this);
            }
            else
                AddTrackCallback?.Invoke(this);

            OnPropertyChanged(nameof(ShowAdd));
            OnPropertyChanged(nameof(ShowRemove));
        }

        public void Play()
        {
            _audioPlayer.Play();
        }

        public void Pause()
        {
            _audioPlayer.Pause();
        }

        public int Volume
        {
            get => _volume;
            set
            {
                SetField(ref _volume, value);
                _audioPlayer?.SetAudioVolume(value / 100.0f);
            }
        }

        private string _commandText;

        public string CommandText
        {
            get => _commandText;
            set => SetField(ref _commandText, value);
        }

        private ICommand _playPauseCommand;

        public ICommand PlayPauseCommand
        {
            get
            {
                return _playPauseCommand ?? (_playPauseCommand = new Command(
                           obj =>
                           {
                               if (CommandText == "Play")
                               {
                                   if (_isStopped)
                                   {
                                       _isStopped = false;
                                       _audioPlayer.Play();
                                   }
                                   else
                                   {
                                       _audioPlayer.Play();
                                   }
                                   CommandText = "Pause";
                               }
                               else
                               {
                                   _audioPlayer.Pause();
                                   CommandText = "Play";
                               }
                           }));
            }
        }
    }
}