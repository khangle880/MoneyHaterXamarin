using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class IconModel : IIdentifiable
   {
      public string Id { get; set; }
      public List<string> Icons { get; set; }
   }
}
