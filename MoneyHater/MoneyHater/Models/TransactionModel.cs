﻿using MoneyHater.Helpers;
using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class TransactionModel : IIdentifiable
   {
      public string Id { get; set; }
      public double Amount { get; set; }
      public double AmountByWallet { get; set; }
      public string CategoryId { get; set; }
      public string CurrencyId { get; set; }
      public string EventId { get; set; }
      public bool ExcludedFromReport { get; set; }
      public DateTime ExecutedTime { get; set; }
      public string Note { get; set; }
      public DateTime Remind { get; set; }
      public string WithUserId { get; set; }

      public EventModel EventModel { get; set; }
      public CategoryModel CategoryModel => FirebaseService.categories.Find(x => x.Id == CategoryId);
      public CurrencyModel CurrencyModel => FirebaseService.currencies.Find(x => x.Id == CurrencyId);
      public AnotherUserModel With => FirebaseService.usersPublicInfo.Find(x => x.Id == WithUserId);

      public DateTime ExecutedDate
      {
         get
         {
            DateTime onlyDate = new DateTime(ExecutedTime.Year, ExecutedTime.Month, ExecutedTime.Day);
            return onlyDate;
         }
      }
   }
}
