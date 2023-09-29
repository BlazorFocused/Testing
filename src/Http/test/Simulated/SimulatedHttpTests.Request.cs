// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Testing.Http.Simulated;
using BlazorFocused.Testing.Http.Test.Model;
using FluentAssertions;
using System.Net;

namespace BlazorFocused.Testing.Http.Test.Simulated;

public partial class SimulatedHttpTests
{
    [Theory]
    [MemberData(nameof(HttpData))]
    public async Task ShouldVerifyRequestMade(
        HttpMethod httpMethod,
        HttpStatusCode httpStatusCode,
        string relativeRequestUrl,
        SimpleClass requestObject,
        SimpleClass responseObject)
    {
        simulatedHttp.Setup(httpMethod, relativeRequestUrl, requestObject)
            .ReturnsAsync(httpStatusCode, responseObject);

        using HttpClient client = simulatedHttp.HttpClient;
        var internalSimulatedHttp = simulatedHttp as SimulatedHttp;

        await MakeRequest(client, httpMethod, relativeRequestUrl, requestObject);

        Exception calledException = Record.Exception(() =>
        {
            Action action = GetVerifyActionGroup(httpMethod);
            action.Invoke();
        });

        Exception calledWithUrlException = Record.Exception(() =>
        {
            Action action = GetVerifyActionGroup(httpMethod, relativeRequestUrl);
            action.Invoke();
        });

        Assert.Null(calledException);
        Assert.Null(calledWithUrlException);

        if (!IsMethodWithoutContent(httpMethod))
        {
            Exception calledWithUrlAndContentException = Record.Exception(() =>
            {
                Action action = GetVerifyActionGroup(httpMethod, relativeRequestUrl, requestObject);
                action.Invoke();
            });

            Assert.Null(calledWithUrlAndContentException);
        }
    }

    [Theory]
    [MemberData(nameof(HttpData))]
    public async Task ShouldVerifyNoRequestWasMade(
        HttpMethod httpMethod,
        HttpStatusCode httpStatusCode,
        string relativeRequestUrl,
        SimpleClass requestObject,
        SimpleClass responseObject)
    {
        simulatedHttp.Setup(httpMethod, relativeRequestUrl, requestObject)
            .ReturnsAsync(httpStatusCode, responseObject);

        using HttpClient client = simulatedHttp.HttpClient;
        var internalSimulatedHttp = simulatedHttp as SimulatedHttp;
        await MakeRequest(client, httpMethod, relativeRequestUrl, requestObject);

        HttpMethod differentHttpMethod = PickDifferentMethod(httpMethod);
        string differentRelativeUrl = GetRandomRelativeUrl();

        Action actWithMethod = GetVerifyActionGroup(differentHttpMethod);
        Action actWithMethodAndUrl = GetVerifyActionGroup(httpMethod, differentRelativeUrl);

        actWithMethod.Should().Throw<SimulatedHttpTestException>()
            .Where(exception => exception.Message.Contains(differentHttpMethod.ToString()));

        actWithMethodAndUrl.Should().Throw<SimulatedHttpTestException>()
            .Where(exception => exception.Message.Contains(httpMethod.ToString()) &&
                exception.Message.Contains(differentRelativeUrl));

        if (!IsMethodWithoutContent(httpMethod))
        {
            Action actWithMethodUrlContent =
                GetVerifyActionGroup(httpMethod, relativeRequestUrl, GetRandomSimpleClass());

            actWithMethodUrlContent.Should().Throw<SimulatedHttpTestException>()
                .Where(exception => exception.Message.Contains("Request Object"));
        }
    }

    [Theory]
    [MemberData(nameof(HttpData))]
    public void ShouldVerifyNoRequestMade(
        HttpMethod httpMethod,
        HttpStatusCode httpStatusCode,
        string relativeRequestUrl,
        SimpleClass requestObject,
        SimpleClass responseObject)
    {
        simulatedHttp.Setup(httpMethod, relativeRequestUrl, requestObject)
            .ReturnsAsync(httpStatusCode, responseObject);

        var internalSimulatedHttp = simulatedHttp as SimulatedHttp;

        Action action = GetVerifyActionGroup(httpMethod);

        action.Should().Throw<SimulatedHttpTestException>();
    }

    private Action GetVerifyActionGroup(HttpMethod httpMethod, string url = null, object content = null) => httpMethod switch
    {
        { } when url is null && content is null =>
            () => simulatedHttp.VerifyWasCalled(httpMethod),

        { } when url is not null && content is null =>
           () => simulatedHttp.VerifyWasCalled(httpMethod, url),

        { } when url is null && content is not null =>
           () => simulatedHttp.VerifyWasCalled(httpMethod, url),

        { } when url is not null && content is not null =>
            () => simulatedHttp.VerifyWasCalled(httpMethod, url, content),

        _ => throw new NotImplementedException("Verify Action Group Not Implemented")
    };

    private static bool IsMethodWithoutContent(HttpMethod httpMethod) => httpMethod == HttpMethod.Delete || httpMethod == HttpMethod.Get;
}
