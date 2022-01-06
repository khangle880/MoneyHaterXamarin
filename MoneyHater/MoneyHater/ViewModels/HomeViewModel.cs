using FFImageLoading.Svg.Forms;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.ViewModels.Transaction;
using MoneyHater.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class HomeViewModel : ViewModelBase
   {
      public AsyncCommand LogOutCommand { get; }
      public AsyncCommand AddTransactionCommand { get; }
      List<TransactionModel> transactions;
      WalletModel wallet;
      public List<TransactionModel> Transactions { get => transactions; set => SetProperty(ref transactions, value); }
      public WalletModel Wallet { get => wallet; set => SetProperty(ref wallet, value); }
      public ImageSource BellIcon { get; set; }
      public ImageSource AppIcon { get; set; }
      public TransactionModel transactionSelected;
      public TransactionModel TransactionSelected { get => transactionSelected; set => SetProperty(ref transactionSelected, value); }

      public ObservableRangeCollection<Grouping<DateTime, TransactionModel>> TransactionsGrouped { get; set; }

      public HomeViewModel()
      {
         //Transactions = new List<TransactionModel> { };
         //ReloadTransaction(Transactions);
         BellIcon = SvgImageSource.FromResource("MoneyHater.Resources.Icons.bell-solid.svg");
         AppIcon = SvgImageSource.FromResource("MoneyHater.Resources.Icons.wallet-solid.svg");
         Wallet = FirebaseService.walletService.currentWallet;
         if (Wallet == null)
         {
            Shell.Current.GoToAsync($"//{nameof(AddWalletPage)}");
         }
         else
         {
            ReloadTransaction(Wallet.Transactions);
         }
         LogOutCommand = new AsyncCommand(LogOut);
         AddTransactionCommand = new AsyncCommand(AddTransaction);
         MessagingCenter.Subscribe<object, TransactionModel>(this, "Add transaction", (obj, s) =>
         {
            Transactions.Insert(0, s);
            ReloadTransaction(Transactions);
         });
         MessagingCenter.Subscribe<object, WalletModel>(this, "Add wallet", (obj, s) =>
         {
            if (Wallet == null)
            {
               Wallet = FirebaseService.walletService.currentWallet;
               ReloadTransaction(Wallet.Transactions);
            }
         });

      }

      void ReloadTransaction(List<TransactionModel> value)
      {
         if (value == null) return;
         Transactions = value;
         TransactionsGrouped = new ObservableRangeCollection<Grouping<DateTime, TransactionModel>>();
         TransactionsGrouped.Clear();
         var sorted = from transaction in transactions
                      orderby transaction.ExecutedTime
                      group transaction by transaction.ExecutedDate into transactionGroup
                      select new Grouping<DateTime, TransactionModel>(transactionGroup.Key, transactionGroup);
         TransactionsGrouped = new ObservableRangeCollection<Grouping<DateTime, TransactionModel>>(sorted);
      }

      async Task LogOut()
      {
         IsBusy = true;
         try
         {
            if (FirebaseService.auth.SignOut())
            {
               FirebaseService.clear();
               Application.Current.MainPage = new SplashPage();
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

      async Task AddTransaction()
      {
         if (wallet != null)
         {
            await Shell.Current.GoToAsync($"{nameof(AddTransactionPage)}");
         }
      }

   }
}
