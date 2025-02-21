using System.Text.Json.Serialization;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Backend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbSource>(options =>
{
    options.UseNpgsql("Server=localhost;Port=5432;Database=root;User Id=root;Password=secret;");
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<ToDo>();
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.AllowTrailingCommas = true;
    config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
