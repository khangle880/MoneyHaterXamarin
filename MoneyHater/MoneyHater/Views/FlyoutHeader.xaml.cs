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
   public partial class FlyoutHeader : Grid
   {
      public FlyoutHeader()
      {
         InitializeComponent();
         image.Source = ImageSource.FromResource("MoneyHater.Resources.Images.premium3.png");
      }
   }
}