using MoneyHater.Helpers;
using MoneyHater.ViewModels.Transaction;
using MoneyHater.Views;
using MoneyHater.Views.PickupPage;
using MoneyHater.Views.Transaction;
using Xamarin.Forms;

namespace MoneyHater
{
   public partial class AppShell : Xamarin.Forms.Shell
   {
      public AppShell()
      {
         InitializeComponent();

         Shell.SetTabBarIsVisible(this, false);

         Routing.RegisterRoute(nameof(AddTransactionPage),
             typeof(AddTransactionPage));
         Routing.RegisterRoute(nameof(PickupUserPage),
             typeof(PickupUserPage));
         Routing.RegisterRoute(nameof(PickupCurrencyPage),
             typeof(PickupCurrencyPage));
         Routing.RegisterRoute(nameof(PickupCategoryPage),
             typeof(PickupCategoryPage));
         Routing.RegisterRoute(nameof(TransactionDetailPage),
             typeof(TransactionDetailPage));


         //Routing.RegisterRoute(nameof(TransactionPage),
         //            typeof(TransactionPage));
         //Routing.RegisterRoute(nameof(LoginPage),
         //            typeof(LoginPage));

      }

   }
}