using System.Text;
using Api.Caching;
using Api.CachingServices;
using Api.Data;
using Api.GlobalException;
using Api.Models;
using FastEndpoints;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(op =>
    op.UseNpgsql(builder.Configuration.GetConnectionString("DBCS")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op => op.TokenValidationParameters =new()
    {
    ValidIssuer = "Us",
    ValidAudience = "US",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSECERT").Value)),
  ValidateIssuerSigningKey = true
    }
);
builder.Services.AddSingleton<IConnectionMultiplexer>(op =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
builder.Services.AddScoped<RedisCaching>();


builder.Services.AddMemoryCache();
builder.Services.AddScoped<MemoryCaching>();

builder.Services.AddFastEndpoints();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("api/school/", (RedisCaching service)  =>
{
   
    return Results.Ok(service.GetAll());
});
app.MapPost("api/school/", (School school,AppDbContext db) =>
{
    try
    {
        db.Schools.Add(school);
        db.SaveChanges();
    return Results.Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
});
app.MapDelete("api/school/", ([FromQuery]int id, AppDbContext db) =>
{
    try
    {
        var data = db.Schools.FirstOrDefault(x => x.Id == id);
        db.Schools.Remove(data);
        db.SaveChanges();
        return Results.Ok();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        throw;
    }
});
app.MapPut("api/school/", (School school, AppDbContext db) =>
{
    try
    {
        var data = db.Schools.Find(school.Id);
        data.Name = school.Name;
        db.SaveChanges();
        return Results.Ok(data);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        throw;
    }
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseFastEndpoints();
app.UseExceptionHandler(op => { });
app.Run();
