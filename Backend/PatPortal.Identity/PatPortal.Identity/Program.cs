using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatPortal.Identity.Application.Configuration;
using PatPortal.Identity.CustomMiddlewere;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.Services;
using PatPortal.Identity.Domain.Services.Interfaces;
using PatPortal.Identity.Infrastructure.Configuration;
using PatPortal.Identity.Infrastructure.Prividers;
using PatPortal.Identity.Infrastructure.Repositories.Mocks;
using FluentValidation.AspNetCore;
using System.Text;
using FluentValidation;
using PatPortal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using PatPortal.Identity.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.AutomaticValidationEnabled = false;
        fv.LocalizationEnabled = false;
    });   

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme."
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            new string[] {}
        }
    });
    });

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
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, MockUserRepository>();
builder.Services.AddTransient<IValidator<User>, UserValidator>();

//Register MediatR & Mapper
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiErrorHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
