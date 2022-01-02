using System;
using System.Collections.Generic;
using Android.Gms.Tasks;
using Firebase.Firestore;
using MoneyHater.Droid.Extensions;
using MoneyHater.Services;

namespace MoneyHater.Droid.ServiceListeners
{
    public class OnCollectionCompleteListener<T> : Java.Lang.Object, IOnCompleteListener
        where T : IIdentifiable
    {
        private System.Threading.Tasks.TaskCompletionSource<IList<T>> _tcs;

        public OnCollectionCompleteListener(System.Threading.Tasks.TaskCompletionSource<IList<T>> tcs)
        {
            _tcs = tcs;
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                var docsObj = task.Result;
                if (docsObj is QuerySnapshot docs)
                {
                    _tcs.TrySetResult(docs.Convert<T>());
                }
            }
        }
    }
}
