using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class RecurringTransactionModel : IIdentifiable
   {
      public string Id { get; set; }
      public double Amount { get; set; }
      public double AmountByWallet { get; set; }
      public string CategoryId { get; set; }
      public CategoryModel CategoryModel { get; set; }
      public string CurrencyId { get; set; }
      public CurrencyModel CurrencyModel { get; set; }
      public string EventId { get; set; }
      public EventModel EventModel { get; set; }
      public bool ExcludedFromReport { get; set; }
      public DateTime From { get; set; }
      public string Note { get; set; }
      public DateTime Remind { get; set; }
      public bool State { get; set; }
      public DateTime To { get; set; }
      public string WithUserId { get; set; }
      public UserModel With { get; set; }
   }
}
