using Application.Commands.Products.Insert;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

EFCoreImplementation.DependencyContainer.AddRepositories(builder.Services, builder.Configuration);
Infraestructure.Services.DependencyContainer.AddServices(builder.Services, builder.Configuration);
Application.DependencyContainer.AddServices(builder.Services);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InsertProductCommandHandler).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<InsertProductRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

var memoryCache = app.Services.GetRequiredService<IMemoryCache>();
var dictionary = new Dictionary<int, string>
{
    { 0, "Inactive" },
    { 1, "Active" }
};

memoryCache.Set("StatusProductDictionary", dictionary, TimeSpan.FromMinutes(5));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();