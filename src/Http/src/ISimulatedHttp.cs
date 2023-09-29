// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http;

/// <summary>
/// Simulates <see cref="System.Net.Http.HttpClient"/> transactions for testing
/// and providing mock data
/// </summary>
public interface ISimulatedHttp
{
    /// <summary>
    /// Handler that can be passed into <see cref="System.Net.Http.HttpClient"/> for
    /// making simulated requests
    /// </summary>
    DelegatingHandler DelegatingHandler { get; }

    /// <summary>
    /// Returns <see cref="System.Net.Http.HttpClient"/> to perform requests
    /// </summary>
    HttpClient HttpClient { get; }

    /// <summary>
    /// Retrieve header values under specified key for a given request
    /// </summary>
    /// <param name="method">Http Method of which request was made</param>
    /// <param name="url">Url of which request was made</param>
    /// <param name="key">Header key for values to obtain</param>
    /// <returns>List of header key/value pairs to verify</returns>
    IEnumerable<string> GetRequestHeaderValues(HttpMethod method, string url, string key);

    /// <summary>
    /// Add headers to simulated response
    /// </summary>
    /// <param name="key">Response header key</param>
    /// <param name="value">Response header value</param>
    void AddResponseHeader(string key, string value);

    /// <summary>
    /// Begin setup for an expected http request that will be used
    /// </summary>
    /// <param name="httpMethod">Expected <see cref="HttpMethod"/> of request</param>
    /// <param name="url">Expected absolute or relative url of request</param>
    /// <param name="content">Expected request body</param>
    /// <returns>Builder for setting additional configuration to handle simulated request object</returns>
    ISimulatedHttpSetup Setup(HttpMethod httpMethod, string url, object content = null);

    /// <summary>
    /// Verify that the expected request parameters were made
    /// </summary>
    /// <param name="httpMethod">Expected <see cref="HttpMethod"/> of request</param>
    /// <param name="url">Expected absolute or relative url of request</param>
    /// <param name="content">Expected request body</param>
    void VerifyWasCalled(HttpMethod httpMethod, string url = default, object content = default);
}
