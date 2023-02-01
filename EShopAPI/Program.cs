using EshopApi.Models;
using EShopAPI.Contracts;
using EShopAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<EshopApi_DBContext>
    (option => option.UseSqlServer
    (@"Server=.;Database=EShopDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer= true,
            ValidateAudience= false,
            ValidateLifetime= true,
            ValidateIssuerSigningKey= true,
            ValidIssuer = "http://localhost:5162",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyEShopSecurityKey"))
        };
    });
;


builder.Services.AddCors(option =>
{
    option.AddPolicy("EnableCors", builder =>
    {
        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .Build();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("EnableCors");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
