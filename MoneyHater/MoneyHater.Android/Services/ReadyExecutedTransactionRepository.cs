using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReadyExecutedTransactionRepository))]
namespace MoneyHater.Droid.Services
{
   public class ReadyExecutedTransactionRepository : BaseRepository<ReadyExecutedTransactionModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/ready_executed_transactions";
   }
}
