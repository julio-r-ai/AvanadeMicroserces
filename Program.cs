using Microsoft.EntityFrameworkCore;
using SalesService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();