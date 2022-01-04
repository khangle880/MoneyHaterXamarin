using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.PickupPage
{
   class PickupCurrencyVM : ViewModelBase
   {
      public List<CurrencyModel> currencies;
      public List<CurrencyModel> Currencies { get => currencies; set => SetProperty(ref currencies, value); }
      public CurrencyModel currencySelected;
      public CurrencyModel CurrencySelected { get => currencySelected; set => SetProperty(ref currencySelected, value); }

      public AsyncCommand CompleteCommand { get; }

      public PickupCurrencyVM()
      {
         currencies = FbApp.currencies;
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         MessagingCenter.Send<object, CurrencyModel>(this, "Load currency", currencySelected);
         await Shell.Current.GoToAsync("..");
      }
   }
}
