---
uid: guides/logging/index
title: BlazorFocused.Testing.Logging
_disableBreadcrumb: true
_disableToc: false
---

# BlazorFocused.Testing.Logging Guide

Tools for verifying logs during Tests

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
