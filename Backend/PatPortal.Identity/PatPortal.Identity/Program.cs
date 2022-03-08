using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PatPortal.Identity.Application.Configuration;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.Services;
using PatPortal.Identity.Domain.Services.Interfaces;
using PatPortal.Identity.Infrastructure.Configuration;
using PatPortal.Identity.Infrastructure.Prividers;
using PatPortal.Identity.Infrastructure.Repositories.Mocks;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.Get<ApplicationConfiguration>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(settings.JwtConfig.Secret);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.JwtConfig.Issuer,
            ValidAudience = settings.JwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddScoped<IIdentityProvider>(provider => new IdentityProvider(settings));
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IUserRepository, MockUserRepository>();

//Register MediatR & Mapper
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
