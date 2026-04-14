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

## Build

This repository depends on the base source repository during CI and local source builds.

```bash
dotnet restore ControlAgentNet.Channels.Console.slnx
dotnet build ControlAgentNet.Channels.Console.slnx -c Release
dotnet pack ControlAgentNet.Channels.Console.slnx -c Release -o artifacts/nuget
```

## Sample

The repository includes `samples/HelloWorld.Console` to demonstrate the channel working with the base package.

## Versioning

- local builds: `0.1.1-dev`
- pull requests: `0.1.1-preview.<run_number>`
- pushes to `main`: `0.1.1-alpha.<run_number>`
- tags like `v0.1.1`: exact stable package version `0.1.1`

See `VERSIONING.md` for the release flow.
