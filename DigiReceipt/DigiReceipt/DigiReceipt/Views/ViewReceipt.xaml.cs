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
    public partial class ViewReceipt : ContentPage
    {
        public ViewReceipt(ReceiptViewModel receiptViewModel)
        {
            InitializeComponent();
            BindingContext = receiptViewModel;
        }

        /// <summary>
        /// Navigate back to the ViewReceipts page when the event is fired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnBack(object sender, EventArgs e)
        {
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
    }
}