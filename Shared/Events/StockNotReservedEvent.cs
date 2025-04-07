using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockNotReservedEvent
    {
        public Guid OrderID { get; set; }

        public Guid BuyerID { get; set; }

        public string Message { get; set; }
    }
}
