using MoneyHater.Models;
using MoneyHater.Services;
using MoneyHater.Services.Account;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

      public static List<CategoryModel> categories;
      public static List<CurrencyModel> currencies;
      public static List<string> icons;
      public static List<AnotherUserModel> usersPublicInfo;
      public static UserModel userLoggedInfo;
      public static void Init()
      {
         auth = DependencyService.Resolve<IAuth>();
         walletService = new WalletService();
         categoryService = new CategoryService();
         currencyRepo = DependencyService.Resolve<IRepository<CurrencyModel>>();
         categoryRepo = DependencyService.Resolve<IRepository<CategoryModel>>();
         userRepo = DependencyService.Resolve<IRepository<UserModel>>();
         iconRepo = DependencyService.Resolve<IRepository<IconModel>>();
      }

      public static void clear()
      {
         if (categories != null) { categories.Clear(); }
         if (icons != null) { icons.Clear(); }
         if (currencies != null) { currencies.Clear(); }
         if (usersPublicInfo != null) { usersPublicInfo.Clear(); }
         if (walletService != null) { walletService = new WalletService(); }
         userLoggedInfo = null;
      }

      public static async Task LoadDataLoggeduser()
      {
         categories = new List<CategoryModel>() { };
         usersPublicInfo = new List<AnotherUserModel>() { };

         var loadCategories = LoadCategories();
         var loadCurrencies = LoadCurrencies();
         var loadIcons = LoadIcons();

         var loadUsersPublicInfo = userRepo.GetAll();
         var loadUserLoggedInfo = auth.GetUserAsync();

         //Task taskReturned = Task.WhenAll(new Task[] { loadIcons, loadCurrencies,
         //   loadCategories, loadUsersPublicInfo, loadUserLoggedInfo});
         Task taskReturned = Task.WhenAll(new Task[] { loadCategories, loadCurrencies, loadIcons, loadUsersPublicInfo });
         try
         {
            await taskReturned;
            await loadCategories;
            await loadCurrencies;
            await loadIcons;
            usersPublicInfo = ((await loadUsersPublicInfo) as List<UserModel>).Cast<AnotherUserModel>().ToList();
            userLoggedInfo = await loadUserLoggedInfo;
            await walletService.LoadWallets();
         }
         catch
         {
            await App.Current.MainPage.DisplayAlert("Alert", taskReturned.Exception.Message, "Ok");
         }
      }

      public static async Task LoadCurrencies()
      {
         var json = string.Empty;

         if (!Barrel.Current.IsExpired(nameof(currencies)))
            json = Barrel.Current.Get<string>(nameof(currencies));

         try
         {
            if (string.IsNullOrWhiteSpace(json))
            {
               var loadCurrencies = currencyRepo.GetAll();
               currencies = (await loadCurrencies) as List<CurrencyModel>;
               json = JsonConvert.SerializeObject(currencies);
               Barrel.Current.Add(nameof(currencies), json, TimeSpan.FromDays(10));
            }
            else
            {
               currencies = JsonConvert.DeserializeObject<List<CurrencyModel>>(json);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Unable to get information from server {ex}");
            throw ex;
         }

      }
      public static async Task LoadCategories()
      {
         var json = string.Empty;
         if (!Barrel.Current.IsExpired(nameof(categories)))
            json = Barrel.Current.Get<string>(nameof(categories));

         try
         {
            if (string.IsNullOrWhiteSpace(json))
            {
               var loadCategories = categoryService.LoadCategories();
               categories = await loadCategories;
               json = JsonConvert.SerializeObject(categories);
               Barrel.Current.Add(nameof(categories), json, TimeSpan.FromDays(10));
            }
            else
            {
               categories = JsonConvert.DeserializeObject<List<CategoryModel>>(json);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Unable to get information from server {ex}");
            throw ex;
         }
      }
      public static async Task LoadIcons()
      {
         var json = string.Empty;
         if (!Barrel.Current.IsExpired(nameof(icons)))
            json = Barrel.Current.Get<string>(nameof(icons));

         try
         {
            if (string.IsNullOrWhiteSpace(json))
            {
               var loadIcons = iconRepo.Get("YnKsVORKhhXO4Wln2C8M");
               icons = (await loadIcons).Icons;
               json = JsonConvert.SerializeObject(icons);
               Barrel.Current.Add(nameof(icons), json, TimeSpan.FromDays(10));
            }
            else
            {
               icons = JsonConvert.DeserializeObject<List<string>>(json);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Unable to get information from server {ex}");
            throw ex;
         }
      }


      public static async Task UpdateProfile(UserModel userModel)
      {
         userModel.Id = userLoggedInfo.Id;
         userModel.Password = userLoggedInfo.Password;
         if (userLoggedInfo.PremiumStatus != userModel.PremiumStatus)
         {
            if (userLoggedInfo.PremiumStatus)
            {
               userModel.PremiumTo = (userLoggedInfo.PremiumTo < DateTime.Now ? DateTime.Now : userLoggedInfo.PremiumTo).AddDays(365);
            }
            else
            {
               userModel.PremiumTo = DateTime.Now;
            }
         }
         userLoggedInfo = userModel;
         await userRepo.Save(userModel, userLoggedInfo.Id);
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
