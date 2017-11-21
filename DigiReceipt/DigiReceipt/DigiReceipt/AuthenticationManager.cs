using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiReceipt
{
    /// <summary>
    /// A singleton that handles the authentication state for the application.
    /// </summary>
    public class AuthenticationManager
    {
        private static AuthenticationManager defaultInstance = new AuthenticationManager();
        private MobileServiceClient client;

        // Define an authenticated user.
        private MobileServiceUser user;

        // Track whether the user has authenticated.
        private bool isAuthenticated;
        private const String ApplicationUrl= "https://digireceipt.azurewebsites.net";

        private AuthenticationManager()
        {
            this.client = new MobileServiceClient(ApplicationUrl);
            this.isAuthenticated = false;
        }

        public static AuthenticationManager DefaultAuthenticationManager
        {
            get { return defaultInstance; }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }

        public MobileServiceUser CurrentUser
        {
            get { return user; }
            set { user = value; }
        }
    }
}
