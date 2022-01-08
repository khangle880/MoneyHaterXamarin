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

      public List<IconSvg> icons;
      public List<IconSvg> Icons { get => icons; set => SetProperty(ref icons, value); }

      public List<CurrencyModel> currencies;
      public List<CurrencyModel> Currencies { get => currencies; set => SetProperty(ref currencies, value); }
      public AsyncCommand RegisterCommand { get; }
      public AsyncCommand PickupMemberCommand { get; }
      public AsyncCommand PickupCategoryCommand { get; }
      public bool isEdit = false;

      public AddWalletViewModel()
      {
         currencies = FirebaseService.currencies;
         var list = FirebaseService.icons.Select(x => new IconSvg() { url = x });
         Icons = list.ToList();


         Task.Run(async () =>
           {
              await Task.Delay(200);
              if (WalletId != null)
              {
                 var WalletModel = FirebaseService.walletService.wallets.Find(x => x.Id == WalletId);
                 if (WalletModel != null)
                 {
                    isEdit = true;
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
                    MembersName = string.Join(", ", Members);
                 }
              }
           });

         MessagingCenter.Subscribe<object, List<AnotherUserModel>>(this, "Load members", (obj, s) =>
         {
            members = s;
            var term = members.ConvertAll<string>((e) => e.Name);
            MembersName = string.Join(", ", term);
         });

         RegisterCommand = new AsyncCommand(RegisterWallet);
         PickupMemberCommand = new AsyncCommand(PickupMember);
         PickupCategoryCommand = new AsyncCommand(PickupCategory);
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
                  Icon = IconSelected.url,
                  State = true,
               }, isEdit, oldWalletModel);
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
