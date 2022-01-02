using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class DebtModel : IIdentifiable
   {
      public string Id { get; set; }
      public List<TransactionModel> DebtList { get; set; }
      public List<TransactionModel> RefundList { get; set; }
      public string Type { get; set; }
      public string WithUserId { get; set; }
      public UserModel With { get; set; }
   }
}
