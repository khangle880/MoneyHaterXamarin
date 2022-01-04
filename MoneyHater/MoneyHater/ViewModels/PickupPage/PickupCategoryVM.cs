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
      public List<CategoryShowable> categories;
      public List<CategoryShowable> Categories { get => categories; set => SetProperty(ref categories, value); }
      public CategoryShowable categorySelected;
      public CategoryShowable CategorySelected { get => categorySelected; set => SetProperty(ref categorySelected, value); }
      public ObservableRangeCollection<Grouping<string, CategoryShowable>> CategoriesGrouped { get; }

      public AsyncCommand CompleteCommand { get; }

      public PickupCategoryVM()
      {
         categories = FbApp.categories.Select(x => new CategoryShowable()
         {
            Type = x.Type,
            Name = x.Name,
            Id = x.Id,
            Icon = x.Icon,
            Children = x.Children,
            childrenShowable = x.Children.Select(
             y => new CategoryShowable()
             {
                Type = y.Type,
                Name = y.Name,
                Id = y.Id,
                Icon = y.Icon,
                Children = y.Children,
                ImageSource = SvgImageSource.FromUri(new Uri(y.Icon)),
             }).ToList(),
            ImageSource = SvgImageSource.FromUri(new Uri(x.Icon)),
         }).ToList();

         CompleteCommand = new AsyncCommand(Complete);
         CategoriesGrouped = new ObservableRangeCollection<Grouping<string, CategoryShowable>>();
         CategoriesGrouped.Clear();
         CategoriesGrouped.Add(new Grouping<string, CategoryShowable>("Expense", categories.Where(c => c.Type == "Expense")));
         CategoriesGrouped.Add(new Grouping<string, CategoryShowable>("Income", categories.Where(c => c.Type == "Income")));
         CategoriesGrouped.Add(new Grouping<string, CategoryShowable>("Debt & Loan", categories.Where(c => c.Type == "Debt & Loan")));
      }

      async Task Complete()
      {
         MessagingCenter.Send<object, CategoryModel>(this, "Load category", categorySelected);
         await Shell.Current.GoToAsync("..");

      }
   }
   class CategoryShowable : CategoryModel
   {
      public ImageSource ImageSource { get; set; }
      public List<CategoryShowable> childrenShowable { get; set; }
   }
}
