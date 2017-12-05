using DigiReceipt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiReceipt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewReceipts : ContentPage
    {
        public ViewReceipts()
        {
            InitializeComponent();
            BindingContext = new ReceiptsViewModel();
        }

        /// <summary>
        /// Navigate back to the Login page when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnLogout(object sender, EventArgs e)
        {
            await AuthenticationManager.DefaultAuthenticationManager.CurrentClient.LogoutAsync();
            AuthenticationManager.DefaultAuthenticationManager.CurrentUser = null;
            AuthenticationManager.DefaultAuthenticationManager.IsAuthenticated = false;
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Navigate to the AddReceipt page when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnAddReceipt(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReceipt());
        }

        /// <summary>
        /// Navigate to the ViewReceipt page for the given item when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ReceiptViewModel;

            if (item != null) {
                await Navigation.PushAsync(new ViewReceipt(item));
            }
        }
    }
}