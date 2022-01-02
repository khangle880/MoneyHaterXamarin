using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class CategoryModel : IIdentifiable
   {
      public string Id { get; set; }
      public string Icon { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public List<CategoryModel> Children { get; set; }
   }
}
