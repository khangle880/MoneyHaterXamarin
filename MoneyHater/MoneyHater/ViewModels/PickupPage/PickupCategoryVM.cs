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
      public ObservableRangeCollection<Grouping<string, CategoryModel>> CategoriesGrouped { get; }

      public AsyncCommand CompleteCommand { get; }

      public PickupCategoryVM()
      {
         categories = FirebaseService.categories;

         CompleteCommand = new AsyncCommand(Complete);
         CategoriesGrouped = new ObservableRangeCollection<Grouping<string, CategoryModel>>();
         CategoriesGrouped.Clear();
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Expense", categories.Where(c => c.Type == "Expense")));
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Income", categories.Where(c => c.Type == "Income")));
         CategoriesGrouped.Add(new Grouping<string, CategoryModel>("Debt & Loan", categories.Where(c => c.Type == "Debt & Loan")));
      }

      async Task Complete()
      {
         MessagingCenter.Send<object, CategoryModel>(this, "Load category", categorySelected);
         await Shell.Current.GoToAsync("..");

      }
   }
}
