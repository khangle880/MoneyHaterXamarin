using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views.PickupPage;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class AddWalletViewModel : ViewModelBase
   {
      double balance;
      CurrencyModel currencyModel;
      bool enableNotification;
      bool excludedFromTotal;
      List<AnotherUserModel> members;
      string name;
      public string Name { get => name; set => SetProperty(ref name, value); }
      public double Balance { get => balance; set => SetProperty(ref balance, value); }
      public CurrencyModel CurrencyModel { get => currencyModel; set => SetProperty(ref currencyModel, value); }
      public bool EnableNotification { get => enableNotification; set => SetProperty(ref enableNotification, value); }
      public bool ExcludedFromTotal { get => excludedFromTotal; set => SetProperty(ref excludedFromTotal, value); }
      public List<AnotherUserModel> Members { get => members; set => SetProperty(ref members, value); }
      public string MembersName
      {
         get
         {
            var membersName = members.ConvertAll<string>((e) => e.Name);
            return string.Join(", ", membersName);
         }
      }
      public AsyncCommand RegisterCommand { get; }
      public AsyncCommand PickupMemberCommand { get; }

      public AddWalletViewModel()
      {
         RegisterCommand = new AsyncCommand(RegisterWallet);
         PickupMemberCommand = new AsyncCommand(PickupMember);
      }

      async Task RegisterWallet()
      {
         try
         {
            await FbApp.walletService.AddWallet(new WalletModel()
            {
               Balance = balance,
               CurrencyId = currencyModel.Id,
               CurrencyModel = currencyModel,
               EnableNotification = enableNotification,
               ExcludedFromTotal = excludedFromTotal,
               Members = members,
               Name = name,
               State = true,
            });
         }
         catch (Exception e)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Register New Wallet Failed", "Ok");
         }

      }

      async Task PickupMember()
      {
         await Shell.Current.GoToAsync($"{nameof(PickupUserPage)}");
      }


   }
}
