using Acr.UserDialogs;
using MoneyHater.Helpers;
using MoneyHater.Models;
using MoneyHater.Views.PickupPage;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Transaction
{
   class AddTransactionVM : ViewModelBase
   {
      double amount;
      CurrencyModel currencyModel;
      CategoryModel categoryModel;
      EventModel eventModel;
      bool excludedFromReport;
      string note;
      DateTime remind;
      AnotherUserModel with;
      WalletModel walletModel;
      public WalletModel WalletModel { get => walletModel; set => SetProperty(ref walletModel, value); }
      public List<CurrencyModel> currencies;
      public List<CurrencyModel> Currencies { get => currencies; set => SetProperty(ref currencies, value); }
      public List<EventModel> events;
      public List<EventModel> Events { get => events; set => SetProperty(ref events, value); }
      public List<AnotherUserModel> anotherUsers;
      public List<AnotherUserModel> AnotherUsers { get => anotherUsers; set => SetProperty(ref anotherUsers, value); }
      public CategoryModel CategoryModel { get => categoryModel; set => SetProperty(ref categoryModel, value); }
      public EventModel EventModel { get => eventModel; set => SetProperty(ref eventModel, value); }
      public AnotherUserModel With { get => with; set => SetProperty(ref with, value); }
      public DateTime Remind { get => remind; set => SetProperty(ref remind, value); }
      public CurrencyModel CurrencyModel { get => currencyModel; set => SetProperty(ref currencyModel, value); }
      public string Note { get => note; set => SetProperty(ref note, value); }
      public double Amount { get => amount; set => SetProperty(ref amount, value); }
      public bool ExcludedFromReport { get => excludedFromReport; set => SetProperty(ref excludedFromReport, value); }

      public AsyncCommand CreateCommand { get; }
      public AsyncCommand PickupCategoryCommand { get; }

      public AddTransactionVM()
      {
         CreateCommand = new AsyncCommand(CreateTransaction);
         Events = FirebaseService.walletService.currentWallet.Events;
         PickupCategoryCommand = new AsyncCommand(PickupCategory);
         Currencies = FirebaseService.currencies;
         AnotherUsers = FirebaseService.usersPublicInfo;
         WalletModel = FirebaseService.walletService.currentWallet;

         MessagingCenter.Subscribe<object, CategoryModel>(this, "Load category", (obj, s) =>
           {
              CategoryModel = s;
           });
      }

      async Task CreateTransaction()
      {
         IsBusy = true;
         UserDialogs.Instance.ShowLoading();

         if (CategoryModel == null)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Category Can Not Empty", "Ok");
         }
         else if (Amount < 0)
         {
            await App.Current.MainPage.DisplayAlert("Alert", "Amount Can Not Equal 0", "Ok");
         }
         else
         {
            try
            {
               double calcAmount;
               if (CurrencyModel == null)
               {
                  calcAmount = Amount;
               }
               else
               {
                  calcAmount = Amount * (walletModel.CurrencyModel.RateUS / CurrencyModel.RateUS);
               }
               var newTransaction = new TransactionModel()
               {
                  Amount = Amount,
                  AmountByWallet = Math.Round(calcAmount, 2),
                  CategoryId = CategoryModel.Id,
                  CurrencyId = (CurrencyModel ?? WalletModel.CurrencyModel).Id,
                  EventId = EventModel?.Id,
                  ExcludedFromReport = ExcludedFromReport,
                  ExecutedTime = DateTime.Now,
                  Note = Note,
                  Remind = Remind,
                  WithUserId = With?.Id,
               };

               newTransaction = await FirebaseService.walletService.AddTransaction(newTransaction);
               MessagingCenter.Send<object, TransactionModel>(this, "Add transaction", newTransaction);
               await Shell.Current.GoToAsync($"..");
            }
            catch (Exception e)
            {
               await App.Current.MainPage.DisplayAlert("Alert", "Register New Transaction Failed", "Ok");
            }
         }
         UserDialogs.Instance.HideLoading();
         IsBusy = false;

      }

      async Task PickupCategory()
      {
         await Shell.Current.GoToAsync($"{nameof(PickupCategoryPage)}");
      }
   }
}
