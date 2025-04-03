using Order.Application.CreateOrder;
using Order.Application.Interfaces;
using Order.Domain.Repository;
using Order.Infrastructure.MessageBroker;
using Order.Infrastructure.Repository;
using Order.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, Order.Application.Services.OrderService>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
builder.Services.AddScoped<IOrderRepository<Order.Domain.Models.Order>, OrderRepository>();
builder.Services.AddHostedService<Worker>();

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

app.Run();
