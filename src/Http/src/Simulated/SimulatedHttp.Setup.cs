// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.Net;
using System.Text.Json;

namespace BlazorFocused.Testing.Http.Simulated;

internal partial class SimulatedHttp
{
    public ISimulatedHttpSetup Setup(HttpMethod method, string url, object content = null)
    {
        string requestString = content switch
        {
            null => null,
            { } when content is HttpContent httpContent => httpContent.ReadAsStringAsync().GetAwaiter().GetResult(),
            _ => JsonSerializer.Serialize(content)
        };

        var request = new SimulatedHttpRequest { Method = method, Url = url, RequestContent = requestString };

        return new SimulatedHttpSetup(request, Resolve);
    }

    private void Resolve(SimulatedHttpRequest request, HttpStatusCode statusCode, object response)
    {
        string responseString = response is not null ? JsonSerializer.Serialize(response) : null;

        var setupResponse = new SimulatedHttpResponse
        {
            Method = request.Method,
            Url = GetFullUrl(request.Url),
            StatusCode = statusCode,
            RequestContent = request?.RequestContent,
            ResponseContent = responseString
        };

        Responses.Add(setupResponse);
    }
}
