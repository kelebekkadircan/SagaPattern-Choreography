using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
   public static class RabbitMQSettings
    {
        public static string Stock_OrderCreatedEvent = "stock-order-created-event-queue";
        public static string Payment_StockReservedEvent = "payment-stock-reserved-event-queue";
        public static string Order_StockNotReservedEvent = "order-stock-not-reserved-event-queue";
    }
}
