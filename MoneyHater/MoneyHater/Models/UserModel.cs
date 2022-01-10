using MoneyHater.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class UserModel : AnotherUserModel
   {
      public DateTime PremiumTo { get; set; }
      public string Password { get; set; }
   }
}
