using DigiReceipt.Data;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DigiReceipt.ViewModels
{
    public class ReceiptViewModel : NotificationBase<Receipt>
    {
        public ICommand TakePhotoCommand { get; private set; }

        public ReceiptViewModel(Receipt receipt = null) : base(receipt) {
            IssuedOn = DateTime.Now;

            // Setup commands.
            TakePhotoCommand = new Command(async () => await OnTakePhoto());
        }

        public DateTime IssuedOn
        {
            get { return This.IssuedOn; }
            set { SetProperty(This.IssuedOn, value, () => This.IssuedOn = value); }
        }

        public ImageSource Image
        {
            get { return This.Image; }
            set { SetProperty(This.Image, value, () => This.Image = value); }
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
                Image = ImageSource.FromStream(() => file.GetStream());
            }
        }
    }
}
