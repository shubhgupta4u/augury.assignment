using Augury.MachineInfoService.DomainServices;
using Augury.MachineInfoService.Models;
using Augury.MachineInfoService.Models.Profile;
using Augury.MachineInfoService.Repository;
using Augury.MachineInfoService.Services;
using Consul;
using MongoDB.Driver;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

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
if (environment != null && environment.Equals("production", StringComparison.InvariantCultureIgnoreCase))
{
    // Add OpenTelemetry Tracing with Jaeger Exporter
    builder.Services.AddOpenTelemetry()
        .WithTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("machine-data-service"))  // Set service name
                .AddAspNetCoreInstrumentation()  // Captures HTTP requests to API Gateway
                .AddJaegerExporter(jaegerOptions =>
                {
                    jaegerOptions.AgentHost = "localhost"; // Jaeger host (use the container name if in Docker)
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
builder.Services.AddScoped<IMachineService, MachineServices>();

//Add Repostories Registration
builder.Services.AddSingleton<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MachineProfile));

var app = builder.Build();

// Map health checks
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();