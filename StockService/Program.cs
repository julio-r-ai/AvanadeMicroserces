using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Repositories;
using StockService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<RabbitMqListener>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var rabbitListener = app.Services.GetRequiredService<RabbitMqListener>();

// ✅ Starta o listener de forma assíncrona
_ = Task.Run(async () => await rabbitListener.StartAsync());

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();