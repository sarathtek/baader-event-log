using EventLogBackend.Repository;
using EventLogBackend.Services;
using EventLogBackend.Mappers;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddHostedService<EventGeneratorService>();
builder.Services.AddAutoMapper(typeof(EventProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var logger = context.RequestServices.GetService<ILoggerFactory>()?.CreateLogger("GlobalExceptionHandler");
            logger?.LogError(error.Error, "Unhandled exception occurred");
            await context.Response.WriteAsync("{\"error\": \"An unexpected error occurred. Please try again later.\"}");
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
