using MoneyHater.Helpers;
using MoneyHater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.Services
{
   public class WalletService
   {
      public IRepository<WalletModel> walletRepo { get; set; }
      public WalletModel currentWallet { get; set; }
      public List<WalletModel> wallets { get; set; }

      public WalletService()
      {
         walletRepo = DependencyService.Resolve<IRepository<WalletModel>>();
      }

      public async Task AddWallet(WalletModel wallet, bool isEdit, WalletModel oldWallet)
      {
         IRepository<AnotherUserModel> memberRepo = DependencyService.Resolve<IRepository<AnotherUserModel>>();
         if (isEdit)
         {
            wallet.Id = oldWallet.Id;
            await walletRepo.Save(wallet, wallet.Id);
            int index = wallets.FindIndex(x => x.Id == oldWallet.Id);
            wallets[index] = wallet;

            string previousPath = walletRepo.DocumentPath + $"/{wallet.Id}";
            memberRepo.Path = previousPath + "/members";

            List<AnotherUserModel> popMembers = new List<AnotherUserModel>();
            foreach (var item in oldWallet.Members)
            {
               if (wallet.Members.Find(x => x.Id == item.Id) == null)
               {
                  popMembers.Add(item);
               }
            }
            List<AnotherUserModel> pushMembers = new List<AnotherUserModel>();
            foreach (var item in wallet.Members)
            {
               if (oldWallet.Members.Find(x => x.Id == item.Id) == null)
               {
                  pushMembers.Add(item);
               }
            }
            foreach (var item in popMembers)
            {
               await memberRepo.Delete(item);
            }
            foreach (var item in popMembers)
            {
               await memberRepo.Save(item, item.Id);
            }
         }
         else
         {
            var newId = await walletRepo.Save(wallet);
            wallet.Id = newId;
            string id = wallet.Id;

            string previousPath = walletRepo.DocumentPath + $"/{id}";
            memberRepo.Path = previousPath + "/members";

            foreach (var item in wallet.Members)
            {
               await memberRepo.Save(item, item.Id);
            }
            wallets.Insert(0, wallet);
            if (currentWallet == null)
            {
               currentWallet = wallets[wallets.Count - 1];
               MessagingCenter.Send<object, WalletModel>(this, "Change wallet", currentWallet);
            }
         }
      }

      public async Task<WalletModel> DeleteWallet(WalletModel wallet)
      {
         var isDeleteCurrentWallet = wallet.Id == currentWallet.Id;

         int index = wallets.FindIndex(x => x.Id == wallet.Id);
         wallets.RemoveAt(index);
         await walletRepo.Delete(wallet);
         if (isDeleteCurrentWallet)
         {
            if (wallets.Count != 0)
            {
               currentWallet = wallets[0];
            }
            else { currentWallet = null; }
            MessagingCenter.Send<object, WalletModel>(this, "Change wallet", currentWallet);
         }
         return currentWallet;
      }


      public async Task LoadWallets()
      {
         wallets = (await walletRepo.GetAll()) as List<WalletModel>;
         if (wallets.Count > 0)
         {
            currentWallet = wallets[0];
         }
         else { currentWallet = null; }
         List<Task<WalletModel>> tasks = new List<Task<WalletModel>>() { };
         for (int i = 0; i < wallets.Count; ++i)
         {
            var index = i;
            WalletModel item = new WalletModel { };
            item = wallets[index];
            tasks.Add(GetWalletDetails(item));
         }

         Task taskReturned = Task.WhenAll(tasks);
         try
         {
            await taskReturned;
            for (int i = 0; i < tasks.Count; i++)
            {
               var index = i;
               wallets[index] = await tasks[index];
            }
         }
         catch
         {
            await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
         }

      }

      public async Task<WalletModel> GetWalletDetails(WalletModel wallet)
      {
         string id = wallet.Id;

         string previousPath = walletRepo.DocumentPath + $"/{id}";
         IRepository<AnotherUserModel> memberRepo = DependencyService.Resolve<IRepository<AnotherUserModel>>();
         IRepository<BudgetModel> budgetRepo = DependencyService.Resolve<IRepository<BudgetModel>>();
         IRepository<CategoryModel> customCategoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
         IRepository<DebtModel> debtRepo = DependencyService.Resolve<IRepository<DebtModel>>();
         IRepository<EventModel> eventRepo = DependencyService.Resolve<IRepository<EventModel>>();
         IRepository<ReadyExecutedTransactionModel> readyExecutedTransactionRepo = DependencyService.Resolve<IRepository<ReadyExecutedTransactionModel>>();
         IRepository<RecurringTransactionModel> recurringTransactionRepo = DependencyService.Resolve<IRepository<RecurringTransactionModel>>();
         IRepository<TransactionModel> transactionRepo = DependencyService.Resolve<IRepository<TransactionModel>>();

         // load event before
         eventRepo.previousPath = previousPath;
         wallet.Events = new List<EventModel>() { };
         var loadEvents = eventRepo.GetAll();
         wallet.Events = (await loadEvents) as List<EventModel>;

         memberRepo.Path = previousPath + "/members";
         budgetRepo.previousPath = previousPath;
         customCategoryRepo.Path = previousPath + "/custom_categories";
         debtRepo.previousPath = previousPath;
         readyExecutedTransactionRepo.previousPath = previousPath;
         recurringTransactionRepo.previousPath = previousPath;
         transactionRepo.Path = previousPath + "/transactions";


         List<Task> tasks = new List<Task> { };
         var loadMembers = memberRepo.GetAll();
         var loadBudgets = budgetRepo.GetAll();
         var loadCustomCategories = customCategoryRepo.GetAll();
         var loadDebts = debtRepo.GetAll();
         var loadReadyExecutedTransactions = readyExecutedTransactionRepo.GetAll();
         var loadRecurringTransactions = recurringTransactionRepo.GetAll();
         var loadTransactions = transactionRepo.GetAll();

         Task taskReturned = Task.WhenAll(new Task[] { loadMembers, loadBudgets, loadCustomCategories, loadDebts,
             loadReadyExecutedTransactions, loadRecurringTransactions, loadTransactions});
         try
         {
            await taskReturned;
            wallet.Members = (await loadMembers) as List<AnotherUserModel>;
            wallet.Budgets = (await loadBudgets) as List<BudgetModel>;
            wallet.CustomCategories = (await loadCustomCategories) as List<CategoryModel>;
            wallet.Debts = (await loadDebts) as List<DebtModel>;
            wallet.ReadyExecutedTransactions = (await loadReadyExecutedTransactions) as List<ReadyExecutedTransactionModel>;
            wallet.RecurringTransactions = (await loadRecurringTransactions) as List<RecurringTransactionModel>;
            wallet.Transactions = (await loadTransactions) as List<TransactionModel>;
         }
         catch
         {
            await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
         }
         return wallet;

      }
   }
}
