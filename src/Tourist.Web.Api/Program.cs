using Tourist.Application.Commands;
using Tourist.Application.Queries;
using Tourist.Domain;
using Tourist.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// TO DO: Use with options
var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
builder.Services.AddSingleton<DatabaseConfig>(databaseSettings);
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddTransient<IGenericRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IGenericRepository<Item>, ItemRepository>();
builder.Services.AddTransient<IGenericRepository<ShipmentLineItem>, ShipmentRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TO DO: Move to extension method
//SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
//SqlMapper.AddTypeHandler(new GuidHandler());
//SqlMapper.AddTypeHandler(new TimeSpanHandler());

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

app.UseAuthorization();

app.MapControllers();

app.Services.GetService<IDatabaseBootstrap>().Setup();

app.Run();
