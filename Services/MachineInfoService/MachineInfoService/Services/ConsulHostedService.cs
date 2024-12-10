using Consul;

namespace Augury.MachineInfoService.Services
{
    public class ConsulHostedService : IHostedService
    {
        private const string ServiceName = "machine-data-service";
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
            _logger.LogInformation($"Machine Data Service is running on: {_hostname}:{_port}");

            var registration = new AgentServiceRegistration
            {
                ID = $"{ServiceName}-{_hostname}",
                Name = ServiceName,
                Address = _hostname,  // Name of the container
                Port = _port,         // Port of the service inside the container
                Tags = new[] { "api", ServiceName },
                Check = new AgentServiceCheck()
                {
                    HTTP = $"http://{_hostname}:{_port}/health", // URL to check health of the service
                    Interval = TimeSpan.FromSeconds(20), // Health check interval
                    Timeout = TimeSpan.FromSeconds(10), // Health check timeout
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(2)
                }
            };
            _registrationId = registration.ID;

            await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Machine Data Service is shutting down: {_hostname}:{_port}");
            await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}
