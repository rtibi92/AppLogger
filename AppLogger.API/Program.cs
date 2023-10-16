using AppLogger.FileLoggerComponent;
using AppLogger.ConsoleLoggerComponent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Eventing.Reader;
using AppLogger.StreamWriterComponent;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();

var loggerTypeSection = builder.Configuration.GetSection("Logging").GetSection("Type");
if (loggerTypeSection != null)
{



    if (loggerTypeSection.Value.Equals("File"))
    {
        builder.Services.AddLogging(loggingBuilder =>
        {
            var loggingSection = builder.Configuration.GetSection("Logging:File");

            loggingBuilder.AddFile(loggingSection);
        });
    }
    else if (loggerTypeSection.Value.Equals("Console"))
    {
        LogLevel minLevel = GetMinLogLevel(builder);

        builder.Logging.AddConsoleLogger(new ConsoleLoggerConfig
        {
            LogLevel = LogLevel.Debug,
            Color = ConsoleColor.Gray,
            MinLevel = minLevel
        }).AddDebug();

        builder.Logging.AddConsoleLogger(new ConsoleLoggerConfig
        {
            LogLevel = LogLevel.Information,
            Color = ConsoleColor.Green,
            MinLevel = minLevel
        }).AddDebug();

        builder.Logging.AddConsoleLogger(new ConsoleLoggerConfig
        {
            LogLevel = LogLevel.Error,
            Color = ConsoleColor.Red,
            MinLevel = minLevel
        }).AddDebug();
    }
    else if (loggerTypeSection.Value.Equals("Stream"))
    {
        builder.Services.AddLogging(loggingBuilder =>
        {
            var loggingSection = builder.Configuration.GetSection("Logging:Stream");

            loggingBuilder.AddStreamLogger(loggingSection, typeof(MemoryStream));
        });

    }
}



var app = builder.Build();

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

static LogLevel GetMinLogLevel(WebApplicationBuilder builder)
{
    var minLevelConfig = builder.Configuration.GetSection("Logging:Console").GetSection("MinLevel");
    LogLevel minLevel = LogLevel.Debug;

    if (minLevelConfig != null)
    {
        switch (minLevelConfig.Value)
        {
            case "Debug":
                minLevel = LogLevel.Debug;
                break;
            case "Information":
                minLevel = LogLevel.Information;
                break;
            case "Error":
                minLevel = LogLevel.Error;
                break;
            default:
                minLevel = LogLevel.Debug;
                break;
        }
    }

    return minLevel;
}