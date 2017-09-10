using Xamarin.Forms;

namespace Ambiance.Views
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AudioPlayerPage();
            //MainPage = new MasterDetailPage
            //{
            //    MasterBehavior = MasterBehavior.Popover,
            //    Master = new SoundPicker {Title = "Ambiance"},
            //    Detail = new NavigationPage(new AudioPlayerPage())
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
