using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiReceipt.Data
{
    public class Receipt
    {
        // The date the receipt was issued on.
        public DateTime IssuedOn { get; set; }

        // The image of the receipt.
        public ImageSource Image { get; set; }

        public Receipt()
        {
            IssuedOn = DateTime.Now;
        }
    }
}
