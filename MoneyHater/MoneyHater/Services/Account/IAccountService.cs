using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoneyHater.Models;

namespace MoneyHater.Services.Account
{
   public interface IAccountService
   {
      Task<bool> LoginAsync(string username, string password);
      Task<bool> SendOtpCodeAsync(string phoneNumber);
      Task<bool> VerifyOtpCodeAsync(string code);

      Task<UserModel> GetUserAsync();
   }
}
