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

        private string _pathToAudioFile;

        public string PathToAudioFile
        {
            get => _pathToAudioFile;
            set
            {
                _pathToAudioFile = value;
                OnPropertyChanged();
            }
        }

        public AudioPlayerViewModel(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioPlayer.OnFinishedPlaying = () =>
            {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;
        }


        public int Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged();
                _audioPlayer?.SetAudioVolume(value / 100.0f);
            }
        }

        private string _commandText;

        public string CommandText
        {
            get => _commandText;
            set
            {
                _commandText = value;
                OnPropertyChanged();
            }
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
                                       //_audioPlayer.Play("Galway.mp3");
                                       //_audioPlayer.Play("Rain.mp3");
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