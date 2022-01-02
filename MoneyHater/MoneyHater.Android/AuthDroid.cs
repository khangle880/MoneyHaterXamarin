using Android.Gms.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using MoneyHater.Droid;
using MoneyHater.Droid.ServiceListeners;
using MoneyHater.Helpers;
using MoneyHater.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace MoneyHater.Droid
{
   class AuthDroid : IAuth
   {
      public bool isSigned()
      {
         var user = FirebaseAuth.Instance.CurrentUser;
         return user != null;
      }

      public async Task<string> LoginWithEAndP(string email, string password)
      {
         try
         {
            var newUser = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = await newUser.User.GetIdToken(false).AsAsync<GetTokenResult>();
            return token.Token;
         }
         catch (FirebaseAuthInvalidUserException e)
         {
            throw e;
         }
         catch (FirebaseAuthInvalidCredentialsException e)
         {
            throw e;
         }
      }

      public bool SignOut()
      {
         try
         {
            Firebase.Auth.FirebaseAuth.Instance.SignOut();
            return true;
         }
         catch (Exception ex)
         {
            App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            return false;
         }
      }

      public async Task<string> SignUpWithEAndP(string email, string password)
      {
         try
         {
            var newUser = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            var token = await newUser.User.GetIdToken(false).AsAsync<GetTokenResult>(); ;
            return token.Token;
         }
         catch (FirebaseAuthInvalidUserException e)
         {
            throw e;
         }
         catch (FirebaseAuthInvalidCredentialsException e)
         {
            throw e;
         }
      }

      public Task<UserModel> GetUserAsync()
      {
         var tcs = new TaskCompletionSource<UserModel>();

         FirebaseFirestore.Instance
             .Collection("users")
             .Document(FirebaseAuth.Instance.CurrentUser.Uid)
             .Get()
             .AddOnCompleteListener(new OnCompleteListener(tcs));

         return tcs.Task;
      }

   }
}