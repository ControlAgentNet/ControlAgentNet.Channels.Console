using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ControlAgentNet.Agents;
using ControlAgentNet.Channels.Console;
using ControlAgentNet.Core.Abstractions;
using ControlAgentNet.Core.Models;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddControlAgentAgent(builder.Configuration, builder.Environment, configureAgent: options =>
{
    options.Id = "hello-world-console";
    options.Name = "Hello World Console Agent";
    options.Description = "Minimal sample for the ControlAgentNet.Channels.Console package.";
    options.Instructions = "Respond directly and clearly in one short sentence.";
})
    .AddConsoleChannel();

builder.Services.AddSingleton<IAgentEngine, HelloWorldConsoleEngine>();

using var host = builder.Build();
await host.RunAsync();

internal sealed class HelloWorldConsoleEngine : IAgentEngine
{
    public Task<AgentEngineResult> RunAsync(AgentContext context, CancellationToken cancellationToken)
    {
        if (string.Equals(context.Message.Text, "hello", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AgentEngineResult.FromText("Hello from the ControlAgentNet console sample."));
        }

        return Task.FromResult(AgentEngineResult.FromText($"ControlAgentNet console sample received: {context.Message.Text}"));
    }

    public async IAsyncEnumerable<string> StreamAsync(AgentContext context, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        yield return (await RunAsync(context, cancellationToken)).Text;
    }
}
