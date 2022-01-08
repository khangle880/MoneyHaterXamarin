using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views;
using MoneyHater.Views.Account;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Account
{
   class ManageWalletVM : ViewModelBase
   {
      public List<WalletModel> wallets;
      public List<WalletModel> Wallets { get => wallets; set => SetProperty(ref wallets, value); }
      public AsyncCommand CompleteCommand { get; }
      public AsyncCommand AddCommand { get; }
      public AsyncCommand<WalletModel> SelectedItemCommand { get; }
      public AsyncCommand<WalletModel> EditItemCommand { get; }
      public AsyncCommand<WalletModel> DeleteItemCommand { get; }

      public ManageWalletVM()
      {
         Wallets = FirebaseService.walletService.wallets;
         CompleteCommand = new AsyncCommand(Complete);
         AddCommand = new AsyncCommand(AddWallet);
         SelectedItemCommand = new AsyncCommand<WalletModel>(SelectedItem);
         EditItemCommand = new AsyncCommand<WalletModel>(EditItem);
         DeleteItemCommand = new AsyncCommand<WalletModel>(DeleteItem);
         MessagingCenter.Subscribe<object, WalletModel>(this, "Add wallet", (obj, s) =>
         {
            ReloadPage(FirebaseService.walletService.wallets);
         });
      }

      void ReloadPage(List<WalletModel> value)
      {
         Wallets = new List<WalletModel>();
         Wallets = value;
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }
      async Task AddWallet()
      {
         await Shell.Current.GoToAsync($"{nameof(AddWalletPage)}");
      }


      async Task SelectedItem(WalletModel wallet)
      {
         if (wallet == null) return;
         var route = $"{nameof(WalletDetailPage)}?WalletId={wallet.Id}";
         await Shell.Current.GoToAsync(route);
      }
      async Task EditItem(WalletModel wallet)
      {
         if (wallet == null) return;
         var route = $"{nameof(AddWalletPage)}?WalletId={wallet.Id}";
         await Shell.Current.GoToAsync(route);
      }
      async Task DeleteItem(WalletModel wallet)
      {
         if (wallet == null) return;
         var currentWallet = await FirebaseService.walletService.DeleteWallet(wallet);
         Wallets = new List<WalletModel>();
         Wallets = FirebaseService.walletService.wallets;
         MessagingCenter.Send<object, WalletModel>(this, "Update current wallet", currentWallet);
      }

   }
}
