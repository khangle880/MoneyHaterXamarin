using System;
using System.Collections.Generic;
using Java.Util;
using MoneyHater.Services;
using Newtonsoft.Json;

namespace MoneyHater.Droid.Extensions
{
   public static class IIdentifiableExtensions
   {
      public static HashMap Convert(this IIdentifiable item)
      {
         var docData = new Dictionary<string, Java.Lang.Object>();

         var jsonStr = JsonConvert.SerializeObject(item);
         var propertyDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

         foreach (var key in propertyDict.Keys)
         {
            if (key.Equals("Id"))
               continue;
            var val = propertyDict[key];
            if (val == null) continue;
            Java.Lang.Object javaVal = null;
            if (val is string str)
               javaVal = new Java.Lang.String(str);
            else if (val is double dbl)
               javaVal = new Java.Lang.Double(dbl);
            else if (val is int intVal)
               javaVal = new Java.Lang.Integer(intVal);
            else if (val is DateTime dt)
               javaVal = dt.ToString();
            else if (val is bool boolVal)
               javaVal = new Java.Lang.Boolean(boolVal);

            if (javaVal != null)
               docData.Add(key, javaVal);
         }

         return new HashMap(docData);
      }
   }
}
