using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MoneyHater.Controls;
using MoneyHater.iOS.Renderers;

[assembly: ExportRenderer(typeof(StandardPicker), typeof(StandardPickerRenderer))]
namespace MoneyHater.iOS.Renderers
{
    // ReSharper disable once InconsistentNaming
    public class StandardPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var view = e.NewElement as Picker;
            this.Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}
