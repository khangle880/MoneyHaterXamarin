using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Account
{
   [QueryProperty(nameof(WalletId), nameof(WalletId))]
   class WalletDetailVM : ViewModelBase
   {
      string walletId;
      public string WalletId { get => walletId; set => SetProperty(ref walletId, Uri.UnescapeDataString(value)); }
      WalletModel walletModel;
      public WalletModel WalletModel { get => walletModel; set => SetProperty(ref walletModel, value); }

      public string membersName;
      public string MembersName { get => membersName; set => SetProperty(ref membersName, value); }
      public List<AnotherUserModel> members;
      public List<AnotherUserModel> Members { get => members; set => SetProperty(ref members, value); }
      public AsyncCommand CompleteCommand { get; }
      public AsyncCommand SwitchToCurrentCommand { get; }
      public bool switchEnable;
      public bool SwitchEnable { get => switchEnable; set => SetProperty(ref switchEnable, value); }
      public ImageSource PremiumSource { get; set; }

      public WalletDetailVM()
      {
         PremiumSource = ImageSource.FromResource("MoneyHater.Resources.Images.premium3.png");
         CompleteCommand = new AsyncCommand(Complete);
         SwitchToCurrentCommand = new AsyncCommand(SwitchToCurrent);
         Members = new List<AnotherUserModel>();
         SwitchEnable = true;
         Task.Run(async () =>
         {
            await Task.Delay(200);
            if (WalletId != null)
            {
               WalletModel = FirebaseService.walletService.wallets.Find(x => x.Id == WalletId);
               SwitchEnable = WalletModel.Id != FirebaseService.walletService.currentWallet.Id;
               if (WalletModel == null)
               {
                  WalletModel = new WalletModel() { };
               }
               Members = WalletModel.Members ?? new List<AnotherUserModel>();
               var term = Members.ConvertAll<string>((e) => e.Name);
               MembersName = string.Join(", ", term);
            }
         });
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }
      async Task SwitchToCurrent()
      {
         FirebaseService.walletService.currentWallet = WalletModel;
         MessagingCenter.Send<object, WalletModel>(this, "Change wallet", walletModel);
         await Shell.Current.GoToAsync("..");
      }


   }
}
