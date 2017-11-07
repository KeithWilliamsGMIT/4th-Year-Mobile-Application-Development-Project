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
        /// Authenticate the user and attempt to navigate to the next page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnLogin(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated = await App.Authenticator.Authenticate();

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
