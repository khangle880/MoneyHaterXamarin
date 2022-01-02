using MoneyHater.Helpers;
using MoneyHater.Views;
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
         Shell.SetTabBarIsVisible(this, false);
         //Routing.RegisterRoute(nameof(TransactionPage),
         //            typeof(TransactionPage));
         //Routing.RegisterRoute(nameof(LoginPage),
         //            typeof(LoginPage));

      }

   }
}