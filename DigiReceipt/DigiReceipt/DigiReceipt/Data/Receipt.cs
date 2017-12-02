﻿using System;
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
        public long IssuedOn { get; set; }

        // The image of the receipt as a byte array.
        public byte[] Image { get; set; }

        public Receipt()
        {
            IssuedOn = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Return the image of the receipt as a base64 encoded string.
        /// </summary>
        /// <returns></returns>
        public string ImageAsBase64()
        {
            return Convert.ToBase64String(Image);
        }
    }
}
