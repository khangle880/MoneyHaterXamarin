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
      public ObservableRangeCollection<Grouping<string, CategoryModel>> CategoriesGrouped { get; }
      public AsyncCommand CompleteCommand { get; }

      public CategoryVM()
      {
         categories = FirebaseService.categories;

         CategoriesGrouped = new ObservableRangeCollection<Grouping<string, CategoryModel>>();
         CategoriesGrouped.Clear();
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Expense", categories.Where(c => c.Type == "Expense")));
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Income", categories.Where(c => c.Type == "Income")));
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Debt & Loan", categories.Where(c => c.Type == "Debt & Loan")));

         CompleteCommand = new AsyncCommand(Complete);
      }

      async Task Complete()
      {
         await Shell.Current.GoToAsync("..");
      }

   }
}
