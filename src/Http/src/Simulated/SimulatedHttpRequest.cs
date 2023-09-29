// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Simulated;

internal class SimulatedHttpRequest
{
    public HttpMethod Method { get; set; }

    public string Url { get; set; }

    public string RequestContent { get; set; }
}
