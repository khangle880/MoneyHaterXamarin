using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   public class Test : ViewModelBase
   {
      public string url { get; set; } = "https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-account-64.svg?alt=media&token=6c4b5580-6108-426c-90c0-ed85b1deea15";

      public UriImageSource Image { get; set; } =
          new UriImageSource
          {
             Uri = new Uri("https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-account-64.svg?alt=media&token=6c4b5580-6108-426c-90c0-ed85b1deea15"),
             CachingEnabled = true,
             CacheValidity = TimeSpan.FromMinutes(1)
          };

      public Command RefreshCommand { get; }

      public Test()
      {
         RefreshCommand = new Command(() =>
         {
            url = "https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-account-64.svg?alt=media&token=6c4b5580-6108-426c-90c0-ed85b1deea15";
            Image = new UriImageSource
            {
               Uri = new Uri("https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-account-64.svg?alt=media&token=6c4b5580-6108-426c-90c0-ed85b1deea15"),
               CachingEnabled = true,
               CacheValidity = TimeSpan.FromMinutes(1)
            };
            OnPropertyChanged(nameof(Image));
         });
      }


   }
}
