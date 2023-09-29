// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.Net;

namespace BlazorFocused.Testing.Http.Simulated;

internal class SimulatedHttpResponse : SimulatedHttpRequest
{
    public HttpStatusCode StatusCode { get; set; }

    public string ResponseContent { get; set; }
}
