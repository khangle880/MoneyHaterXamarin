using FFImageLoading.Svg.Forms;
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

namespace MoneyHater.ViewModels.PickupPage
{
   class PickupCategoryVM : ViewModelBase
   {
      public List<CategoryModel> categories;
      public List<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
      public CategoryModel categorySelected;
      public CategoryModel CategorySelected { get => categorySelected; set => SetProperty(ref categorySelected, value); }
      public List<CategoryModel> incomeList;
      public List<CategoryModel> IncomeList { get => incomeList; set => SetProperty(ref incomeList, value); }
      public List<CategoryModel> debtLoanList;
      public List<CategoryModel> DebtLoanList { get => debtLoanList; set => SetProperty(ref debtLoanList, value); }
      public List<CategoryModel> expenseList;
      public List<CategoryModel> ExpenseList { get => expenseList; set => SetProperty(ref expenseList, value); }


      public AsyncCommand CompleteCommand { get; }
      public AsyncCommand<CategoryModel> SelectedCommand { get; }

      public PickupCategoryVM()
      {
         categories = FirebaseService.categories;

         CompleteCommand = new AsyncCommand(Complete);
         SelectedCommand = new AsyncCommand<CategoryModel>(Selected);
         Task.Run(async () =>
           {
              await Task.Delay(800);
              ExpenseList = categories.Where(c => c.Type == "Expense").ToList();
              IncomeList = categories.Where(c => c.Type == "Income").ToList();
              DebtLoanList = categories.Where(c => c.Type == "Debt & Loan").ToList();
           });

      }

      async Task Selected(CategoryModel category)
      {
         if (category == null) return;
         MessagingCenter.Send<object, CategoryModel>(this, "Load category", category);
         await Shell.Current.GoToAsync("..");
      }
      async Task Complete()
      {
         MessagingCenter.Send<object, CategoryModel>(this, "Load category", categorySelected);
         await Shell.Current.GoToAsync("..");
      }
   }
}
