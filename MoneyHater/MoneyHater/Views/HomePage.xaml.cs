using MoneyHater.Helpers;
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
   public partial class HomePage : ContentPage
   {
      public HomePage()
      {
         InitializeComponent();
         if (FirebaseService.walletService.currentWallet == null)
         {
            Shell.Current.GoToAsync($"{nameof(AddWalletPage)}");
         }
      }
      protected override void OnAppearing()
      {
         base.OnAppearing();
      }

   }
}