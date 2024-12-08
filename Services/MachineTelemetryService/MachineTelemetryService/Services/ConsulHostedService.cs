using Consul;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Augury.MachineTelemetryService.Services
{
    public class ConsulHostedService : IHostedService
    {
        private const string ServiceName = "machine-telemetry-service";
        private readonly IConsulClient _consulClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConsulHostedService> _logger;
        private string _registrationId;
        private string _hostname;
        private int _port;

        public ConsulHostedService(IConsulClient consulClient, IConfiguration configuration, ILogger<ConsulHostedService> logger)
        {
            _consulClient = consulClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _hostname = _configuration.GetValue<string>("ServiceDomain");
            _port = _configuration.GetValue<int>("Port");
            _logger.LogInformation($"Machine Telemetry Service is running on: {_hostname}:{_port}");

            var registration = new AgentServiceRegistration
            {
                ID = $"{ServiceName}_{_hostname}:{_port}",
                Name = ServiceName,
                Address = _hostname,  // Name of the container
                Port = _port,                   // Port of the service inside the container
                Tags = new[] { "api", "machine-telemetry" }
            };
            _registrationId = registration.ID;

            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Machine Telemetry Service is shutting down: {_hostname}:{_port}");
            await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}
