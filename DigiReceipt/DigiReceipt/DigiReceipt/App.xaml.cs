using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DigiReceipt
{
    public partial class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }

        public static MobileServiceClient MobileService = new MobileServiceClient("https://digireceipt.azurewebsites.net");

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.Gold,
                BarTextColor = Color.White
            };
        }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
