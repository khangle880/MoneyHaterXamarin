using MoneyHater.Helpers;
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
   class PickupUserViewModel : ViewModelBase
   {
      public List<SelectableUser> selectableUsers;
      public List<SelectableUser> SelectableUsers { get => selectableUsers; set => SetProperty(ref selectableUsers, value); }

      public AsyncCommand CompleteCommand { get; }
      public ImageSource PremiumSource { get; set; }

      public PickupUserViewModel()
      {
         PremiumSource = ImageSource.FromResource("MoneyHater.Resources.Images.premium3.png");
         var list = FirebaseService.usersPublicInfo.Where(x => x.Id != FirebaseService.userLoggedInfo.Id);
         selectableUsers = list.Select(x => new SelectableUser()
         {
            Email = x.Email,
            Id = x.Id,
            IsSelected = false,
            Name = x.Name,
            PremiumStatus = x.PremiumStatus,
         }).ToList();
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         var users = selectableUsers.Where(x => x.IsSelected == true).Cast<AnotherUserModel>().ToList();
         MessagingCenter.Send<object, List<AnotherUserModel>>(this, "Load members", users);
         await Shell.Current.GoToAsync("..");
      }
   }

   class SelectableUser : AnotherUserModel
   {
      public bool IsSelected { get; set; }
   }
}
