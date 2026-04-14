using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ControlAgentNet.Core.Abstractions;
using ControlAgentNet.Core.Models;

namespace ControlAgentNet.Channels.Console;

public sealed class ConsoleChannelService : BackgroundService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IAgentOrchestrator _orchestrator;
    private readonly ILogger<ConsoleChannelService> _logger;

    public ConsoleChannelService(
        IHostApplicationLifetime applicationLifetime,
        IAgentOrchestrator orchestrator,
        ILogger<ConsoleChannelService> logger)
    {
        _applicationLifetime = applicationLifetime;
        _orchestrator = orchestrator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Console channel started. Type 'exit' to quit.");

        while (!stoppingToken.IsCancellationRequested)
        {
            System.Console.Write("You: ");
            var input = await Task.Run(() => System.Console.ReadLine(), stoppingToken);

            if (input is null)
            {
                _logger.LogInformation("Console input closed. Stopping host.");
                _applicationLifetime.StopApplication();
                break;
            }

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogInformation("Console channel received exit command.");
                _applicationLifetime.StopApplication();
                break;
            }

            var response = await _orchestrator.ProcessAsync(new IncomingMessage
            {
                ConversationId = "console-session",
                TenantId = "local",
                UserId = "console-user",
                Text = input,
                ChannelId = "console"
            }, stoppingToken);

            System.Console.WriteLine($"Agent: {response.Text}");
        }
    }
}
