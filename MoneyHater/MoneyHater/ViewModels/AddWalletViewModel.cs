using Acr.UserDialogs;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views;
using MoneyHater.Views.PickupPage;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class AddWalletViewModel : ViewModelBase
   {
      public double balance;
      public CurrencyModel currencyModel;
      public bool enableNotification;
      public bool excludedFromTotal;
      public List<AnotherUserModel> members;
      public string name;
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
         currencies = FirebaseService.currencies;

         MessagingCenter.Subscribe<object, List<AnotherUserModel>>(this, "Load members", (obj, s) =>
         {
            members = s;
            var term = members.ConvertAll<string>((e) => e.Name);
            MembersName = string.Join(", ", term);
         });

      }

      async Task RegisterWallet()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();
         if (Name == null)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Name Can Not Empty", "Ok");
         }
         else if (CurrencyModel == null)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Currency Unit Can Not Empty", "Ok");
         }

         else
         {
            try
            {
               List<AnotherUserModel> memberList = new List<AnotherUserModel>();
               memberList = members;
               memberList.Add(FirebaseService.userLoggedInfo as AnotherUserModel);
               await FirebaseService.walletService.AddWallet(new WalletModel()
               {
                  Balance = Balance,
                  CurrencyId = CurrencyModel?.Id,
                  EnableNotification = EnableNotification,
                  ExcludedFromTotal = ExcludedFromTotal,
                  Members = memberList,
                  Name = Name,
                  State = true,
               });
               MessagingCenter.Send<object, WalletModel>(this, "Add wallet", null);
               await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            catch (Exception e)
            {
               await App.Current.MainPage.DisplayAlert("Alert", $"Register New Wallet Failed.\n{e.Message}", "Ok");
            }
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
