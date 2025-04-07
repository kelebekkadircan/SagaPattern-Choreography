using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events;
using STOCK.API.Context;
using STOCK.API.Entities;

namespace STOCK.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly StocAPIDbContext _context;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(StocAPIDbContext context, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderItems = context.Message.OrderItemMessages;
            var stocks = await _context.Stocks.ToListAsync(); // ya da .Where() ile filtreleyebilirsin

            List<bool> stockResult = new();

            foreach (var item in orderItems)
            {
                Stock? stock = stocks.Find(x => x.ProductID == item.ProductID);

                if (stock == null || stock.Count < item.Count)
                {
                    stockResult.Add(false);
                }
                else
                {
                    stock.Count -= item.Count; // stoktan düş
                    stockResult.Add(true);
                }
            }

            // Eğer tüm stoklar uygunsa güncelle
            if (stockResult.TrueForAll(x => x.Equals(true)))
            {
              var sendEndpoint =  await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEvent}"));

                StockReservedEvent stockReservedEvent = new()
                {
                    BuyerID = context.Message.BuyerID,
                    OrderID = context.Message.OrderID,
                    TotalPrice = context.Message.TotalPrice,
                    OrderItemMessage = context.Message.OrderItemMessages

                };

                await sendEndpoint.Send(stockReservedEvent);

                await _context.SaveChangesAsync();
            }
            else
            {
                // Loglama, hata fırlatma, event publish etme gibi işlemler yapılabilir
                Console.WriteLine("Yetersiz stok var.");
                StockNotReservedEvent stockNotReservedEvent = new()
                {
                    BuyerID = context.Message.BuyerID,
                    OrderID = context.Message.OrderID,
                   Message = "Stock Miktarı Yetersiz",
                };

                await _publishEndpoint.Publish(stockNotReservedEvent);

            }
        }
    }
}
