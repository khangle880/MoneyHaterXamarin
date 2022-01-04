using MoneyHater.Helpers;
using MoneyHater.Views;
using MoneyHater.Views.PickupPage;
using Xamarin.Forms;

namespace MoneyHater
{
   public partial class AppShell : Xamarin.Forms.Shell
   {
      public AppShell()
      {
         InitializeComponent();

         Routing.RegisterRoute(nameof(RegistrationPage),
             typeof(RegistrationPage));
         Routing.RegisterRoute(nameof(PickupUserPage),
             typeof(PickupUserPage));
         Routing.RegisterRoute(nameof(PickupCurrencyPage),
             typeof(PickupCurrencyPage));
         Routing.RegisterRoute(nameof(PickupCategoryPage),
             typeof(PickupCategoryPage));

         Shell.SetTabBarIsVisible(this, false);
         //Routing.RegisterRoute(nameof(TransactionPage),
         //            typeof(TransactionPage));
         //Routing.RegisterRoute(nameof(LoginPage),
         //            typeof(LoginPage));

      }

   }
}