# ControlAgentNet.Channels.Console

<p align="center">
  <img src="https://img.shields.io/github/license/ControlAgentNet/ControlAgentNet.Channels.Console" alt="License">
  <img src="https://img.shields.io/github/actions/workflow/status/ControlAgentNet/ControlAgentNet.Channels.Console/ci.yml?branch=main" alt="CI">
  <img src="https://img.shields.io/nuget/v/ControlAgentNet.Channels.Console" alt="NuGet Version">
</p>

> Interactive console channel for ControlAgentNet agents.

## What This Repository Contains

This repository publishes the `ControlAgentNet.Channels.Console` package and includes a sample application for local experimentation.

## What It Does

This package adds an interactive console channel to a ControlAgentNet host.

Use it when you want:

- the simplest local channel for development and testing
- an interactive REPL-style console host for your agent
- a lightweight channel without extra external platform dependencies

This repository does **not** include the base runtime itself. You still need the base packages from `ControlAgentNet.Agents`.

## Installation

```bash
dotnet add package ControlAgentNet.Agents
dotnet add package ControlAgentNet.Channels.Console
```

## Usage

```csharp
using ControlAgentNet.Agents;
using ControlAgentNet.Channels.Console;

builder.Services.AddControlAgentAgent(builder.Configuration, builder.Environment, options =>
{
    options.Id = "my-agent";
    options.Name = "My Agent";
    options.Instructions = "You are a helpful assistant.";
})
    .AddConsoleChannel();
```

## Repository Layout

```text
src/
  ControlAgentNet.Channels.Console/
samples/
  HelloWorld.Console/
ControlAgentNet.Channels.Console.slnx
```

## Message Contract

Console input is normalized into the shared `IncomingMessage` contract from `ControlAgentNet.Core`.

- `ChannelId` is `console`
- `ChannelType` is `Console`
- `ConversationId` is the local console session
- `UserId` is `console-user`

Agents should read `IncomingMessage` and stay independent of console implementation details.

## Build

By default this repository depends on the `ControlAgentNet.Core` and `ControlAgentNet.Runtime` packages from NuGet or another configured package source.

```bash
dotnet restore ControlAgentNet.Channels.Console.slnx
dotnet build ControlAgentNet.Channels.Console.slnx -c Release
dotnet pack ControlAgentNet.Channels.Console.slnx -c Release -o artifacts/nuget
```

For local coordinated development against the sibling base repository, use project references instead of NuGet by passing `UseLocalControlAgentNet=true`:

```bash
dotnet restore ControlAgentNet.Channels.Console.slnx /p:UseLocalControlAgentNet=true
dotnet build ControlAgentNet.Channels.Console.slnx -c Release /p:UseLocalControlAgentNet=true
dotnet test ControlAgentNet.Channels.Console.slnx -c Release --no-build /p:UseLocalControlAgentNet=true
```

The default base path is `../ControlAgentNet.Agents` relative to this repository. Override it when the repos are not siblings:

```bash
dotnet build ControlAgentNet.Channels.Console.slnx /p:UseLocalControlAgentNet=true /p:ControlAgentNetBasePath=/absolute/path/to/ControlAgentNet.Agents
```

Do not use `UseLocalControlAgentNet=true` for package publishing; releases should validate the NuGet dependency graph.

## Sample

The repository includes `samples/HelloWorld.Console` to demonstrate the channel working with the base package.

## Versioning

- local builds: `0.1.3-dev`
- pull requests: `0.1.3-preview.<run_number>`
- pushes to `main`: `0.1.3-alpha.<run_number>`
- tags like `v0.1.3`: exact stable package version `0.1.3`

See `VERSIONING.md` for the release flow.
