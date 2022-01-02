using MoneyHater.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyHater.Models
{
   public class BudgetModel : IIdentifiable
   {
      public string Id { get; set; }
      public double Consume { get; set; }
      public string CategoryId { get; set; }
      public CategoryModel CategoryModel { get; set; }
      public DateTime From { get; set; }
      public double GoalValue { get; set; }
      public bool Repeatable { get; set; }
      public bool State { get; set; }
      public DateTime To { get; set; }
   }
}
