using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class AnotherUserModel : IIdentifiable
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public bool PremiumStatus { get; set; }
   }
}
