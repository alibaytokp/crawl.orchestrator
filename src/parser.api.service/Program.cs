using messaging.service.Interfaces;
using messaging.service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using parser.api.service.Extensions;
using parser.api.service.Interfaces;
using parser.api.service.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();


bool isProd = builder.Environment.IsProduction();
var secretKey = builder.Configuration["Jwt:SecretKey"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "authentication-service",
            ValidateAudience = true,
            ValidAudience = "parser-api-service",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuerSigningKey = true,
        };
    });



// Configure Authorization
builder.Services.AddAuthorization();

builder.Services.AddMassTransitWithEnvironment(builder.Configuration, isProd);

builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddScoped<IAdProducerService, AdProducerService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health").AllowAnonymous();

app.MapControllers();

app.Run();
