using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configura DbContext
builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();