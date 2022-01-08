using FFImageLoading.Svg.Forms;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Account
{
   class IconVm : ViewModelBase
   {
      public List<List<IconSvg>> icons;
      public List<List<IconSvg>> Icons { get => icons; set => SetProperty(ref icons, value); }
      public List<IconSvg> icons1;
      public List<IconSvg> Icons1 { get => icons1; set => SetProperty(ref icons1, value); }

      public AsyncCommand CompleteCommand { get; }

      public IconVm()
      {
         var list = FirebaseService.icons.Select(x => new IconSvg() { url = x });
         Icons1 = list.ToList();
         Icons = Utils.Make2DArrayV(Icons1, 4);
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }
   }
}
