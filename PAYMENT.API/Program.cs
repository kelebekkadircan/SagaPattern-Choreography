using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(config =>
{
   
    config.UsingRabbitMq((context,_configure) =>
    {
        _configure.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

    });
});


var app = builder.Build();




app.Run();
