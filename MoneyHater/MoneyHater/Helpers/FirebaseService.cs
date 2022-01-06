using MoneyHater.Models;
using MoneyHater.Services;
using MoneyHater.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyHater.Helpers
{
   public static class FirebaseService
   {
      //static public string WebApiKey = "AIzaSyDPzGKskOasSnpYCwmKJq6NDRkR9JbUNNU";
      //static public FirebaseAuthProvider FbAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
      public static IAuth auth { get; set; }
      public static IRepository<UserModel> userRepo { get; set; }
      public static IRepository<CurrencyModel> currencyRepo { get; set; }
      public static IRepository<CategoryModel> categoryRepo { get; set; }
      public static IRepository<IconModel> iconRepo { get; set; }
      public static WalletService walletService { get; set; }
      public static CategoryService categoryService { get; set; }
      public static IRepository<WalletModel> walletRepo { get; set; }

      public static List<CategoryModel> categories;
      public static List<CurrencyModel> currencies;
      public static List<string> icons;
      public static List<AnotherUserModel> usersPublicInfo;
      public static List<WalletModel> wallets;
      public static UserModel userInfo;
      public static void Init()
      {
         auth = DependencyService.Resolve<IAuth>();
         walletService = new WalletService();
         categoryService = new CategoryService();
         currencyRepo = DependencyService.Resolve<IRepository<CurrencyModel>>();
         categoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
         userRepo = DependencyService.Resolve<IRepository<UserModel>>();
         iconRepo = DependencyService.Resolve<IRepository<IconModel>>();
         walletRepo = DependencyService.Resolve<IRepository<WalletModel>>();
      }

      public static void clear()
      {
         if (categories != null) { categories.Clear(); }
         if (icons != null) { icons.Clear(); }
         if (currencies != null) { currencies.Clear(); }
         if (usersPublicInfo != null) { usersPublicInfo.Clear(); }
         if (wallets != null) { wallets.Clear(); }
         userInfo = null;
      }

      public static async Task LoadDataLoggeduser()
      {

         //var loadCurrencies = currencyRepo.GetAll();
         //var loadCategories = categoryService.LoadCategories();
         //var loadUsersPublicInfo = userRepo.GetAll();
         //var loadIcons = iconRepo.Get("YnKsVORKhhXO4Wln2C8M");
         var loadUserLoggedInfo = auth.GetUserAsync();

         //Task taskReturned = Task.WhenAll(new Task[] { loadIcons, loadCurrencies,
         //   loadCategories, loadUsersPublicInfo, loadUserLoggedInfo});
         Task taskReturned = Task.WhenAll(new Task[] { });
         try
         {
            await taskReturned;
            //icons = (await loadIcons).Icons;
            //currencies = (await loadCurrencies) as List<CurrencyModel>;
            //categories = await loadCategories;
            //usersPublicInfo = ((await loadUsersPublicInfo) as List<UserModel>).Cast<AnotherUserModel>().ToList();
            //userInfo = await loadUserLoggedInfo;
            await walletService.LoadWallets();
         }
         catch
         {
            await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
         }
      }
   }

   public interface IAuth
   {
      string Uid { get; set; }
      Task<string> LoginWithEAndP(string email, string password);
      Task<string> SignUpWithEAndP(string email, string password);
      Task<UserModel> GetUserAsync();

      bool SignOut();

      bool isSigned();
   }
}
