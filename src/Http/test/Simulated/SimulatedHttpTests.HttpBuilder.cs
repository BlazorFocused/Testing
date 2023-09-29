// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Testing.Http.Test.Extensions;
using BlazorFocused.Testing.Http.Test.Model;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace BlazorFocused.Testing.Http.Test.Simulated;

public partial class SimulatedHttpTests
{
    [Fact]
    public async Task ShouldProvideMockDataThroughDependencyInjection()
    {
        string testClientName = "TestClient";
        string relativeUrl = new Faker().Internet.UrlRootedPath();
        SimpleClass expectedResponse = SimpleClassUtilities.GetRandomSimpleClass();
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddConfiguration()
            .AddHttpClient(testClientName, httpClient =>
                httpClient.BaseAddress = new Uri(baseAddress))
            .AddSimulatedHttp(simulatedHttp);

        using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

        simulatedHttp.Setup(HttpMethod.Get, relativeUrl)
            .ReturnsAsync(HttpStatusCode.OK, expectedResponse);

        HttpClient client = httpClientFactory.CreateClient(testClientName);

        SimpleClass actualResponse = await client.GetFromJsonAsync<SimpleClass>(relativeUrl);

        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }
}
