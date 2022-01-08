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
      }
      protected override void OnAppearing()
      {
         Task.Run(async () =>
           {
              await Task.Delay(200);

              if (FirebaseService.walletService.currentWallet == null)
              {
                 await Shell.Current.GoToAsync($"{nameof(AddWalletPage)}");
              }
           });
         base.OnAppearing();
      }

   }
}