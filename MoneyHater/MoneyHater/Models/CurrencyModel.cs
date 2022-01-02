using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class CurrencyModel : IIdentifiable
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public string Country { get; set; }
      public string Icon { get; set; }
      public string Symbol { get; set; }
      public string Iso { get; set; }
      public int TimesUsed { get; set; }
   }
}
