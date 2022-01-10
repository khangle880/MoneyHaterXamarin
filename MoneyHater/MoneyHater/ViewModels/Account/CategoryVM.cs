using MoneyHater.Helpers;
using MoneyHater.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.ViewModels.Account
{
   class CategoryVM : ViewModelBase
   {
      public List<CategoryModel> categories;
      public List<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
      public List<CategoryModel> incomeList;
      public List<CategoryModel> IncomeList { get => incomeList; set => SetProperty(ref incomeList, value); }
      public List<CategoryModel> debtLoanList;
      public List<CategoryModel> DebtLoanList { get => debtLoanList; set => SetProperty(ref debtLoanList, value); }
      public List<CategoryModel> expenseList;
      public List<CategoryModel> ExpenseList { get => expenseList; set => SetProperty(ref expenseList, value); }
      public AsyncCommand CompleteCommand { get; }

      public CategoryVM()
      {
         categories = FirebaseService.categories;

         CompleteCommand = new AsyncCommand(Complete);
         Task.Run(async () =>
           {
              await Task.Delay(800);
              ExpenseList = categories.Where(c => c.Type == "Expense").ToList();
              IncomeList = categories.Where(c => c.Type == "Income").ToList();
              DebtLoanList = categories.Where(c => c.Type == "Debt & Loan").ToList();
           });
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }

   }
}
