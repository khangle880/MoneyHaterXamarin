using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(IconRepository))]
namespace MoneyHater.Droid.Services
{
   public class IconRepository : BaseRepository<IconModel>
   {
      public override string DocumentPath =>
          "icons";
   }
}
