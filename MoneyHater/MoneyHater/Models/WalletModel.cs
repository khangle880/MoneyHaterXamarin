using MoneyHater.Helpers;
using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
      public bool State { get; set; }
      public List<AnotherUserModel> Members { get; set; }
      public List<BudgetModel> Budgets { get; set; }
      public List<CategoryModel> CustomCategories { get; set; }
      public List<DebtModel> Debts { get; set; }
      public List<EventModel> Events { get; set; }
      public List<ReadyExecutedTransactionModel> ReadyExecutedTransactions { get; set; }
      public List<RecurringTransactionModel> RecurringTransactions { get; set; }
      public List<TransactionModel> Transactions { get; set; }

      public CurrencyModel CurrencyModel => FirebaseService.currencies.Find(x => x.Id == CurrencyId);
      public string BalanceText => $"{Balance} {CurrencyModel.Iso}";
   }
}
