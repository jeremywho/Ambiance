using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ambiance.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoundPicker : ContentPage
    {
        public SoundPicker()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var nav = Application.Current.MainPage as NavigationPage;
            nav?.Navigation?.PopModalAsync();
        }
    }
}