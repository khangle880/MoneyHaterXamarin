using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class ReportVM : ViewModelBase
   {
      List<TransactionModel> transactions;
      public List<TransactionModel> Transactions { get => transactions; set => SetProperty(ref transactions, value); }
      WalletModel wallet;
      public WalletModel Wallet { get => wallet; set => SetProperty(ref wallet, value); }

      List<ChartInfo> chartInfos;
      public List<ChartInfo> ChartInfos { get => chartInfos; set => SetProperty(ref chartInfos, value); }
      public ReportVM()
      {
         ChartInfos = new List<ChartInfo>();
         Transactions = new List<TransactionModel> { };
         Wallet = FirebaseService.walletService.currentWallet;
         if (Wallet != null)
         {
            Transactions = Wallet.Transactions;
         }
         ReloadPage(Transactions);

         MessagingCenter.Subscribe<object, TransactionModel>(this, "Add transaction", (obj, s) =>
         {
            ReloadPage(FirebaseService.walletService.currentWallet.Transactions);
         });
         MessagingCenter.Subscribe<object, WalletModel>(this, "Change wallet", (obj, s) =>
         {
            Wallet = s;
            ReloadPage(s.Transactions);
         });

      }

      void ReloadPage(List<TransactionModel> value)
      {
         if (Wallet == null) return;

         Transactions = value ?? new List<TransactionModel>();
         var date = DateTime.Now;
         var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
         var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
         Transactions = Transactions.Where(x => x.ExecutedTime <= lastDayOfMonth || x.ExecutedTime >= firstDayOfMonth).ToList();
         List<ChartInfo> list = new List<ChartInfo>() { };
         for (int i = 1; i < lastDayOfMonth.Day; i++)
         {
            var todayTransactions = Transactions.Where(x => x.ExecutedTime.Day == i);
            double income = 0;
            double expense = 0;
            foreach (var item in todayTransactions)
            {
               if (item.CategoryModel.Type == "Expense") expense += item.AmountByWallet;
               if (item.CategoryModel.Type == "Income") income += item.AmountByWallet;
            }
            list.Add(new ChartInfo()
            {
               Date = firstDayOfMonth.AddDays(i - 1).ToString("dd/MM"),
               Income = income,
               Expense = expense
            });
         }
         ChartInfos = list;
      }
   }

   class ChartInfo
   {
      public string Date { get; set; }
      public double Income { get; set; }
      public double Expense { get; set; }
   }
}
