using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DigiReceipt.ViewModels
{
    public class LoginViewModel : NotificationBase
    {
        public ICommand GoogleLoginCommand { get; private set; }
        public ICommand MicrosoftLoginCommand { get; private set; }

        // Determine if the user is currently logging in.
        private bool isLoggingIn = false;

        public LoginViewModel() {
            // Setup commands.
            GoogleLoginCommand = new Command(() => OnLoginWithGoogle());
            MicrosoftLoginCommand = new Command(() => OnLoginWithMicrosoft());
        }

        public bool IsLoggingIn
        {
            get { return isLoggingIn; }
            set {
                isLoggingIn = value;
                RaisePropertyChanged(nameof(IsLoggingIn));
            }
        }

        /// <summary>
        /// Authenticate the user using Google.
        /// </summary>
        private void OnLoginWithGoogle()
        {
            LoginUser(MobileServiceAuthenticationProvider.Google);
        }

        /// <summary>
        /// Authenticate the user using Microsoft.
        /// </summary>
        private void OnLoginWithMicrosoft()
        {
            LoginUser(MobileServiceAuthenticationProvider.MicrosoftAccount);
        }

        /// <summary>
        /// Attempt login and navigate to the next page.
        /// </summary>
        /// <param name="provider"></param>
        private async void LoginUser(MobileServiceAuthenticationProvider provider)
        {
            IsLoggingIn = true;

            if (App.Authenticator != null)
                AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated = await App.Authenticator.Authenticate(provider);

            NavigateViewReceipts();
        }

        /// <summary>
        /// Navigate to the ViewReceipts page if the user is authenticated.
        /// </summary>
        public async void NavigateViewReceipts()
        {
            IsLoggingIn = false;

            if (AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated)
                await Application.Current.MainPage.Navigation.PushAsync(new ViewReceipts());
        }
    }
}
