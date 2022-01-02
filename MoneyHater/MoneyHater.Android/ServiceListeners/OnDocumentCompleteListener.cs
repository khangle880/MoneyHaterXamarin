using System;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Firestore;
using MoneyHater.Droid.Extensions;
using MoneyHater.Services;
using Task = Android.Gms.Tasks.Task;

namespace MoneyHater.Droid.ServiceListeners
{
    public class OnDocumentCompleteListener<T> : Java.Lang.Object, IOnCompleteListener
        where T : IIdentifiable
    {
        private TaskCompletionSource<T> _tcs;

        public OnDocumentCompleteListener(TaskCompletionSource<T> tcs)
        {
            _tcs = tcs;
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                var docObj = task.Result;
                if (docObj is DocumentSnapshot docRef)
                {
                    _tcs.TrySetResult(docRef.Convert<T>());
                    return;
                }
            }
            // something went wrong
            _tcs.TrySetResult(default);
        }
    }
}
