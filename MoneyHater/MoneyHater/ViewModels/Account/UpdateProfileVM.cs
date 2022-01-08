using Acr.UserDialogs;
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
   class UpdateProfileVM : ViewModelBase
   {
      public string name;
      public string email;
      public bool premiumStatus;

      public string Name { get => name; set => SetProperty(ref name, value); }
      public string Email { get => email; set => SetProperty(ref email, value); }
      public bool PremiumStatus { get => premiumStatus; set => SetProperty(ref premiumStatus, value); }
      public AsyncCommand CompleteCommand { get; }
      public AsyncCommand SaveCommand { get; }

      public UpdateProfileVM()
      {
         var userInfo = FirebaseService.userLoggedInfo;
         Name = userInfo.Name;
         Email = userInfo.Email;
         PremiumStatus = userInfo.PremiumStatus;
         SaveCommand = new AsyncCommand(SaveInfo);
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }

      async Task SaveInfo()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();
         if (string.IsNullOrWhiteSpace(Name))
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Name Can Not Empty", "Ok");
         }
         else
         {
            try
            {
               await FirebaseService.UpdateProfile(new UserModel() { Name = Name, Email = Email, PremiumStatus = PremiumStatus });

               MessagingCenter.Send<object, bool>(this, "Update profile", true);
               await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
               await App.Current.MainPage.DisplayAlert("Alert", $"Update Info Failed.\n{e.Message}", "Ok");
            }
         }

         UserDialogs.Instance.HideLoading();
         IsBusy = false;


      }

   }
}
