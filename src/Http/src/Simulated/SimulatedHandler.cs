// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Simulated;

internal class SimulatedHandler
{
    public static async Task<(HttpMethod method, string url, string content)> GetRequestMessageContents(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpMethod method = request.Method;
        string url = request.RequestUri.OriginalString;

        string content = request.Content is not null ?
            await request.Content.ReadAsStringAsync(cancellationToken) : default;

        return (method, url, content);
    }
}
