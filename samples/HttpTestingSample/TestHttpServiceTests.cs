﻿// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using FluentAssertions;
using System.Net;
using BlazorFocused.Testing.Http;

namespace HttpTestingSample;

public class TestHttpServiceTests
{
    private readonly TestHttpService testHttpService;
    private readonly ISimulatedHttp simulatedHttp;

    public record TestClass(int Id, string Name);
    public record TestClassInput(string Description);

    public TestHttpServiceTests()
    {
        simulatedHttp = SimulatedHttpBuilder.CreateSimulatedHttp();
        testHttpService = new TestHttpService(simulatedHttp.HttpClient);
    }

    [Fact]
    public async Task GetResponseObjectAsync_ShouldGetResponseFromGetRequest()
    {
        // Arrange
        string relativeUrl = "api/Test";
        var expectedResponse = new TestClass(Id: 1, Name: "TestName");

        simulatedHttp.Setup(HttpMethod.Get, relativeUrl)
            .ReturnsAsync(HttpStatusCode.OK, expectedResponse);

        // Act
        TestClass actualResponse =
            await testHttpService.GetResponseObjectAsync<TestClass>(relativeUrl);

        // Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task PostRequestAsync_ShouldSendPostRequest()
    {
        // Arrange
        string relativeUrl = "api/Test";
        var requestObject = new TestClassInput(Description: "NeedCreation");
        var expectedResponse = new TestClass(Id: 5, Name: "AutoGeneratedTestName");

        simulatedHttp.Setup(HttpMethod.Post, relativeUrl, requestObject)
            .ReturnsAsync(HttpStatusCode.OK, expectedResponse);

        // Act
        TestClass actualResponse =
            await testHttpService.PostRequestAsync<TestClassInput, TestClass>(relativeUrl, requestObject);

        // Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task PostRequestAsync_ShouldThrowForNonSuccessResponse()
    {
        // Arrange
        string relativeUrl = "api/Test";
        var requestObject = new TestClassInput(Description: "NeedCreation");
        var expectedResponse = new TestClass(Id: 5, Name: "AutoGeneratedTestName");

        simulatedHttp.Setup(HttpMethod.Post, relativeUrl, requestObject)
            .ReturnsAsync(HttpStatusCode.BadGateway, null);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() =>
            testHttpService.PostRequestAsync<TestClassInput, TestClass>(relativeUrl, requestObject));
    }
}
