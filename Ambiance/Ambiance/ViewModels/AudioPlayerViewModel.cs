using System;
using System.Diagnostics;
using System.Windows.Input;
using Ambiance.Services;
using Xamarin.Forms;

namespace Ambiance.ViewModels
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        private readonly IAudioPlayerService _audioPlayer;
        private bool _isStopped;

        public AudioPlayerViewModel(IAudioPlayerService audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioPlayer.OnFinishedPlaying = () => {
                _isStopped = true;
                CommandText = "Play";
            };
            CommandText = "Play";
            _isStopped = true;
        }

        private int _volume;

        public int Volume
        {
            get => _volume;
            set
            {
                Debug.WriteLine($"volume: {value / (float)100.0}");
                _volume = value;
                OnPropertyChanged();
                _audioPlayer.SetAudioVolume(value / (float)100.0);
            }
        }


        private string _commandText;
        public string CommandText
        {
            get { return _commandText; }
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
                           (obj) =>
                           {
                               if (CommandText == "Play")
                               {
                                   if (_isStopped)
                                   {
                                       _isStopped = false;
                                       //_audioPlayer.Play("Galway.mp3");
                                       _audioPlayer.Play("Rain.mp3");
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
