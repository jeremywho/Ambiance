using System.Windows.Input;
using Ambiance.Services;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        private readonly IAudioPlayer _audioPlayer;
        private bool _isStopped;
        private int _volume = 50;
        public string TrackName { get; }
        public string TrackIcon { get; }

        private string _pathToAudioFile;

        public string PathToAudioFile
        {
            get => _pathToAudioFile;
            set => SetField(ref _pathToAudioFile, value);
        }

        public AudioPlayerViewModel(IAudioPlayer audioPlayer, string trackName, string trackIcon)
        {
            TrackName = trackName;
            TrackIcon = trackIcon;
            _audioPlayer = audioPlayer;
            _audioPlayer.OnFinishedPlaying = () =>
            {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;

            _audioPlayer.Play();
            _audioPlayer.Pause();
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