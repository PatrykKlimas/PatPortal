using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using PatPortal.API.CustomMiddlewere;
using PatPortal.Application.Configuration;
using PatPortal.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllers()
        .AddFluentValidation( fv =>
        {
            fv.AutomaticValidationEnabled = false;
            fv.LocalizationEnabled = false;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = builder.Configuration.Get<ApplicationConfiguration>();

//Dependency Injection using autofac
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
                builder.RegisterModule(new ApplicationModule(settings));
                builder.RegisterModule(new InfrastructureModule(settings));
                builder.RegisterModule(new DomainModule(settings));
            });

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

app.UseAuthorization();

app.MapControllers();

app.Run();
