using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ControlAgentNet.Core.Abstractions;
using ControlAgentNet.Core.Models;
using ControlAgentNet.Runtime.Channels;
using ControlAgentNet.Runtime.Extensions;
using Xunit;

namespace ControlAgentNet.Channels.Console.Tests;

public class ConsoleChannelCompositionTests
{
    [Fact]
    public void AddConsoleChannel_registers_console_hosted_service()
    {
        var services = CreateServices();

        services.AddSingleton<IAgentEngine, TestAgentEngine>();
        services.AddControlAgentNet(CreateConfiguration(), new TestHostEnvironment())
            .AddConsoleChannel();

        using var provider = services.BuildServiceProvider();
        var hostedServices = provider.GetServices<IHostedService>().ToList();

        Assert.Contains(hostedServices, service => service is ConsoleChannelService);
    }

    [Fact]
    public void AddConsoleChannel_registers_console_descriptor_in_channel_registry()
    {
        var services = CreateServices();

        services.AddSingleton<IAgentEngine, TestAgentEngine>();
        services.AddControlAgentNet(CreateConfiguration(), new TestHostEnvironment())
            .AddConsoleChannel();

        using var provider = services.BuildServiceProvider();
        var channelRegistry = provider.GetRequiredService<ChannelRegistry>();
        var channel = Assert.Single(channelRegistry.GetChannelStates(), state => state.Descriptor.Id == "console");

        Assert.Equal("Console", channel.Descriptor.Name);
        Assert.True(channel.IsEnabled);
    }

    private static ServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IHostApplicationLifetime, TestHostApplicationLifetime>();
        return services;
    }

    private static IConfiguration CreateConfiguration()
        => new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Agent:Id"] = "console-test-agent",
                ["Agent:Name"] = "Console Test Agent",
                ["Agent:Description"] = "Test agent for console channel",
                ["Agent:Instructions"] = "Be brief"
            })
            .Build();

    private sealed class TestAgentEngine : IAgentEngine
    {
        public Task<AgentEngineResult> RunAsync(AgentContext context, CancellationToken cancellationToken)
            => Task.FromResult(AgentEngineResult.FromText("test"));

        public async IAsyncEnumerable<string> StreamAsync(AgentContext context, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
        {
            yield return (await RunAsync(context, cancellationToken)).Text;
        }
    }

    private sealed class TestHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";
        public string ApplicationName { get; set; } = "Test";
        public string ContentRootPath { get; set; } = "/";
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }

    private sealed class TestHostApplicationLifetime : IHostApplicationLifetime
    {
        public CancellationToken ApplicationStarted => CancellationToken.None;
        public CancellationToken ApplicationStopping => CancellationToken.None;
        public CancellationToken ApplicationStopped => CancellationToken.None;

        public void StopApplication()
        {
        }
    }
}
