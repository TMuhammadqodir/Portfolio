using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Contexts;
using Portfolio.Service.Helpers;
using Portfolio.WebApi.Extensions;
using Portfolio.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Enrich.FromLogContext();
});

// Services
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DB connection string not found"));
});

// JWT
builder.Services.AddJwt(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Helpers
PathHelper.WebRootPath = Path.Combine(builder.Environment.WebRootPath);

// Set ASPNETCORE_URLS from environment variable for Render.com
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";  // Default to 8080 if not set
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();