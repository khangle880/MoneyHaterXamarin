using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyHater.ViewModels.Transaction
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AddTransactionPage : ContentPage
   {
      public AddTransactionPage()
      {
         InitializeComponent();
      }
   }
}