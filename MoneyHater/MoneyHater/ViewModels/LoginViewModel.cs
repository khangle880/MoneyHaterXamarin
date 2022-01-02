using Acr.UserDialogs;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class LoginViewModel : ViewModelBase
   {
      public AsyncCommand LoginCommand { get; }
      public AsyncCommand ToRegisterCommand { get; }

      string email, password, link;
      public string Link { get => link; set => SetProperty(ref link, value); }
      public string Email { get => email; set => SetProperty(ref email, value); }
      public string Password { get => password; set => SetProperty(ref password, value); }

      public LoginViewModel()
      {
         LoginCommand = new AsyncCommand(Login);
         ToRegisterCommand = new AsyncCommand(ToRegister);
      }
      async Task Login()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();
         //await FbApp.LoadDataLoggeduser();
         //link = FbApp.icons[0];

         //await FbApp.walletRepo.Save(new WalletModel
         //{
         //   Balance = 100,
         //   CurrencyId = "sHzNHRH1BGH9tOkvlQVU",
         //   EnableNotification = false,
         //   ExcludedFromTotal = true,
         //   Name = "New waleet",
         //   State = true,
         //}
         //    );
         //try
         //{
         //   var user = await FbApp.auth.LoginWithEAndP(email, password);
         //   if (user != null)
         //   {
         await FbApp.LoadDataLoggeduser();
         link = FbApp.icons[0];
         //         await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
         //      }
         //      else
         //      {
         //         await App.Current.MainPage.DisplayAlert("Alert", "User Not Found", "Ok");
         //      }
         //   }
         //   catch (Exception e)
         //   {
         //      await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
         //   }

         UserDialogs.Instance.HideLoading();
         IsBusy = false;
      }

      async Task ToRegister()
      {
         await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
      }

   }
}
