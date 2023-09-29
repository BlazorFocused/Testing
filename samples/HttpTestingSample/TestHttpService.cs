// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.Net.Http.Json;
using System.Text.Json;

namespace HttpTestingSample;

public class TestHttpService
{
    private readonly HttpClient httpClient;

    public TestHttpService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<T> GetResponseObjectAsync<T>(string url) =>
        await httpClient.GetFromJsonAsync<T>(url);

    public async Task<TOutput> PostRequestAsync<TInput, TOutput>(string url, TInput content)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(url, content);

        httpResponseMessage.EnsureSuccessStatusCode();

        Stream responseBodyContent = await httpResponseMessage.Content.ReadAsStreamAsync();

        return JsonSerializer.Deserialize<TOutput>(responseBodyContent);
    }

}
