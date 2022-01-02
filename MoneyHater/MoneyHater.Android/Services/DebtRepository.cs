using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(DebtRepository))]
namespace MoneyHater.Droid.Services
{
   public class DebtRepository : BaseRepository<DebtModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/debts";
   }
}
