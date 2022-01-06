using MoneyHater.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyHater.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SplashPage : ContentPage
   {
      bool isFirstAppeared = false;
      public SplashPage()
      {
         InitializeComponent();
      }
      protected override void OnAppearing()
      {
         CheckWhetherTheUserIsSignedIn();
         base.OnAppearing();
      }


      private async void CheckWhetherTheUserIsSignedIn()
      {
         if (FirebaseService.auth.isSigned())
         {
            await FirebaseService.LoadDataLoggeduser();
            Application.Current.MainPage = new AppShell();
         }
         else
         {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
         }
      }

   }
}