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
   class TransactionViewModel : ViewModelBase
   {
      public AsyncCommand LogOutCommand { get; }

      public TransactionViewModel()
      {

         LogOutCommand = new AsyncCommand(LogOut);
      }

      async Task LogOut()
      {
         IsBusy = true;
         try
         {
            if (FbApp.auth.SignOut())
            {
               await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("Alert", "Something Happened", "Ok");
            }
         }
         catch (Exception e)
         {
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
         }

         IsBusy = false;
      }
   }
}
