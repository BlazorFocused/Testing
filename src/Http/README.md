[![Nuget Version](https://img.shields.io/nuget/v/BlazorFocused.Testing.Http?logo=nuget)](https://www.nuget.org/packages/BlazorFocused.Testing.Http)
[![Nuget Downloads](https://img.shields.io/nuget/dt/BlazorFocused.Testing.Http?logo=nuget)](https://www.nuget.org/packages/BlazorFocused.Testing.Http)
[![Continuous Integration](https://github.com/BlazorFocused/testing/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/BlazorFocused/testing/actions/workflows/continuous-integration.yml)

# BlazorFocused Testing Http Project

Adding Testing Tools for .NET development HttpClient requests

## NuGet Packages

| Package                                                                                  | Description                                     |
| ---------------------------------------------------------------------------------------- | ----------------------------------------------- |
| [BlazorFocused.Testing.Http](https://www.nuget.org/packages/BlazorFocused.Testing.Http/) | Tools for simulating Http requests during Tests |

## Documentation

Please visit the [BlazorFocused.Testing Documentation Site](https://BlazorFocused.github.io/Testing/) for installation, getting started, and API documentation.

## Samples

Please visit and/or download our [BlazorFocused.Testing Sample Solution](https://github.com/BlazorFocused/testing/tree/main/samples/HttpSample) to get a more in-depth view of usage.

## Installation

Dotnet CLI

```dotnetcli

dotnet add package BlazorFocused.Testing.Http

```

## Quick Start

```csharp
private readonly ISimulatedHttp simulatedHttp;
private readonly TestClient testClient;

public TestClientTests()
{
    string baseAddress = "https://<your base address>";
    simulatedHttp = SimulatedHttpBuilder.CreateSimulatedHttp(baseAddress);

    testClient =
        new TestClient(simulatedHttp.HttpClient);
}

[Fact]
public async Task ShouldPerformHttpRequest()
{
    HttpMethod httpMethod = HttpMethod.Get;
    string url = "api/test";
    var request = new TestClass { Name = "Testing" };
    HttpStatusCode statusCode = HttpStatusCode.OK;
    var response = new TestClass { Id = "123", Name = "Testing" };

    simulatedHttp.Setup(httpMethod, url, request)
        .ReturnsAsync(statusCode, response);

    var actualResponse = await testClient.PostAsync<SimpleClass>(url, request);

    actualResponse.Should().BeEquivalentTo(response);

    simulatedHttp.VerifyWasCalled(httpMethod, url, request);
}
```
