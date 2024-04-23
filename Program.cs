using TodoApi.Libs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0
builder.Services.AddScoped<CustomLogger>();
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-8.0

var app = builder.Build();

// Configure the HTTP request pipeline.
Console.WriteLine(app.Environment);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
