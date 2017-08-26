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
            set => SetField(ref _pathToAudioFile, value);
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