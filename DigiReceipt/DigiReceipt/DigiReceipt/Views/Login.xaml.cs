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
        // Track whether the user has authenticated.
        bool authenticated = false;

        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Navigate to the ViewReceipts page when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnLogin(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();
            
            if (authenticated == true)
                await Navigation.PushAsync(new ViewReceipts());
        }
    }
}
