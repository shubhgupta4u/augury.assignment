using Augury.MachineTelemetryService.Services;
using Augury.RepairTelemetryService.DomainServices;
using Augury.RepairTelemetryService.Models;
using Augury.RepairTelemetryService.Models.Profile;
using Augury.RepairTelemetryService.Repository;
using Consul;
using MongoDB.Driver;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

string seqLogUiUrl = builder.Configuration["SeqLogUiUrl"];
// Set up Serilog for logging
Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("ServiceName", "MachineTelemetryService")
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(seqLogUiUrl) // Replace with your centralized logging server URL
            .CreateLogger();

builder.Host.UseSerilog();

// Register the API with Consul
string consulUrl = builder.Configuration["ConsulUrl"] ?? "http://consul:8500";
builder.Services.AddSingleton<IConsulClient, ConsulClient>(sp =>
{
    var config = new ConsulClientConfiguration
    {
        Address = new Uri(consulUrl)  // Consul container URL
    };
    return new ConsulClient(config);
});

string environment = builder.Configuration["ASPNETCORE_ENVIRONMENT"];
if (environment != null && environment.Equals("production", StringComparison.InvariantCultureIgnoreCase)){
    string jaegerUrl = builder.Configuration["JaegerUrl"];
    // Add OpenTelemetry Tracing with Jaeger Exporter
    builder.Services.AddOpenTelemetry()
        .WithTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("machine-telemetry-service"))  // Set service name
                .AddAspNetCoreInstrumentation()  // Captures HTTP requests to API Gateway
                .AddJaegerExporter(jaegerOptions =>
                {
                    jaegerOptions.AgentHost = jaegerUrl; // Jaeger host (use the container name if in Docker)
                    jaegerOptions.AgentPort = 6831;        // Jaeger default port for traces
                });
        });
    builder.Services.AddSingleton<IHostedService, ConsulHostedService>();
}

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add health checks
builder.Services.AddHealthChecks();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

//Add Services Registration
builder.Services.AddScoped<IMachineFeedService, MachineFeedServices>();

//Add Repostories Registration
builder.Services.AddSingleton<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IMachineFeedRepository, MachineFeedRepository>();


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MachineFeedProfile));

var app = builder.Build();

// Map health checks
app.MapHealthChecks("/health");
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting MachineTelemetryService web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "MachineTelemetryService Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
