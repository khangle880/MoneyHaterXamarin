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
   public partial class SplashPage : ContentPage
   {
      bool isFirstAppeared = false;
      public SplashPage()
      {
         InitializeComponent();
         CheckWhetherTheUserIsSignedIn();
      }

      protected override void OnAppearing()
      {
         if (!isFirstAppeared)
         {
            CheckWhetherTheUserIsSignedIn();
         }
         else
         {
            Shell.Current.GoToAsync("..");
         }
         isFirstAppeared = true;
         base.OnAppearing();
      }

      private async void CheckWhetherTheUserIsSignedIn()
      {
         await Task.Delay(2000);
         if (FbApp.auth.isSigned())
         {
            await Shell.Current.GoToAsync($"{nameof(HomePage)}");
         }
         else
         {
            await Shell.Current.GoToAsync($"{nameof(HomePage)}");
         }
      }

   }
}