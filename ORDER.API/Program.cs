using MassTransit;
using Microsoft.EntityFrameworkCore;
using ORDER.API.Models;
using ORDER.API.Models.Context;
using ORDER.API.ViewModels;
using ORDER.API.Enums;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context,_configure) =>
    {
        _configure.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

    });
});

builder.Services.AddDbContext<OrderAPIDbContext>((opt) =>
{
   opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();



app.MapPost("/create-order", async (CreateOrderVM createOrder , OrderAPIDbContext _context) =>
{
    Order order = new()
    {
        BuyerID = Guid.TryParse(createOrder.BuyerID, out  Guid result) ? result : Guid.NewGuid(),
        CreatedDate = DateTime.UtcNow,
        OrderStatus =OrderStatusEnum.Suspended,
        OrderItems = createOrder.OrderItems.Select(x => new OrderItem()
        {
            Count = x.Count,
            Price = x.Price,
            ProductID = Guid.TryParse(x.ProductId, out Guid result) ? result : Guid.NewGuid(),

        }).ToList(),
        TotalPrice = createOrder.OrderItems.Sum(x => x.Price * x.Count)


    };

    await _context.Orders.AddAsync(order);
    await _context.SaveChangesAsync();
});


app.Run();
