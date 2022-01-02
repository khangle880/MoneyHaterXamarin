using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.Helpers
{
   public static class Utils
   {
      public static async Task ReplaceRoot(Page page)
      {
         var pages = Shell.Current.Navigation.NavigationStack;
         if (pages.Count >= 1)
         {
            Shell.Current.Navigation.InsertPageBefore(page, pages[0]);
            await Shell.Current.Navigation.PopToRootAsync();
         }
         else
         {
            await Shell.Current.Navigation.PushAsync(page, false);
         }
      }
   }
}
