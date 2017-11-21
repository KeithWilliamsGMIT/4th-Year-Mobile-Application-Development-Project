using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DigiReceipt.UWP
{
    public sealed partial class MainPage : IAuthenticate
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Initialize the authenticator before loading the app.
            DigiReceipt.App.Init(this);

            LoadApplication(new DigiReceipt.App());
        }

        public async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            var success = false;

            try
            {
                // Login with the given provider using a server-managed flow.
                if (AuthenticationManager.DefaultAuthenticationManager.CurrentUser == null)
                {
                    AuthenticationManager.DefaultAuthenticationManager.CurrentUser = await AuthenticationManager.DefaultAuthenticationManager.CurrentClient.LoginAsync(provider, "digireceipt");

                    if (AuthenticationManager.DefaultAuthenticationManager.CurrentUser != null)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Display failure message.
                await new MessageDialog(ex.Message, "Failed to login!").ShowAsync();
            }

            return success;
        }
    }
}
