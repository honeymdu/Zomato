using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zomato.Service;
using Zomato.Service.Impl;
using Zomato.Exceptions;
using Zomato.Advices;
using Zomato.ServiceRegistry;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using Zomato.Strategies.Impl;
using Zomato.Strategies;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// ✅ Use default Kestrel
builder.WebHost.UseKestrel();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"), x => x.UseNetTopologySuite()));

// ✅ Load JWT settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSettings.GetValue<string>("Secret") ?? throw new InvalidOperationException("JWT Secret is missing.");

// ✅ Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// Registering the services

var services = builder.Services;

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<Zomato.Config.MappingProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);



// Register all strategies using extension method
services.AddStrategyServices(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Fix: Configure Swagger properly
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "My Public API",
//        Version = "v1",
//        Description = "Public API with JWT Authentication"
//    });
//});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// ✅ Always enable Swagger
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Public API v1");
//    c.RoutePrefix = string.Empty; // Swagger UI at root
//});

//app.UseHsts();

// Add Middleware for Exception Handling
app.UseMiddleware<ExceptionMiddleware>();



// Register global response middleware
app.UseMiddleware<GlobalResponseMiddleware>(); 



//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
