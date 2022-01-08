using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Account
{
   [QueryProperty(nameof(WalletId), nameof(WalletId))]
   class WalletDetailVM : ViewModelBase
   {
      string walletId;
      public string WalletId { get => walletId; set => SetProperty(ref walletId, Uri.UnescapeDataString(value)); }
      WalletModel walletModel;
      public WalletModel WalletModel { get => walletModel; set => SetProperty(ref walletModel, value); }

      public AsyncCommand CompleteCommand { get; }

      public WalletDetailVM()
      {
         CompleteCommand = new AsyncCommand(Complete);
         Task.Run(async () =>
         {
            await Task.Delay(200);
            if (WalletId != null)
            {
               WalletModel = FirebaseService.walletService.wallets.Find(x => x.Id == WalletId);
               if (WalletModel == null)
               {
                  WalletModel = new WalletModel() { };
               }
            }
         });
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }

   }
}
