using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiReceipt
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigate to the next page if the user is already authenticated.
        /// </summary>
        protected override void OnAppearing()
        {
            NavigateViewReceipts();
        }

        /// <summary>
        /// Authenticate the user using Google.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnLoginWithGoogle(object sender, EventArgs e)
        {
            LoginUser(MobileServiceAuthenticationProvider.Google);
        }

        /// <summary>
        /// Authenticate the user using Microsoft.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnLoginWithMicrosoft(object sender, EventArgs e)
        {
            LoginUser(MobileServiceAuthenticationProvider.MicrosoftAccount);
        }

        /// <summary>
        /// Attempt login and navigate to the next page.
        /// </summary>
        /// <param name="provider"></param>
        private async void LoginUser(MobileServiceAuthenticationProvider provider)
        {
            if (App.Authenticator != null)
                AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated = await App.Authenticator.Authenticate(provider);

            if (AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated)
                NavigateViewReceipts();
        }

        /// <summary>
        /// Navigate to the ViewReceipts page.
        /// </summary>
        private async void NavigateViewReceipts()
        {
            if (AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated)
                await Navigation.PushAsync(new ViewReceipts());
        }
    }
}
