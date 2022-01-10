using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MoneyHater.Models
{
   class IconSvg
   {
      public string url { get; set; }
      public ImageSource ImageSource => SvgImageSource.FromUri(new Uri(url));
      public string LocalName
      {
         get
         {
            var s = url;
            s = Regex.Replace(s, @".*%2F", "");
            s = Regex.Replace(s, @"\?alt.*", "");
            s = Regex.Replace(s, @"icons8-", "");
            s = Regex.Replace(s, @"-", " ");
            s = Regex.Replace(s, @"64\.svg", "");
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower()); ;
         }
      }
   }
}
