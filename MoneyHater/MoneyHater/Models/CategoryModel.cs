using FFImageLoading.Svg.Forms;
using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MoneyHater.Models
{
   public class CategoryModel : IIdentifiable
   {
      public string Id { get; set; }
      public string Icon { get; set; }
      public string IconName { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public List<CategoryModel> Children { get; set; }

      public ImageSource ImageSource => SvgImageSource.FromUri(new Uri(Icon));
      public ImageSource ImageSourceLocal => SvgImageSource.FromResource($"MoneyHater.Resources.Category.{LocalPath}");
      public int ChildrenSize => Children == null ? 0 : Children.Count * 50;
      public string LocalPath
      {
         get
         {
            var s = Icon;
            s = Regex.Replace(s, @".*%2F", "");
            s = Regex.Replace(s, @"\?alt.*", "");
            return s;
         }
      }

   }
}
