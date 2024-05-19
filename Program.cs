using System.Globalization;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using TodoApi.Libs;
using TodoApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0
builder.Services.AddScoped<CustomLogger>();
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0

builder.Services.AddLocalization();
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
// using Redis for catch
builder.Services.AddStackExchangeRedisCache(o =>
{
    o.Configuration = "localhost:6379";
    o.InstanceName = "redis";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
Console.WriteLine(app.Environment);
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseMiddleware<HeaderCheckerMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("zh-TW"))
};

app.UseRequestLocalization(options);
// app.UseRequestLocalization();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
