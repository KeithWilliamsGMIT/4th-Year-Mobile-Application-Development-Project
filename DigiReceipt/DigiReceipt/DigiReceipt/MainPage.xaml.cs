using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiReceipt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
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
            await Navigation.PushAsync(new ViewReceipts());
        }
    }
}
