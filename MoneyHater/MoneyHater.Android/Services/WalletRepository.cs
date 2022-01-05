using System;
using Firebase.Auth;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(WalletRepository))]
namespace MoneyHater.Droid.Services
{
   public class WalletRepository : BaseRepository<WalletModel>
   {
      public override string DocumentPath =>
         "users/"
         + FirebaseAuth.Instance.CurrentUser.Uid
         //+ "6JgDy10KgXU71Y1zvBP3DDPMDbi2"
         + "/wallets";

   }
}
