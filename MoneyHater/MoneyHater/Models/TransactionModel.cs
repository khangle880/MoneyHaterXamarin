using MoneyHater.Helpers;
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

      public string AmountText => $"{Amount} {CurrencyModel.Iso}";

      public EventModel EventModel => FirebaseService.walletService.currentWallet.Events.Find(x => x.Id == EventId);
      public CategoryModel CategoryModel
      {
         get
         {
            CategoryModel category = new CategoryModel();
            category = FindCategory();
            return category;
         }
      }
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

      CategoryModel FindCategory()
      {
         foreach (var item in FirebaseService.categories)
         {
            if (item.Id == CategoryId)
            {
               return item;
            }
            else
            {
               foreach (var child in item.Children)
               {
                  if (child.Id == CategoryId)
                  {
                     return child;
                  }
               }
            }
         }
         return null;

      }
   }
}
