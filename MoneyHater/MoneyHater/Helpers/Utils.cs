using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Helpers
{
   class Utils
   {
      public static List<List<T>> Make2DArrayH<T>(List<T> input, int width)
      {
         List<List<T>> output = new List<List<T>>();
         for (int i = 0; i < input.Count; i++)
         {
            if (i % width == 0)
            {
               output.Add(new List<T>());
            }
            output[output.Count - 1].Add(input[i]);
         }
         return output;
      }
      public static List<List<T>> Make2DArrayV<T>(List<T> input, int width)
      {
         List<List<T>> output = new List<List<T>>();
         for (int i = 0; i < width; i++)
         {
            output.Add(new List<T>());
         }
         for (int i = 0; i < input.Count; i++)
         {
            output[i % width].Add(input[i]);
         }
         return output;
      }

   }
}
