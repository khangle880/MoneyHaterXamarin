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
      }

      private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {

      }
   }
}