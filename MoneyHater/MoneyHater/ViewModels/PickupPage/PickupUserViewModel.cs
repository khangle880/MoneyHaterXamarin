using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyHater.ViewModels.PickupPage
{
   class PickupUserViewModel : ViewModelBase
   {
      public List<SelectableUser> selectableUsers;
      public List<SelectableUser> SelectableUsers { get => selectableUsers; set => SetProperty(ref selectableUsers, value); }

      public AsyncCommand CompleteCommand { get; }

      public PickupUserViewModel()
      {
         selectableUsers = FbApp.usersPublicInfo.Select(x => new SelectableUser()
         {
            Email = x.Email,
            Id = x.Id,
            IsSelected = false,
            Name = x.Name,
         }).ToList();
         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {


      }


   }

   class SelectableUser : AnotherUserModel
   {
      public bool IsSelected { get; set; }
   }
}
