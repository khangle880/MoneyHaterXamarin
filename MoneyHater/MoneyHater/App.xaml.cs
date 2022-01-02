using MoneyHater.Helpers;
using MonkeyCache.FileStore;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyHater
{
   //static public fsApp;

   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         TheTheme.SetTheme();

         FbApp.Init();
         Barrel.ApplicationId = AppInfo.PackageName;

         MainPage = new AppShell();
      }

      void InitializeFirebase()
      {
         //var app = FirebaseApp
      }

      protected override void OnStart()
      {
         OnResume();
      }

      protected override void OnSleep()
      {
         TheTheme.SetTheme();
         RequestedThemeChanged -= App_RequestedThemeChanged;
      }

      protected override void OnResume()
      {
         TheTheme.SetTheme();
         RequestedThemeChanged += App_RequestedThemeChanged;
      }

      private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
      {
         MainThread.BeginInvokeOnMainThread(() =>
         {
            TheTheme.SetTheme();
         });
      }
   }
}
