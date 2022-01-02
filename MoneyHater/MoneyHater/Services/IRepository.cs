using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyHater.Services
{
   public interface IIdentifiable
   {
      string Id { get; set; }
   }

   public interface IRepository<T> where T : IIdentifiable
   {
      string previousPath { get; set; }
      string DocumentPath { get; }
      string Path { get; set; }

      Task<T> Get(string id);
      Task<IList<T>> GetAll();
      Task<string> Save(T item);
      Task<bool> Delete(T item);
   }
}
