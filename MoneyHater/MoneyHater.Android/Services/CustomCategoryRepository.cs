using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomCategoryRepository))]
namespace MoneyHater.Droid.Services
{
   public class CustomCategoryRepository : BaseRepository<CategoryModel>
   {
      public override string DocumentPath =>
          previousPath
          + "/custom_categories";
   }
}
