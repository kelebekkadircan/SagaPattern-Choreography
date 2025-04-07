using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservedEvent
    {
        public Guid BuyerID { get; set; }

        public Guid OrderID { get; set; }

        public decimal TotalPrice { get; set; }
    

        public List<OrderItemMessages> OrderItemMessage { get; set; }

    }
}
