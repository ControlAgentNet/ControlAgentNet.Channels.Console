using Microsoft.Extensions.DependencyInjection;
using ControlAgentNet.Core.Abstractions;
using ControlAgentNet.Runtime.Extensions;

namespace ControlAgentNet.Channels.Console;

public static class ConsoleChannelExtensions
{
    public static IControlAgentNetBuilder AddConsoleChannel(this IControlAgentNetBuilder builder)
    {
        builder.Services.AddHostedService<ConsoleChannelService>();
        return builder.AddChannelDescriptor(ConsoleChannelDescriptor.Instance);
    }
}
