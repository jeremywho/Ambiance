using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambiance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPlayerView : ContentView
    {
        public AudioPlayerView()
        {
            InitializeComponent();
        }
    }
}