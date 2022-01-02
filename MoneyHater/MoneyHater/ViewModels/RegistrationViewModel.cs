using MoneyHater.Helpers;
using MoneyHater.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class RegistrationViewModel : ViewModelBase
   {
      public AsyncCommand RegisterCommand { get; }
      public AsyncCommand ToLoginCommand { get; }
      string email, password;
      public string Email { get => email; set => SetProperty(ref email, value); }
      public string Password { get => password; set => SetProperty(ref password, value); }

      public RegistrationViewModel()
      {
         RegisterCommand = new AsyncCommand(Register);
         ToLoginCommand = new AsyncCommand(ToLogin);
      }
      async Task Register()
      {
         try
         {
            var user = await FbApp.auth.SignUpWithEAndP(email, password);
            if (user != null)
            {
               await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("Alert", "Register Failed", "Ok");
            }
         }
         catch (Exception e)
         {
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
         }

      }

      async Task ToLogin()
      {
         await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
      }



   }
}
