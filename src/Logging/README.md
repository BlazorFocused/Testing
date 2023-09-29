[![Nuget Version](https://img.shields.io/nuget/v/BlazorFocused.Testing.Logging?logo=nuget)](https://www.nuget.org/packages/BlazorFocused.Testing.Logging)
[![Nuget Downloads](https://img.shields.io/nuget/dt/BlazorFocused.Testing.Logging?logo=nuget)](https://www.nuget.org/packages/BlazorFocused.Testing.Logging)
[![Continuous Integration](https://github.com/BlazorFocused/testing/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/BlazorFocused/testing/actions/workflows/continuous-integration.yml)

# BlazorFocused Testing Logging Project

Adding Testing Tools for .NET development logging events

## NuGet Packages

| Package                                                                                        | Description                           |
| ---------------------------------------------------------------------------------------------- | ------------------------------------- |
| [BlazorFocused.Testing.Logging](https://www.nuget.org/packages/BlazorFocused.Testing.Logging/) | Tools for verifying logs during Tests |

## Documentation

Please visit the [BlazorFocused.Testing Documentation Site](https://BlazorFocused.github.io/Testing/) for installation, getting started, and API documentation.

## Samples

Please visit and/or download our [BlazorFocused.Testing Sample Solution](https://github.com/BlazorFocused/testing/tree/main/samples/LoggingSample) to get a more in-depth view of usage.

## Installation

```dotnetcli
dotnet add package BlazorFocused.Testing.Logging
```

## Quick Start

```csharp
private readonly ITestLogger<TestService> testLogger;
private readonly ITestService testService;

public TestServiceTests()
{
    testLogger = ToolsBuilder.CreateTestLogger<TestService>();

    testService =
        new TestService(testLogger);
}

[Fact]
public async Task ShouldLogError()
{
    testService.GeneralMethodWithError();

    testLogger.VerifyWasCalledWith(LogLevel.Error)
}
```
