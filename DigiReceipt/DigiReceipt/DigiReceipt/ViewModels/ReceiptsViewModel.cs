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

        // Collection of receipts to display to the user.
        private ObservableCollection<ReceiptViewModel> receipts = new ObservableCollection<ReceiptViewModel>();

        // Query for receipts issued after this datetime.
        private DateTime issuedAfter = DateTime.Now;

        // Determine visablility of loading message.
        private bool isLoading = false;

        // Determine if there are more receipts to load.
        private bool isFinishedLoading = false;

        public ReceiptsViewModel(ReceiptsModel receipts = null) : base(receipts) {
            OnLoadNextReceipts();

            // Setup commands.
            LoadNextReceiptsCommand = new Command(async () => await OnLoadNextReceipts());
        }
        
        public ObservableCollection<ReceiptViewModel> Receipts
        {
            get { return receipts; }
            set { SetProperty(ref receipts, value); }
        }

        public DateTime IssuedAfter
        {
            get { return issuedAfter; }
            set
            {
                if (!IssuedAfter.Date.Equals(value.Date)) {
                    issuedAfter = value;
                    RaisePropertyChanged(nameof(IssuedAfter));

                    Receipts.Clear();
                    RaisePropertyChanged(nameof(Receipts));

                    isFinishedLoading = false;
                    OnLoadNextReceipts();
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
    }
}
