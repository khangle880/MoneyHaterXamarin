using Firebase.Auth;
using Foundation;
using MoneyHater.Helpers;
using MoneyHater.iOS;
using MoneyHater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthIOS))]
namespace MoneyHater.iOS
{
   class AuthIOS : IAuth
   {
      public bool isSigned()
      {
         var user = Auth.DefaultInstance.CurrentUser;
         return user != null;
      }

      public async Task<string> LoginWithEAndP(string email, string password)
      {
         try
         {
            var newUser = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return await newUser.User.GetIdTokenAsync();
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public bool SignOut()
      {
         try
         {
            Auth.DefaultInstance.SignOut(out NSError error);
            return error == null;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      public async Task<string> SignUpWithEAndP(string email, string password)
      {
         try
         {
            var newUser = await Auth.DefaultInstance.CreateUserAsync(email, password);
            return await newUser.User.GetIdTokenAsync();
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public Task<UserModel> GetUserAsync()
      {
         var tcs = new TaskCompletionSource<UserModel>();

         Firebase.CloudFirestore.Firestore.SharedInstance
             .GetCollection("users")
             .GetDocument(Auth.DefaultInstance.CurrentUser.Uid)
             .GetDocument((snapshot, error) =>
             {
                if (error != null)
                {
                   // something went wrong
                   tcs.TrySetResult(default(UserModel));
                   return;
                }
                tcs.TrySetResult(new UserModel
                {
                   //Id = snapshot.Id,
                   //FirstName = snapshot.GetValue(new NSString("FirstName")).ToString(),
                   //LastName = snapshot.GetValue(new NSString("LastName")).ToString()
                });
             });

         return tcs.Task;
      }

   }
}