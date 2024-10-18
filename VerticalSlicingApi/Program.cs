using Microsoft.EntityFrameworkCore;
using VerticalSlicingApi.CustomRoute;
using VerticalSlicingApi.Data;
using VerticalSlicingApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));
builder.Services.AddMediatR(op => op.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ProductService>();
builder.Services.Configure<RouteOptions>(op=>op.ConstraintMap.Add("Yousef",typeof(Yousef)));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
