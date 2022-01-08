using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyHater.ViewModels.Transaction
{
   [QueryProperty(nameof(TransactionId), nameof(TransactionId))]
   class TransactionDetailVM : ViewModelBase
   {
      string transactionId;
      public string TransactionId { get => transactionId; set => SetProperty(ref transactionId, Uri.UnescapeDataString(value)); }

      WalletModel walletModel;
      public WalletModel WalletModel { get => walletModel; set => SetProperty(ref walletModel, value); }
      TransactionModel transactionModel;
      public TransactionModel TransactionModel { get => transactionModel; set => SetProperty(ref transactionModel, value); }
      public AsyncCommand CompleteCommand { get; }

      public TransactionDetailVM()
      {
         CompleteCommand = new AsyncCommand(Complete);
         Task.Run(async () =>
         {
            await Task.Delay(200);
            WalletModel = FirebaseService.walletService.currentWallet;
            TransactionModel = (WalletModel.Transactions ?? new List<TransactionModel>() { }).Find(x => x.Id == TransactionId);
         });
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }


   }
}
