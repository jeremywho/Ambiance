using Ambiance.Services;
using Ambiance.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambiance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPlayerPage : ContentPage
    {
        public AudioPlayerPage()
        {
            InitializeComponent();
            var audioService = DependencyService.Get<IAudioPlayerService>();
            BindingContext = new AudioPlayerListViewModel(audioService);
        }
    }
}