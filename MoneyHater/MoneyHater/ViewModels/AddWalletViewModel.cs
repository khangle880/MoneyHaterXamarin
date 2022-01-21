using Acr.UserDialogs;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views;
using MoneyHater.Views.PickupPage;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   [QueryProperty(nameof(WalletId), nameof(WalletId))]
   class AddWalletViewModel : ViewModelBase
   {
      string walletId;
      public string WalletId { get => walletId; set => SetProperty(ref walletId, Uri.UnescapeDataString(value)); }
      WalletModel oldWalletModel;
      public double balance;
      public CurrencyModel currencyModel;
      public bool enableNotification;
      public bool excludedFromTotal;
      public List<AnotherUserModel> members;
      public string name;
      public IconSvg icon;
      public string membersName;
      public IconSvg IconSelected { get => icon; set => SetProperty(ref icon, value); }
      public string Name { get => name; set => SetProperty(ref name, value); }
      public double Balance { get => balance; set => SetProperty(ref balance, value); }
      public CurrencyModel CurrencyModel { get => currencyModel; set => SetProperty(ref currencyModel, value); }
      public bool EnableNotification { get => enableNotification; set => SetProperty(ref enableNotification, value); }
      public bool ExcludedFromTotal { get => excludedFromTotal; set => SetProperty(ref excludedFromTotal, value); }
      public List<AnotherUserModel> Members { get => members; set => SetProperty(ref members, value); }
      public string MembersName { get => membersName; set => SetProperty(ref membersName, value); }

      public string headerText;
      public string HeaderText { get => headerText; set => SetProperty(ref headerText, value); }
      public List<CurrencyModel> currencies;
      public List<CurrencyModel> Currencies { get => currencies; set => SetProperty(ref currencies, value); }
      public AsyncCommand RegisterCommand { get; }
      public AsyncCommand PickupMemberCommand { get; }
      public AsyncCommand PickupCategoryCommand { get; }
      public AsyncCommand PickupIconCommand { get; }
      public bool isEdit = false;

      public AddWalletViewModel()
      {
         currencies = FirebaseService.currencies;
         if (FirebaseService.walletService.currentWallet == null)
         {
            HeaderText = "Create Your First Wallet";
         }
         else
         {
            HeaderText = "Create New Wallet";
         }
         IconSelected = new IconSvg() { url = "https://firebasestorage.googleapis.com/v0/b/moneyhater-e3629.appspot.com/o/icon%2Ficons8-wallet-64.svg?alt=media&token=27167c8a-818a-4b46-a076-9aebfd46c9ea" };

         Task.Run(async () =>
           {
              await Task.Delay(200);
              if (WalletId != null)
              {
                 var WalletModel = FirebaseService.walletService.wallets.Find(x => x.Id == WalletId);
                 if (WalletModel != null)
                 {
                    isEdit = true;
                    HeaderText = "Edit Wallet";
                    oldWalletModel = WalletModel;
                    Name = WalletModel.Name;
                    if (WalletModel.Icon != null)
                    {
                       IconSelected = new IconSvg() { url = WalletModel.Icon };
                    }
                    Balance = WalletModel.Balance;
                    CurrencyModel = WalletModel.CurrencyModel;
                    EnableNotification = WalletModel.EnableNotification;
                    ExcludedFromTotal = WalletModel.ExcludedFromTotal;
                    Members = WalletModel.Members;
                    var term = Members.ConvertAll<string>((e) => e.Name);
                    MembersName = string.Join(", ", term);
                 }
              }
           });

         MessagingCenter.Subscribe<object, List<AnotherUserModel>>(this, "Load members", (obj, s) =>
         {
            members = s;
            var term = members.ConvertAll<string>((e) => e.Name);
            MembersName = string.Join(", ", term);
         });
         MessagingCenter.Subscribe<object, IconSvg>(this, "Pick icon", (obj, s) =>
         {
            IconSelected = s;
         });

         RegisterCommand = new AsyncCommand(RegisterWallet);
         PickupMemberCommand = new AsyncCommand(PickupMember);
         PickupCategoryCommand = new AsyncCommand(PickupCategory);
         PickupIconCommand = new AsyncCommand(PickupIcon);
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
               var newWallet = new WalletModel()
               {
                  Balance = Balance,
                  CurrencyId = CurrencyModel?.Id,
                  EnableNotification = EnableNotification,
                  ExcludedFromTotal = ExcludedFromTotal,
                  Members = Members ?? new List<AnotherUserModel>(),
                  Name = Name,
                  Icon = IconSelected?.url,
                  State = true,
               };
               newWallet = await FirebaseService.walletService.AddWallet(newWallet, isEdit, oldWalletModel);
               MessagingCenter.Send<object, WalletModel>(this, "Add wallet", newWallet);
               await Shell.Current.GoToAsync("..");
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
      async Task PickupIcon()
      {
         await Shell.Current.GoToAsync($"{nameof(PickupIconPage)}");
      }

   }
}
