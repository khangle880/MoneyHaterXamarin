﻿using FFImageLoading.Svg.Forms;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.ViewModels.Transaction;
using MoneyHater.Views;
using MoneyHater.Views.Transaction;
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
      public AsyncCommand<TransactionModel> SelectedItemCommand { get; }
      public AsyncCommand<TransactionModel> EditItemCommand { get; }
      public AsyncCommand<TransactionModel> DeleteItemCommand { get; }
      public AsyncCommand AddTransactionCommand { get; }
      List<TransactionModel> transactions;
      WalletModel wallet;
      string balanceText;
      public List<TransactionModel> Transactions { get => transactions; set => SetProperty(ref transactions, value); }
      public WalletModel Wallet { get => wallet; set => SetProperty(ref wallet, value); }
      public string BalanceText { get => balanceText; set => SetProperty(ref balanceText, value); }
      public ImageSource BellIcon { get; set; }
      public ImageSource AppIcon { get; set; }
      public TransactionModel transactionSelected;
      public TransactionModel TransactionSelected { get => transactionSelected; set => SetProperty(ref transactionSelected, value); }

      public ObservableRangeCollection<Grouping<DateTime, TransactionModel>> TransactionsGrouped { get; }

      public HomeViewModel()
      {
         //Transactions = new List<TransactionModel> { };
         //ReloadTransaction(Transactions);
         BellIcon = SvgImageSource.FromResource("MoneyHater.Resources.Icons.bell-solid.svg");
         AppIcon = SvgImageSource.FromResource("MoneyHater.Resources.Icons.wallet-solid.svg");
         Wallet = FirebaseService.walletService.currentWallet;
         TransactionsGrouped = new ObservableRangeCollection<Grouping<DateTime, TransactionModel>>();
         if (Wallet == null)
         {
            Shell.Current.GoToAsync($"//{nameof(AddWalletPage)}");
         }
         else
         {
            ReloadPage(Wallet.Transactions);
         }
         LogOutCommand = new AsyncCommand(LogOut);
         SelectedItemCommand = new AsyncCommand<TransactionModel>(SelectedItem);
         EditItemCommand = new AsyncCommand<TransactionModel>(EditItem);
         DeleteItemCommand = new AsyncCommand<TransactionModel>(DeleteItem);
         AddTransactionCommand = new AsyncCommand(AddTransaction);
         MessagingCenter.Subscribe<object, TransactionModel>(this, "Add transaction", (obj, s) =>
         {
            //Transactions.Insert(0, s);
            ReloadPage(Transactions);
         });
         MessagingCenter.Subscribe<object, WalletModel>(this, "Add wallet", (obj, s) =>
         {
            if (Wallet == null)
            {
               Wallet = FirebaseService.walletService.currentWallet;
               ReloadPage(Wallet.Transactions);
            }
         });

      }

      void ReloadPage(List<TransactionModel> value)
      {
         if (Wallet == null) return;
         if (value == null) return;
         Task.Run(async () =>
                 {
                    await Task.Delay(500);
                    BalanceText = Wallet.BalanceText;
                 });

         Transactions = value;
         TransactionsGrouped.Clear();
         var sorted = from transaction in transactions
                      orderby transaction.ExecutedTime
                      group transaction by transaction.ExecutedDate into transactionGroup
                      select new Grouping<DateTime, TransactionModel>(transactionGroup.Key, transactionGroup);
         var newTransaction = new ObservableRangeCollection<Grouping<DateTime, TransactionModel>>(sorted);
         foreach (var item in newTransaction)
         {
            TransactionsGrouped.Add(item);
         }
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

      async Task SelectedItem(TransactionModel transaction)
      {
         if (transaction == null) return;
         var route = $"{nameof(TransactionDetailPage)}?TransactionId={transaction.Id}";
         await Shell.Current.GoToAsync(route);
      }
      async Task EditItem(TransactionModel transaction)
      {
         // todo: test (check id of local currencies)
         if (transaction == null) return;
         var route = $"{nameof(AddTransactionPage)}?TransactionId={transaction.Id}";
         await Shell.Current.GoToAsync(route);
      }
      async Task DeleteItem(TransactionModel transaction)
      {

         if (transaction == null) return;
         await FirebaseService.walletService.DeleteTransaction(transaction);
         ReloadPage(Wallet.Transactions);
      }


   }
}
