using MoneyHater.Helpers;
using MoneyHater.Views;
using MoneyHater.Views.Account;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels
{
   class MyPageVM : ViewModelBase
   {
      public string name;
      public string email;
      public bool premiumStatus;

      public string Name { get => name; set => SetProperty(ref name, value); }
      public string Email { get => email; set => SetProperty(ref email, value); }
      public bool PremiumStatus { get => premiumStatus; set => SetProperty(ref premiumStatus, value); }

      public AsyncCommand GotoUpdateProfileCommand { get; }
      public AsyncCommand GotoWalletsCommand { get; }
      public AsyncCommand GotoIconsCommand { get; }
      public AsyncCommand GotoCategoriesCommand { get; }
      public AsyncCommand SignOutCommand { get; }

      public MyPageVM()
      {
         LoadUserInfo();
         GotoUpdateProfileCommand = new AsyncCommand(UpdateProfile);
         GotoWalletsCommand = new AsyncCommand(GotoWallets);
         GotoIconsCommand = new AsyncCommand(GotoIcons);
         GotoCategoriesCommand = new AsyncCommand(GotoCategories);
         SignOutCommand = new AsyncCommand(SignOut);
         MessagingCenter.Subscribe<object, bool>(this, "Update profile", (obj, s) =>
         {
            if (s)
            {
               LoadUserInfo();
            }
         });

      }

      void LoadUserInfo()
      {
         var userInfo = FirebaseService.userLoggedInfo;
         Name = userInfo.Name;
         Email = userInfo.Email;
         PremiumStatus = userInfo.PremiumStatus;

      }

      async Task UpdateProfile()
      {
         await Shell.Current.GoToAsync($"{nameof(UpdateProfilePage)}");
      }
      async Task GotoIcons()
      {
         await Shell.Current.GoToAsync($"{nameof(IconPage)}");
      }
      async Task GotoCategories()
      {
         await Shell.Current.GoToAsync($"{nameof(CategoryPage)}");
      }
      async Task GotoWallets()
      {
         await Shell.Current.GoToAsync($"{nameof(ManageWalletPage)}");
      }

      async Task SignOut()
      {
         IsBusy = true;
         try
         {
            if (FirebaseService.auth.SignOut())
            {
               FirebaseService.clear();
               Application.Current.MainPage = new SplashPage();
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("Alert", "Something Happened", "Ok");
            }
         }
         catch (Exception e)
         {
            await App.Current.MainPage.DisplayAlert("Alert", e.Message, "Ok");
         }

         IsBusy = false;
      }


   }
}
