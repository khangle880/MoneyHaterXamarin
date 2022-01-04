using System;
using MoneyHater.Droid.Services;
using MoneyHater.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CategoryRepository))]
namespace MoneyHater.Droid.Services
{
   public class CategoryRepository : BaseRepository<CategoryModel>
   {
      public override string DocumentPath => Path ??
         "categories";
   }
}
