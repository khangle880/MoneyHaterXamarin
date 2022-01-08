using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(TransactionRepository))]
namespace MoneyHater.Droid.Services
{
   public class TransactionRepository : BaseRepository<TransactionModel>
   {
      public override string DocumentPath => Path ??
          previousPath
          + "/transactions";
   }
}
