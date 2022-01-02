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
   public partial class LoginPage : ContentPage
   {
      public LoginPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         CheckWhetherTheUserIsSignedIn();
         base.OnAppearing();
      }

      private async void CheckWhetherTheUserIsSignedIn()
      {
         if (FbApp.auth.isSigned())
         {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
         }
      }

      private async void Button_Clicked(object sender, EventArgs e)
      {
         await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
      }

      private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {
         await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");

      }
   }
}