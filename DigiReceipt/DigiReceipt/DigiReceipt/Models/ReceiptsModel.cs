using DigiReceipt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiReceipt.Models
{
    public class ReceiptsModel
    {
        public List<Receipt> Receipts { get; set; }

        /// <summary>
        /// Explicit default constructor for ReceiptsModel class.
        /// </summary>
        public ReceiptsModel()
        {
            Receipts = new List<Receipt>();
        }

        /// <summary>
        /// Get a collection of receipts from the web service.
        /// </summary>
        public async Task Retrieve()
        {
            List<Receipt> receipts = await Service.Retrieve();
            Receipts.AddRange(receipts);
        }
    }
}
