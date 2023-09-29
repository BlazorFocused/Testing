// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Simulated;

internal class SimulatedVerificationHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        (HttpMethod _, string url, string _) =
            await SimulatedHandler.GetRequestMessageContents(request, cancellationToken);

        return !Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri _)
            ? throw new SimulatedHttpTestException($"Url is not a proper Uri: {url}")
            : await base.SendAsync(request, cancellationToken);
    }
}
