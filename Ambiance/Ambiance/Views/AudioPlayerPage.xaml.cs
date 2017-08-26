using Ambiance.Services;
using Ambiance.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambiance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPlayerPage : ContentPage
    {
        public AudioPlayerPage()
        {
            InitializeComponent();
            BindingContext = new AudioPlayerListViewModel();
        }
    }
}