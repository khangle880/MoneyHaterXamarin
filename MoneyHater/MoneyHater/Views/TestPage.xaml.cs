using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyHater.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class TestPage : ContentPage
   {
      public TestPage()
      {
         InitializeComponent();
         abcSvg.Source = SvgImageSource.FromUri(new Uri("https://drive.google.com/file/d/1N87mK4CZvjmJc4HkKSiBTFhSpKRtUan4/view"));
      }

      private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {

      }
   }
}