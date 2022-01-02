using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(BudgetRepository))]
namespace MoneyHater.Droid.Services
{
   public class BudgetRepository : BaseRepository<BudgetModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/budgets";
   }
}
