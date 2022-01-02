using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(EventRepository))]
namespace MoneyHater.Droid.Services
{
   public class EventRepository : BaseRepository<EventModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/events";
   }
}
