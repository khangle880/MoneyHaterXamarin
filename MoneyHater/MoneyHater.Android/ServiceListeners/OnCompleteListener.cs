using System;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Auth;
using Firebase.Firestore;
using MoneyHater.Models;

namespace MoneyHater.Droid.ServiceListeners
{
   public class OnCompleteListener : Java.Lang.Object, IOnCompleteListener
   {
      private TaskCompletionSource<UserModel> _tcs;

      public OnCompleteListener(TaskCompletionSource<UserModel> tcs)
      {
         _tcs = tcs;
      }

      public void OnComplete(Android.Gms.Tasks.Task task)
      {
         if (task.IsSuccessful)
         {
            // process document
            var result = task.Result;
            if (result is DocumentSnapshot doc)
            {
               var user = new UserModel();
               user.Id = doc.Id;
               if (user.Id == FirebaseAuth.Instance.CurrentUser.Uid)
               {
                  user.Name = doc.GetString("Name");
                  user.Email = doc.GetString("Email");
                  user.PremiumStatus = doc.GetBoolean("PremiumStatus").BooleanValue();
                  user.PremiumTo = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(doc.GetString("PremiumTo"));
               }
               else
               {
                  user.Name = doc.GetString("Name");
                  user.Email = doc.GetString("Email");
               }
               _tcs.TrySetResult(user);
               return;
            }
         }
         // something went wrong
         _tcs.TrySetResult(default(UserModel));
      }
   }
}
