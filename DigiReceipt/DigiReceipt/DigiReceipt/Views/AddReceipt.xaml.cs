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
    public partial class AddReceipt : ContentPage
    {
        public AddReceipt() : this(new ReceiptViewModel()) { }

        public AddReceipt(ReceiptViewModel receiptViewModel)
        {
            InitializeComponent();
            BindingContext = receiptViewModel;
        }

        /// <summary>
        /// Navigate back to the last page when the event is fired. This will be either the ViewReceipts or ViewReceipt page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}