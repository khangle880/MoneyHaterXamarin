using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class EventModel : IIdentifiable
   {
      public string Id { get; set; }
      public double Consume { get; set; }
      public string CurrencyId { get; set; }
      public CurrencyModel CurrencyModel { get; set; }
      public DateTime From { get; set; }
      public string Icon { get; set; }
      public string Name { get; set; }
      public bool State { get; set; }
      public DateTime To { get; set; }
   }
}
