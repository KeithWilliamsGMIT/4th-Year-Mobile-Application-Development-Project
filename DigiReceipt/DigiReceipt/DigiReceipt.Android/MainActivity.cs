using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace DigiReceipt.Droid
{
    [Activity(Label = "DigiReceipt", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        public async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            bool success = false;

            try
            {
                // Login with the given provider using a server-managed flow.
                if (AuthenticationManager.DefaultAuthenticationManager.CurrentUser == null)
                {
                    AuthenticationManager.DefaultAuthenticationManager.CurrentUser = await AuthenticationManager.DefaultAuthenticationManager.CurrentClient.LoginAsync(this, provider, "digireceipt");

                    if (AuthenticationManager.DefaultAuthenticationManager.CurrentUser != null)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Display the success or failure message.
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetMessage(ex.Message);
                builder.SetTitle("Sign-in result");
                builder.Create().Show();
            }

            return success;
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            App.Init((IAuthenticate)this);
            LoadApplication(new App());
        }
    }
}
