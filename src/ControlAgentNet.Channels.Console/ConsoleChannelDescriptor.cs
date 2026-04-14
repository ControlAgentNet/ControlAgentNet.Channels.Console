using ControlAgentNet.Core.Descriptors;

namespace ControlAgentNet.Channels.Console;

internal static class ConsoleChannelDescriptor
{
    public static readonly ChannelDescriptor Instance = new(
        Id: "console",
        Name: "Console",
        Description: "Interactive console channel for local ControlAgentNet agent sessions.",
        DefaultEnabled: true,
        Transport: ChannelTransportKind.Console,
        Version: "1.0.0",
        SourceAssembly: typeof(ConsoleChannelExtensions).Assembly.GetName().Name ?? nameof(ControlAgentNet.Channels.Console),
        Category: "cli");
}
