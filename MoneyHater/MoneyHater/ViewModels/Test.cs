using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   public class Test : ViewModelBase
   {
      public ImageSource url { get; set; }
      public ImageSource ImageSource { get; set; }
      public UriImageSource Image { get; set; } =
          new UriImageSource
          {
             Uri = new Uri("resource://MoneyHater.Resources.Icons.bellsolid.svg"),
             CachingEnabled = true,
             CacheValidity = TimeSpan.FromMinutes(1)
          };

      public Command RefreshCommand { get; }

      public Test()
      {
         ImageSource = SvgImageSource.FromResource("MoneyHater.Resources.Icons.bell-solid.svg");
         url = SvgImageSource.FromUri(new Uri("https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-food-and-wine-64.svg?alt=media&token=aab6922c-377c-40a4-b2a2-912788f89fd5"));
         RefreshCommand = new Command(() =>
         {
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
