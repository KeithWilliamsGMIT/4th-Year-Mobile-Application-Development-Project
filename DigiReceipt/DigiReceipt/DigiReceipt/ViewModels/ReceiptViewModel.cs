using DigiReceipt.Data;
using DigiReceipt.Models;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DigiReceipt.ViewModels
{
    public class ReceiptViewModel : NotificationBase<ReceiptModel>
    {
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand SelectPhotoCommand { get; private set; }
        public ICommand SaveReceiptCommand { get; private set; }

        // Determine if a receipt is currently being saved.
        public bool isSaving = false;

        public ReceiptViewModel(ReceiptModel receipt = null) : base(receipt) {
            // Setup commands.
            TakePhotoCommand = new Command(async () => await OnTakePhoto());
            SelectPhotoCommand = new Command(async () => await OnSelectPhoto());
            SaveReceiptCommand = new Command(async () => await OnSaveReceipt());
        }

        public string ReceiptId
        {
            get { return This.Receipt.ReceiptId; }
        }

        public DateTime IssuedOn
        {
            get { return new DateTime(This.Receipt.IssuedOn); }
            set {
                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                value = value.Date + ts;

                long issuedOn = value.Ticks;
                SetProperty(This.Receipt.IssuedOn, issuedOn, () => This.Receipt.IssuedOn = issuedOn);
            }
        }

        public ImageSource Image
        {
            get {
                if (This.Receipt.Image != null)
                    return ImageSource.FromStream(() => new MemoryStream(This.Receipt.Image));

                return null;
            }
        }

        public bool HasNoImage
        {
            get { return This.Receipt.Image == null || This.Receipt.Image.Length == 0; }
        }

        public float Price
        {
            get { return This.Receipt.Price; }
            set {
                SetProperty(This.Receipt.Price, value, () => This.Receipt.Price = value);
            }
        }

        public bool IsSaving
        {
            get { return isSaving; }
            set {
                isSaving = value;
                RaisePropertyChanged(nameof(IsSaving));
            }
        }

        public bool IsOnline
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        /// <summary>
        /// Take a picture and display it to the user.
        /// </summary>
        private async Task OnTakePhoto()
        {
            // Create instance on singleton class.
            await CrossMedia.Current.Initialize();

            // Adapted from https://channel9.msdn.com/Blogs/MVP-VisualStudio-Dev/XamarinForms-taking-pictures-from-the-camera-and-from-disk-using-the-Media-plugin
            if (!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = false,
                Name = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            await UpdatePhoto(file);
        }

        /// <summary>
        /// Select a picture from the users device and display it to the user. 
        /// </summary>
        /// <returns></returns>
        private async Task OnSelectPhoto()
        {
            // Create instance on singleton class.
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());

            await UpdatePhoto(file);
        }

        /// <summary>
        /// Sets the given file as the image of the current receipt and update bindings.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task UpdatePhoto(MediaFile file)
        {
            if (file != null)
            {
                var stream = file.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                This.Receipt.Image = bytes;
                RaisePropertyChanged(nameof(Image));
                RaisePropertyChanged(nameof(HasNoImage));
            }
        }

        /// <summary>
        /// Save the new, or changed, receipt and go back to the ViewReceipts page.
        /// </summary>
        private async Task OnSaveReceipt()
        {
            RaisePropertyChanged(nameof(IsOnline));

            if (IsOnline)
            {
                IsSaving = true;

                if (This.Receipt.ReceiptId == null || This.Receipt.ReceiptId == String.Empty)
                {
                    await This.Create();
                }
                else
                {
                    await This.Update();
                }

                IsSaving = false;

                await Application.Current.MainPage.Navigation.PushAsync(new ViewReceipts());
            }
        }

        /// <summary>
        /// Delete this receipt.
        /// </summary>
        public async Task DeleteReceipt()
        {
            RaisePropertyChanged(nameof(IsOnline));

            if (IsOnline) {
                await This.Delete();
            }
        }
    }
}
