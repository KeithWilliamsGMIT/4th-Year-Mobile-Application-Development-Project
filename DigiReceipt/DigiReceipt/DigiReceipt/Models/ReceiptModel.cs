using DigiReceipt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiReceipt.Models
{
    public class ReceiptModel
    {
        public Receipt Receipt { get; set; }

        /// <summary>
        /// Explicit default constructor for ReceiptModel class.
        /// </summary>
        public ReceiptModel()
        {
            Receipt = new Receipt();
        }

        /// <summary>
        /// Fully parameterised constructor for ReceiptModel class.
        /// </summary>
        public ReceiptModel(Receipt receipt)
        {
            Receipt = receipt;
        }

        /// <summary>
        /// Finalise receipt and save to the web service.
        /// </summary>
        public async void Save()
        {
            await Service.Write(Receipt);
        }
    }
}
