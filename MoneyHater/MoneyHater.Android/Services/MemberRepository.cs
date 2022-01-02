using System;
using Firebase.Auth;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(MemberRepository))]
namespace MoneyHater.Droid.Services
{
   public class MemberRepository : BaseRepository<AnotherUserModel>
   {
      public override string DocumentPath =>
          previousPath
         + "/members";
   }
}
