// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Test;

public class TestHttpService : ITestHttpService
{
    public HttpClient HttpClient { get; private set; }

    public TestHttpService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public ValueTask<T> GetValueAsync<T>(string url) => new(default(T));
}
