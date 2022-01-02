using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserInfoRepository))]
namespace MoneyHater.Droid.Services
{
   public class UserInfoRepository : BaseRepository<UserModel>
   {
      public override string DocumentPath =>
          "users";
   }
}
