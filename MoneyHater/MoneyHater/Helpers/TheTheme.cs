using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.Helpers
{
   public static class TheTheme
   {
      public static void SetTheme()
      {
         switch (Settings.Theme)
         {
            //default
            case 0:
               Application.Current.UserAppTheme = OSAppTheme.Unspecified;
               break;
            //light
            case 1:
               Application.Current.UserAppTheme = OSAppTheme.Light;
               break;
            //dark
            case 2:
               Application.Current.UserAppTheme = OSAppTheme.Dark;
               break;
         }

         var nav = Application.Current.MainPage as NavigationPage;

         var e = DependencyService.Get<IEnvironment>();
         if (Application.Current.RequestedTheme == OSAppTheme.Dark)
         {
            e?.SetStatusBarColor(Color.Black, false);
            if (nav != null)
            {
               nav.BarBackgroundColor = Color.Black;
               nav.BarTextColor = Color.White;
            }
         }
         else
         {
            e?.SetStatusBarColor(Color.White, true);
            if (nav != null)
            {
               nav.BarBackgroundColor = Color.White;
               nav.BarTextColor = Color.Black;
            }
         }


      }
   }
}
