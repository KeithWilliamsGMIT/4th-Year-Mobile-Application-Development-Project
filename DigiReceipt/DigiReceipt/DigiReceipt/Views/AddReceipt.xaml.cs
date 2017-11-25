using Plugin.Media;
using Plugin.Media.Abstractions;
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
        public AddReceipt()
        {
            InitializeComponent();

            CrossMedia.Current.Initialize();
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

        /// <summary>
        /// Take a picture and display it to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTakePicture(object sender, EventArgs e)
        {
            // Adapted from https://channel9.msdn.com/Blogs/MVP-VisualStudio-Dev/XamarinForms-taking-pictures-from-the-camera-and-from-disk-using-the-Media-plugin
            if (!CrossMedia.Current.IsCameraAvailable && !CrossMedia.Current.IsTakePhotoSupported)
            {
                // Display failure message.
                await DisplayAlert("No Camera!", "Unable to take a photo", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = false,
                Name = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            if (file != null) {
                imgReceiptPhoto.Source = ImageSource.FromStream(() => file.GetStream());
            }
        }
    }
}