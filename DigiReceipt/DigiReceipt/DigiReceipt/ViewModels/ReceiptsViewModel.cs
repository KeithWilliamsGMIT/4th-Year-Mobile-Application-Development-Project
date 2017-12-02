using DigiReceipt.Data;
using DigiReceipt.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DigiReceipt.ViewModels
{
    public class ReceiptsViewModel : NotificationBase<ReceiptsModel>
    {
        // Collection of receipts to display to the user.
        private ObservableCollection<ReceiptViewModel> receipts = new ObservableCollection<ReceiptViewModel>();

        // Selected receipt from the collection.
        private ReceiptViewModel selectedItem;

        public ReceiptsViewModel(ReceiptsModel receipts = null) : base(receipts) {
            LoadNextReceipts();
        }
        
        public ObservableCollection<ReceiptViewModel> Receipts
        {
            get { return receipts; }
            set { SetProperty(ref receipts, value); }
        }
        
        public ReceiptViewModel SelectedItem
        {
            get { return selectedItem; }
            set {
                selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        /// <summary>
        /// Load the next batch of receipts and append them to the list.
        /// </summary>
        /// <returns></returns>
        private async Task LoadNextReceipts()
        {
            await This.Retrieve();

            foreach (var receipt in This.Receipts)
            {
                ReceiptModel model = new ReceiptModel(receipt);
                ReceiptViewModel viewModel = new ReceiptViewModel(model);
                Receipts.Add(viewModel);
            }
        }
    }
}
