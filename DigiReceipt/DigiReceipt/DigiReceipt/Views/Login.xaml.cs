using DigiReceipt.ViewModels;
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
        private LoginViewModel loginViewModel;

        public Login()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
        }

        /// <summary>
        /// Navigate to the next page if the user is already authenticated.
        /// </summary>
        protected override void OnAppearing()
        {
            loginViewModel.NavigateViewReceipts();
        }
    }
}
