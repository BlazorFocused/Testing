// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Test;

public interface ITestHttpService
{
    HttpClient HttpClient { get; }

    ValueTask<T> GetValueAsync<T>(string url);
}
