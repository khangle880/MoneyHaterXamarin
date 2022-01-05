﻿using Acr.UserDialogs;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views;
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
      public List<CurrencyModel> currencies;
      public List<CurrencyModel> Currencies { get => currencies; set => SetProperty(ref currencies, value); }
      public string Name { get => name; set => SetProperty(ref name, value); }
      public double Balance { get => balance; set => SetProperty(ref balance, value); }
      public CurrencyModel CurrencyModel { get => currencyModel; set => SetProperty(ref currencyModel, value); }
      public bool EnableNotification { get => enableNotification; set => SetProperty(ref enableNotification, value); }
      public bool ExcludedFromTotal { get => excludedFromTotal; set => SetProperty(ref excludedFromTotal, value); }
      public List<AnotherUserModel> Members { get => members; set => SetProperty(ref members, value); }

      public string membersName;
      public string MembersName { get => membersName; set => SetProperty(ref membersName, value); }
      public AsyncCommand RegisterCommand { get; }
      public AsyncCommand PickupMemberCommand { get; }
      public AsyncCommand PickupCategoryCommand { get; }

      public AddWalletViewModel()
      {
         RegisterCommand = new AsyncCommand(RegisterWallet);
         PickupMemberCommand = new AsyncCommand(PickupMember);
         PickupCategoryCommand = new AsyncCommand(PickupCategory);
         currencies = FbApp.currencies;

         MessagingCenter.Subscribe<object, List<AnotherUserModel>>(this, "Load members", (obj, s) =>
         {
            members = s;
            var term = members.ConvertAll<string>((e) => e.Name);
            MembersName = string.Join(", ", term);
         });
         MessagingCenter.Subscribe<object, CurrencyModel>(this, "Load category", (obj, s) =>
           {
              CurrencyModel = s;
           });

      }

      async Task RegisterWallet()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();

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
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
         }
         catch (Exception e)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Register New Wallet Failed", "Ok");
         }
         UserDialogs.Instance.HideLoading();
         IsBusy = false;

      }

      async Task PickupMember()
      {
         await Shell.Current.GoToAsync($"{nameof(PickupUserPage)}");
      }
      async Task PickupCategory()
      {
         await Shell.Current.GoToAsync($"{nameof(PickupCategoryPage)}");
      }

   }
}
