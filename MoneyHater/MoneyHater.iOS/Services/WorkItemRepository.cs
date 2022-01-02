using System;
using MoneyHater.Models;

namespace MoneyHater.iOS.Services
{
   public class WorkItemRepository : BaseRepository<WalletModel>
   {
      public override string DocumentPath =>
          "users/"
          + Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid
          + "/work";
   }
}
