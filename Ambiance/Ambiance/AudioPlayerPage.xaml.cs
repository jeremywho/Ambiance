using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BindingContext = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());
        }
    }
}