using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(RecurringTransactionRepository))]
namespace MoneyHater.Droid.Services
{
   public class RecurringTransactionRepository : BaseRepository<RecurringTransactionModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/recurring_transactions";
   }
}
