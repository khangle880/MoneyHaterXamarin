using FFImageLoading.Svg.Forms;
using MoneyHater.Helpers;
using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.Models
{
   public class WalletModel : IIdentifiable
   {
      public string Id { get; set; }
      public double Balance { get; set; }
      public string CurrencyId { get; set; }
      public bool EnableNotification { get; set; }
      public bool ExcludedFromTotal { get; set; }
      public string Name { get; set; }
      public string Icon { get; set; }
      public bool State { get; set; }
      public List<AnotherUserModel> Members { get; set; }
      public List<BudgetModel> Budgets { get; set; }
      public List<CategoryModel> CustomCategories { get; set; }
      public List<DebtModel> Debts { get; set; }
      public List<EventModel> Events { get; set; }
      public List<ReadyExecutedTransactionModel> ReadyExecutedTransactions { get; set; }
      public List<RecurringTransactionModel> RecurringTransactions { get; set; }
      public List<TransactionModel> Transactions { get; set; }

      public ImageSource ImageSource => SvgImageSource.FromUri(new Uri(Icon ?? "https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-wallet-64.svg?alt=media&token=27167c8a-818a-4b46-a076-9aebfd46c9ea"));
      public CurrencyModel CurrencyModel => FirebaseService.currencies.Find(x => x.Id == CurrencyId);
      public string BalanceText => $"{Balance} {CurrencyModel.Iso}";
   }
}
