using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Infrastructure;
using Tourist.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGenericRepository<Customer>,GenericRepository<Customer>>();
builder.Services.AddTransient<IGenericRepository<ShipmentLineItem>,GenericRepository<ShipmentLineItem>>();
builder.Host.ConfigureServices((host, services) =>
{
    services
        .Init()
        .AddCommandHandlers()
        .AddQueryHandlers()
        .AddInMemoryCommandDispatcher()
        .AddInMemoryQueryDispatcher()
        .Build();
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
