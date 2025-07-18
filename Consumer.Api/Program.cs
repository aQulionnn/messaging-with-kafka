using Consumer.Api.Data;
using Consumer.Api.Handlers;
using Consumer.Api.Repositories;
using Consumer.Api.Settings;
using Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();
    
    x.AddConsumer<PlaceOrderHandler>();
    
    x.AddRider(rider =>
    {
        rider.AddConsumer<PlaceOrderHandler>();
        
        rider.UsingKafka((context, k) =>
        {
            k.Host("kafka:9092");
            
            k.TopicEndpoint<PlaceOrder>("PlaceOrder", "group-id", e =>
            {
                e.ConfigureConsumer<PlaceOrderHandler>(context);
            });
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://0.0.0.0:8080");