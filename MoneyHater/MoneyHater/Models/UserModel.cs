using MoneyHater.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class UserModel : AnotherUserModel
   {
      public bool PremiumStatus { get; set; }
      public DateTime PremiumTo { get; set; }
   }
}
