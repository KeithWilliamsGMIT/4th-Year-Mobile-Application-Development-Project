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
        /// <summary>
        /// Get a collection of receipts issued after the given date from the web service.
        /// </summary>
        public async Task<List<Receipt>> Retrieve(long lastTimestamp)
        {
            return await Service.Retrieve(lastTimestamp);
        }
    }
}
