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
      public IRepository<AnotherUserModel> memberRepo { get; set; }
      public IRepository<BudgetModel> budgetRepo { get; set; }
      public IRepository<CategoryModel> customCategoryRepo { get; set; }
      public IRepository<DebtModel> debtRepo { get; set; }
      public IRepository<EventModel> eventRepo { get; set; }
      public IRepository<ReadyExecutedTransactionModel> readyExecutedTransactionRepo { get; set; }
      public IRepository<RecurringTransactionModel> recurringTransactionRepo { get; set; }
      public IRepository<TransactionModel> transactionRepo { get; set; }
      public WalletModel currentWallet { get; set; }
      public List<WalletModel> wallets { get; set; }

      public WalletService()
      {
         walletRepo = DependencyService.Resolve<IRepository<WalletModel>>();
         memberRepo = DependencyService.Resolve<IRepository<AnotherUserModel>>();
         budgetRepo = DependencyService.Resolve<IRepository<BudgetModel>>();
         customCategoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
         debtRepo = DependencyService.Resolve<IRepository<DebtModel>>();
         eventRepo = DependencyService.Resolve<IRepository<EventModel>>();
         readyExecutedTransactionRepo = DependencyService.Resolve<IRepository<ReadyExecutedTransactionModel>>();
         recurringTransactionRepo = DependencyService.Resolve<IRepository<RecurringTransactionModel>>();
         transactionRepo = DependencyService.Resolve<IRepository<TransactionModel>>();
      }

      public async Task AddWallet(WalletModel wallet)
      {
         var newId = await walletRepo.Save(wallet);
         wallet.Id = newId;
         string id = wallet.Id;

         string previousPath = walletRepo.DocumentPath + $"/{id}";
         memberRepo.Path = previousPath + "/members";

         foreach (var item in wallet.Members)
         {
            await memberRepo.Save(item);
         }
         wallets.Add(wallet);
         currentWallet = wallets[wallets.Count - 1];
      }

      public async Task LoadWallets()
      {
         wallets = (await walletRepo.GetAll()) as List<WalletModel>;
         if (wallets.Count > 0)
         {
            currentWallet = wallets[0];
         }
         else { currentWallet = null; }
      }
      //public async Task<List<WalletModel>> GetAllWallet()
      //{
      //   var wallets = (await walletRepo.GetAll()) as List<WalletModel>;
      //   List<Task> tasks = new List<Task>() { };
      //   for (int i = 0; i < wallets.Count; ++i)
      //   {
      //      Task t;
      //      var index = i;
      //      t = Task.Factory.StartNew(async () =>
      //      {
      //         WalletModel item = new WalletModel { };
      //         item = wallets[index];
      //         wallets[index] = await GetWalletDetails(item);
      //      });
      //      tasks.Add(t);
      //   }

      //   Task taskReturned = Task.WhenAll(tasks);
      //   try
      //   {
      //      wallets = await taskReturned;
      //   }
      //   catch
      //   {
      //      await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
      //   }
      //   return wallets;
      //}
      public async Task<WalletModel> GetWalletDetails(WalletModel wallet)
      {
         string id = wallet.Id;

         string previousPath = walletRepo.DocumentPath + $"/{id}";
         memberRepo.Path = previousPath + "/members";
         budgetRepo.previousPath = previousPath;
         customCategoryRepo.Path = previousPath + "/custom_categories";
         debtRepo.previousPath = previousPath;
         eventRepo.previousPath = previousPath;
         readyExecutedTransactionRepo.previousPath = previousPath;
         recurringTransactionRepo.previousPath = previousPath;
         transactionRepo.previousPath = previousPath;

         List<Task> tasks = new List<Task> { };
         var loadMembers = memberRepo.GetAll();
         var loadBudgets = budgetRepo.GetAll();
         var loadCustomCategories = customCategoryRepo.GetAll();
         var loadDebts = debtRepo.GetAll();
         var loadEvents = eventRepo.GetAll();
         var loadReadyExecutedTransactions = readyExecutedTransactionRepo.GetAll();
         var loadRecurringTransactions = recurringTransactionRepo.GetAll();
         var loadTransactions = transactionRepo.GetAll();

         wallet.CurrencyModel = FbApp.currencies.Find(x => x.Id == wallet.CurrencyId);

         Task taskReturned = Task.WhenAll(new Task[] { loadMembers, loadBudgets, loadCustomCategories, loadDebts,
            loadEvents, loadReadyExecutedTransactions, loadRecurringTransactions, loadTransactions});
         try
         {
            await taskReturned;
            wallet.Members = (await loadMembers) as List<AnotherUserModel>;
            wallet.Budgets = (await loadBudgets) as List<BudgetModel>;
            wallet.CustomCategories = (await loadCustomCategories) as List<CategoryModel>;
            wallet.Debts = (await loadDebts) as List<DebtModel>;
            wallet.Events = (await loadEvents) as List<EventModel>;
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
