---
uid: guides/http/index
title: BlazorFocused.Testing.Http
_disableBreadcrumb: true
_disableToc: false
---

# BlazorFocused.Testing.Http Guide

Tools for simulating Http requests during Tests

## Installation

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
