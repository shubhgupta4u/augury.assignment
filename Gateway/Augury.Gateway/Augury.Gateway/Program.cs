using Augury.Gateway.Aggregators;
using Consul;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Serilog;
using Ocelot.Cache.CacheManager;
using CacheManager.Core;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);
string environment = builder.Configuration["Env"];

// Set up Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

//Add Ocelot
if (!string.IsNullOrEmpty(environment))
{
    if(environment.Equals("production", StringComparison.InvariantCultureIgnoreCase))
    {
        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    }else if (environment.Equals("staging", StringComparison.InvariantCultureIgnoreCase))
    {
        builder.Configuration.AddJsonFile("ocelot.staging.json", optional: false, reloadOnChange: true);
    }
    else
    {
        builder.Configuration.AddJsonFile("ocelot.development.json", optional: false, reloadOnChange: true);
    }
}
else
{
    builder.Configuration.AddJsonFile("ocelot.development.json", optional: false, reloadOnChange: true);
}
IOcelotBuilder ocelot = builder.Services.AddOcelot()
        .AddSingletonDefinedAggregator<MachineFeedAggregator>()
        .AddSingletonDefinedAggregator<TabularMachineFeedAggregator>()
        .AddPolly()
        .AddCacheManager(options =>
        {
            options.WithDictionaryHandle(); // Default in-memory cache
        });

if (!string.IsNullOrEmpty(environment) || environment.Equals("production", StringComparison.InvariantCultureIgnoreCase))
{
    ocelot.AddConsul();

    // Add OpenTelemetry Tracing
    builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("augury-api-gateway"))  // Set service name
            .AddAspNetCoreInstrumentation()   // Capture traces for incoming HTTP requests
            .AddHttpClientInstrumentation()   // Capture traces for outgoing HTTP requests (if applicable)
            .AddJaegerExporter(jaegerOptions =>
            {
                jaegerOptions.AgentHost = "localhost"; // Jaeger host (use the container name if in Docker)
                jaegerOptions.AgentPort = 6831;        // Jaeger default port for traces
            });
    });
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForOcelot(builder.Configuration,
  (o) =>
  {
      o.GenerateDocsForAggregates = true;
  });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Augury Gateway API",
        Version = "v1",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
