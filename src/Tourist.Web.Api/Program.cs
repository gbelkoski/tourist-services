using Microsoft.AspNetCore.Authentication;
using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;
using Tourist.Infrastructure;
using Tourist.Web.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// configure basic authentication 
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
builder.Services.AddSingleton<DatabaseConfig>(databaseSettings);
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddTransient<IGenericRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IGenericRepository<Item>, ItemRepository>();
builder.Services.AddTransient<IGenericRepository<ShipmentLineItem>, ShipmentRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.GetService<IDatabaseBootstrap>().Setup();

app.Run();
