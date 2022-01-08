using MoneyHater.Helpers;
using MoneyHater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.Services
{
   class TransactionService
   {

      public static async Task<TransactionModel> AddTransaction(TransactionModel transaction, bool isEdit, TransactionModel oldTransaction)
      {
         IRepository<TransactionModel> transactionRepo = DependencyService.Resolve<IRepository<TransactionModel>>();
         IRepository<WalletModel> walletRepo = DependencyService.Resolve<IRepository<WalletModel>>();

         var currentWallet = FirebaseService.walletService.currentWallet;
         if (currentWallet == null) return null;
         string walletId = currentWallet.Id;
         string previousPath = walletRepo.DocumentPath + $"/{walletId}";
         transactionRepo.previousPath = previousPath;

         if (isEdit)
         {
            transaction.Id = oldTransaction.Id;
            await transactionRepo.Save(transaction, transaction.Id);
            int index = currentWallet.Transactions.FindIndex(x => x.Id == oldTransaction.Id);
            currentWallet.Transactions[index] = transaction;
         }
         else
         {
            var newId = await transactionRepo.Save(transaction);
            transaction.Id = newId;
            currentWallet.Transactions.Insert(0, transaction);
         }

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
         if (isEdit)
         {
            var oldCategory = oldTransaction.CategoryModel;
            var oldAmountChanged = oldCategory.Type == "Expense" ||
                    oldCategory.Name == "Loan" ||
                    oldCategory.Name == "Repayment"
                      ? -oldTransaction.AmountByWallet
                      : oldCategory.Type == "Income" ||
                        oldCategory.Name == "Debt" ||
                        oldCategory.Name == "Debt Collection"
                      ? oldTransaction.AmountByWallet
                      : 0;

            currentWallet.Balance -= oldAmountChanged;
         }

         await walletRepo.Save(currentWallet, currentWallet.Id);

         return transaction;
      }
      public static async Task DeleteTransaction(TransactionModel transaction)
      {
         IRepository<TransactionModel> transactionRepo = DependencyService.Resolve<IRepository<TransactionModel>>();
         IRepository<WalletModel> walletRepo = DependencyService.Resolve<IRepository<WalletModel>>();

         var currentWallet = FirebaseService.walletService.currentWallet;
         if (currentWallet == null) return;
         string walletId = currentWallet.Id;
         string previousPath = walletRepo.DocumentPath + $"/{walletId}";
         transactionRepo.previousPath = previousPath;

         int index = currentWallet.Transactions.FindIndex(x => x.Id == transaction.Id);
         currentWallet.Transactions.RemoveAt(index);
         await transactionRepo.Delete(transaction);

         currentWallet.Balance -= transaction.AmountByWallet;

         await walletRepo.Save(currentWallet, currentWallet.Id);
      }

   }
}
