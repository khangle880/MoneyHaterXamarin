using Acr.UserDialogs;
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
      string email, password, name;
      public string Name { get => name; set => SetProperty(ref name, value); }
      public string Email { get => email; set => SetProperty(ref email, value); }
      public string Password { get => password; set => SetProperty(ref password, value); }

      public RegistrationViewModel()
      {
         RegisterCommand = new AsyncCommand(Register);
         ToLoginCommand = new AsyncCommand(ToLogin);
      }
      async Task Register()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();

         try
         {
            var user = await FirebaseService.auth.SignUpWithEAndP(email, password);
            if (user != null)
            {
               await FirebaseService.userRepo.Save(new Models.UserModel()
               {
                  Email = email,
                  Name = name,
                  PremiumStatus = false,
                  Password = password,
               }, FirebaseService.auth.Uid);
               await FirebaseService.LoadDataLoggeduser();
               Application.Current.MainPage = new SplashPage();
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

         UserDialogs.Instance.HideLoading();
         IsBusy = false;

      }

      async Task ToLogin()
      {
         Application.Current.MainPage = new LoginPage();
      }



   }
}
