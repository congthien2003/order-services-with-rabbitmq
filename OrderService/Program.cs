using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Application.Commands;
using Order.Application.Commands.Repositories;
using Order.Application.Queries;
using Order.Infrastructure;
using Order.Infrastructure.DataContext;
using Order.Infrastructure.MessageBroker;
using Order.Infrastructure.Repository;
using Order.Worker;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();

builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();

builder.Services.AddDbContext<CommandDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommandConnection")));

builder.Services.AddDbContext<QueryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("QueryConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<OrderCreatedConsumer>();

    busConfigurator.UsingRabbitMq((context, config) =>
    {

        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        config.ConfigureEndpoints(context);

        config.ReceiveEndpoint("order", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });


    });

});

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
