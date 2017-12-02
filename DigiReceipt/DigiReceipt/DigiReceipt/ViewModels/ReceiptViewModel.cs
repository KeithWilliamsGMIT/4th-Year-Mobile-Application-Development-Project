using DigiReceipt.Data;
using DigiReceipt.Models;
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
        public ICommand SaveReceiptCommand { get; private set; }

        public ReceiptViewModel(ReceiptModel receipt = null) : base(receipt) {
            IssuedOn = DateTime.Now;

            // Setup commands.
            TakePhotoCommand = new Command(async () => await OnTakePhoto());
            SaveReceiptCommand = new Command(() => OnSaveReceipt());
        }

        public DateTime IssuedOn
        {
            get { return new DateTime(This.Receipt.IssuedOn); }
            set {
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

            if (file != null)
            {
                var stream = file.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                This.Receipt.Image = bytes;
                RaisePropertyChanged(nameof(Image));
            }
        }

        /// <summary>
        /// Save the new receipt.
        /// </summary>
        private void OnSaveReceipt()
        {
            This.Save();
        }
    }
}
