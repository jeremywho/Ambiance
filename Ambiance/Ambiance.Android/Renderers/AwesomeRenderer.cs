using Ambiance.Droid.Renderers;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(AwesomeRenderer))]
namespace Ambiance.Droid.Renderers
{
    public class AwesomeRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var label = Control;

            var text = label.Text;
            if (text.Length > 1 || text[0] < 0xf000)
            {
                return;
            }

            var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets,
                "FontAwesome.otf");
            label.Typeface = font;
        }
    }
}