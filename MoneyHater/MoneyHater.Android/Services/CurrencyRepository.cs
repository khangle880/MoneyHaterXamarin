using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CurrencyRepository))]
namespace MoneyHater.Droid.Services
{
   public class CurrencyRepository : BaseRepository<CurrencyModel>
   {
      public override string DocumentPath =>
          "currencies";
   }
}
