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
        public ICommand LoadNextReceiptsCommand { get; private set; }
        public ICommand DeleteReceiptCommand { get; private set; }
        public ICommand EditReceiptCommand { get; private set; }

        // Collection of receipts to display to the user.
        private ObservableCollection<ReceiptViewModel> receipts = new ObservableCollection<ReceiptViewModel>();

        // Selected receipt in the list.
        private ReceiptViewModel selectedItem;

        // Query for receipts issued after this datetime.
        private DateTime issuedAfter = DateTime.Now;

        // Determine visablility of loading message.
        private bool isLoading = false;

        // Determine if there are more receipts to load.
        private bool isFinishedLoading = false;

        public ReceiptsViewModel(ReceiptsModel receipts = null) : base(receipts) {
            RefreshReceipts();

            // Setup commands.
            LoadNextReceiptsCommand = new Command(async () => await OnLoadNextReceipts());
            DeleteReceiptCommand = new Command(async (receipt) => await OnDeleteReceipt(receipt));
            EditReceiptCommand = new Command(async (receipt) => await OnEditReceipt(receipt));
        }
        
        public ObservableCollection<ReceiptViewModel> Receipts
        {
            get { return receipts; }
            set { SetProperty(ref receipts, value); }
        }

        public ReceiptViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public bool HasNoReceipts
        {
            get { return !IsLoading && Receipts.Count == 0; }
        }

        public DateTime IssuedAfter
        {
            get { return issuedAfter; }
            set
            {
                if (!IssuedAfter.Date.Equals(value.Date)) {
                    issuedAfter = value;
                    RaisePropertyChanged(nameof(IssuedAfter));

                    RefreshReceipts();
                }
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
                RaisePropertyChanged(nameof(HasNoReceipts));
            }
        }

        /// <summary>
        /// Load the next batch of receipts and append them to the list.
        /// </summary>
        /// <returns></returns>
        private async Task OnLoadNextReceipts()
        {
            if (!IsLoading && !isFinishedLoading) {
                IsLoading = true;

                long lastTimestamp = IssuedAfter.AddDays(1).Ticks;

                if (Receipts.Count > 0)
                {
                    lastTimestamp = Receipts[Receipts.Count - 1].IssuedOn.Ticks;
                }

                List<Receipt> newReceipts = await This.Retrieve(lastTimestamp);

                foreach (var receipt in newReceipts)
                {
                    ReceiptModel model = new ReceiptModel(receipt);
                    ReceiptViewModel viewModel = new ReceiptViewModel(model);
                    Receipts.Add(viewModel);
                }

                if (newReceipts.Count < 5)
                {
                    isFinishedLoading = true;
                }

                IsLoading = false;
            }
        }

        /// <summary>
        /// Delete the given receipt and remove it from the list.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private async Task OnDeleteReceipt(object obj)
        {
            ReceiptViewModel receipt = (ReceiptViewModel)obj;
            await receipt.DeleteReceipt();
            SelectedItem = null;
            Receipts.Remove(receipt);
            RaisePropertyChanged(nameof(Receipts));
        }

        /// <summary>
        /// Navigate to a page where the user can edit the receipt.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private async Task OnEditReceipt(object obj)
        {
            ReceiptViewModel receipt = (ReceiptViewModel)obj;
            await Application.Current.MainPage.Navigation.PushAsync(new AddReceipt(receipt));
        }

        /// <summary>
        /// Refresh the list of receipts.
        /// </summary>
        private void RefreshReceipts()
        {
            Receipts.Remove(SelectedItem);
            SelectedItem = null;

            Receipts.Clear();
            RaisePropertyChanged(nameof(Receipts));

            isFinishedLoading = false;
            OnLoadNextReceipts();
        }
    }
}
