using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PatPortal.API.CustomMiddlewere;
using PatPortal.Application.Configuration;
using PatPortal.Database;
using PatPortal.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
var settings = builder.Configuration.Get<ApplicationConfiguration>();

builder.Services.AddDbContext<PatPortalDbContext>(options =>
    options.UseSqlServer(settings.ConnectionStrings.PatPortalDataBase),
    ServiceLifetime.Transient);

builder.Services.AddControllers()
        .AddFluentValidation( fv =>
        {
            fv.AutomaticValidationEnabled = false;
            fv.LocalizationEnabled = false;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

//Dependency Injection using autofac
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
                builder.RegisterModule(new ApplicationModule());
                builder.RegisterModule(new InfrastructureModule(settings));
                builder.RegisterModule(new DomainModule(settings));
            });

//Register MediatR & Mapper
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseMiddleware<ApiErrorHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("default");

app.Run();
