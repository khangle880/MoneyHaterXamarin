﻿using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.PickupPage
{
   class PickupIconVM : ViewModelBase
   {
      public List<IconSvg> icons;
      public List<IconSvg> Icons { get => icons; set => SetProperty(ref icons, value); }
      public IconSvg iconSelected;
      public IconSvg IconSelected { get => iconSelected; set => SetProperty(ref iconSelected, value); }
      public AsyncCommand CompleteCommand { get; }

      public PickupIconVM()
      {
         var list = FirebaseService.icons.Select(x => new IconSvg() { url = x });
         Icons = list.ToList();
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         MessagingCenter.Send<object, IconSvg>(this, "Pick icon", IconSelected);
         await Shell.Current.GoToAsync("..");
      }

   }
}
