﻿using FFImageLoading.Svg.Forms;
using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyHater.Models
{
   public class CategoryModel : IIdentifiable
   {
      public string Id { get; set; }
      public string Icon { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public List<CategoryModel> Children { get; set; }

      public ImageSource ImageSource => SvgImageSource.FromUri(new Uri(Icon));
      public int ChildrenSize => Children.Count * 50;
   }
}
