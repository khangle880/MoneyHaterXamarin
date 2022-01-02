using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MoneyHater.Helpers
{
   public interface IEnvironment
   {
      void SetStatusBarColor(Color color, bool darkStatusBarTint);
   }
}
