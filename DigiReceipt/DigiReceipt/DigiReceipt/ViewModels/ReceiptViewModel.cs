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
        public ICommand SelectPhotoCommand { get; private set; }
        public ICommand SaveReceiptCommand { get; private set; }

        public ReceiptViewModel(ReceiptModel receipt = null) : base(receipt) {
            IssuedOn = DateTime.Now;

            // Setup commands.
            TakePhotoCommand = new Command(async () => await OnTakePhoto());
            SelectPhotoCommand = new Command(async () => await OnSelectPhoto());
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

        public string Price
        {
            get { return This.Receipt.Price.ToString("0.##"); }
            set {
                float price;

                if (float.TryParse(value, out price))
                    SetProperty(This.Receipt.Price, price, () => This.Receipt.Price = price);
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
