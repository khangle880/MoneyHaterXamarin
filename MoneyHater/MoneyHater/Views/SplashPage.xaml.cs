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

      private async void CheckWhetherTheUserIsSignedIn()
      {
         if (FbApp.auth.isSigned())
         {
            await FbApp.LoadDataLoggeduser();
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
         }
         else
         {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
         }
      }

   }
}