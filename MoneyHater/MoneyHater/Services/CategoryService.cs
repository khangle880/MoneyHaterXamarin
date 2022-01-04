using MoneyHater.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.Services
{
   public class CategoryService
   {
      public IRepository<CategoryModel> rootCategoryRepo { get; set; }
      public List<CategoryModel> categories { get; set; }

      public CategoryService()
      {
         rootCategoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
      }

      public async Task<List<CategoryModel>> LoadCategories()
      {
         {
            var categories = (await rootCategoryRepo.GetAll()) as List<CategoryModel>;
            List<Task<CategoryModel>> tasks = new List<Task<CategoryModel>>() { };
            for (int i = 0; i < categories.Count; ++i)
            {
               var index = i;
               CategoryModel item = new CategoryModel { };
               item = categories[index];
               tasks.Add(GetChildren(item));
            }

            Task taskReturned = Task.WhenAll(tasks);
            try
            {
               await taskReturned;
               for (int i = 0; i < tasks.Count; i++)
               {
                  var index = i;
                  categories[index] = await tasks[index];
               }
            }
            catch
            {
               await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
            }
            return categories;
         }
      }

      public async Task<CategoryModel> GetChildren(CategoryModel category)
      {
         string id = category.Id;
         IRepository<CategoryModel> subCategoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
         subCategoryRepo.Path = $"categories/{id}/children";

         var categories = (await rootCategoryRepo.GetAll()) as List<CategoryModel>;
         category.Children = categories;
         return category;
      }
   }
}
