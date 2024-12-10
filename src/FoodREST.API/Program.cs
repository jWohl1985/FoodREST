using FoodREST.API;
using FoodREST.API.Mapping;
using FoodREST.Application;
using FoodREST.Infrastructure;
using FoodREST.Infrastructure.Persistence;
using FoodREST.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddOutputCache(x =>
{
    x.AddBasePolicy(c => c.Cache());
    x.AddPolicy(OutputCachePolicies.FoodCachePolicy, c =>
    {
        c.Cache()
            .Expire(TimeSpan.FromMinutes(1))
            .Tag(OutputCacheTags.FoodTag);
    });
});

builder.Services.AddControllers();

builder.Services
    .AddInfrastructure(config)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();

app.Run();