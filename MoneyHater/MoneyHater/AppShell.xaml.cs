using MoneyHater.Helpers;
using MoneyHater.ViewModels.Transaction;
using MoneyHater.Views;
using MoneyHater.Views.Account;
using MoneyHater.Views.PickupPage;
using MoneyHater.Views.Planning;
using MoneyHater.Views.Transaction;
using Xamarin.Forms;

namespace MoneyHater
{
   public partial class AppShell : Xamarin.Forms.Shell
   {
      public AppShell()
      {
         InitializeComponent();

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
         Routing.RegisterRoute(nameof(UpdateProfilePage),
             typeof(UpdateProfilePage));
         Routing.RegisterRoute(nameof(CategoryPage),
             typeof(CategoryPage));
         Routing.RegisterRoute(nameof(IconPage),
             typeof(IconPage));
         Routing.RegisterRoute(nameof(ManageWalletPage),
             typeof(ManageWalletPage));
         Routing.RegisterRoute(nameof(BudgetPage),
             typeof(BudgetPage));
         Routing.RegisterRoute(nameof(EventPage),
             typeof(EventPage));
         Routing.RegisterRoute(nameof(TransactionModelPage),
             typeof(TransactionModelPage));
         Routing.RegisterRoute(nameof(RecurringTransactionPage),
             typeof(RecurringTransactionPage));
         Routing.RegisterRoute(nameof(WalletDetailPage),
             typeof(WalletDetailPage));
         Routing.RegisterRoute(nameof(AddWalletPage),
             typeof(AddWalletPage));


         //Routing.RegisterRoute(nameof(TransactionPage),
         //            typeof(TransactionPage));
         //Routing.RegisterRoute(nameof(LoginPage),
         //            typeof(LoginPage));

      }

   }
}