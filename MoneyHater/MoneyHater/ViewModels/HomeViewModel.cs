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
   class HomeViewModel : ViewModelBase
   {
      public AsyncCommand LogOutCommand { get; }
      List<TransactionModel> transactions;
      WalletModel wallet;
      public List<TransactionModel> Transactions { get => transactions; set => SetProperty(ref transactions, value); }
      public WalletModel Wallet { get => wallet; set => SetProperty(ref wallet, value); }


      public HomeViewModel()
      {
         wallet = FbApp.walletService.currentWallet;
         if (wallet == null)
         {
            Shell.Current.GoToAsync($"//{nameof(AddWalletPage)}");
         }
         else
         {
            transactions = wallet.Transactions;
         }
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
