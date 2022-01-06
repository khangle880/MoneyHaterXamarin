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
         wallets.Insert(0, wallet);
         currentWallet = wallets[wallets.Count - 1];
      }
      public async Task<TransactionModel> AddTransaction(TransactionModel transaction)
      {
         if (currentWallet == null) return null;
         string walletId = currentWallet.Id;
         string previousPath = walletRepo.DocumentPath + $"/{walletId}";
         transactionRepo.previousPath = previousPath;

         var newId = await transactionRepo.Save(transaction);
         transaction.Id = newId;
         currentWallet.Transactions.Insert(0, transaction);

         var category = transaction.CategoryModel;
         var amountChanged = category.Type == "Expense" ||
                 category.Name == "Loan" ||
                 category.Name == "Repayment"
                   ? -transaction.AmountByWallet
                   : category.Type == "Income" ||
                     category.Name == "Debt" ||
                     category.Name == "Debt Collection"
                   ? transaction.AmountByWallet
                   : 0;
         currentWallet.Balance += amountChanged;

         await walletRepo.Save(currentWallet, currentWallet.Id);

         return transaction;
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
         memberRepo.Path = previousPath + "/members";
         budgetRepo.previousPath = previousPath;
         customCategoryRepo.Path = previousPath + "/custom_categories";
         debtRepo.previousPath = previousPath;
         eventRepo.previousPath = previousPath;
         readyExecutedTransactionRepo.previousPath = previousPath;
         recurringTransactionRepo.previousPath = previousPath;
         transactionRepo.previousPath = previousPath;

         // load event before
         List<Task> tasks = new List<Task> { };
         var loadMembers = memberRepo.GetAll();
         var loadBudgets = budgetRepo.GetAll();
         var loadCustomCategories = customCategoryRepo.GetAll();
         var loadDebts = debtRepo.GetAll();
         var loadEvents = eventRepo.GetAll();
         var loadReadyExecutedTransactions = readyExecutedTransactionRepo.GetAll();
         var loadRecurringTransactions = recurringTransactionRepo.GetAll();
         var loadTransactions = transactionRepo.GetAll();

         wallet.Events = (await loadEvents) as List<EventModel>;
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
