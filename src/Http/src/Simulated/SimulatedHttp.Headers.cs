// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Simulated;

internal partial class SimulatedHttp
{
    public IEnumerable<string> GetRequestHeaderValues(HttpMethod method, string url, string key)
    {
        SimulatedHttpHeaders match = requestHeaders
                .Where(request => request.Method == method && request.Url == GetFullUrl(url))
                .FirstOrDefault();

        if (match is not null)
        {
            bool containsKey = match.Headers.TryGetValue(key, out IEnumerable<string> values);

            if (containsKey)
            {
                return values;
            }
        }

        return Enumerable.Empty<string>();
    }

    public void AddResponseHeader(string key, string value)
    {
        if (ResponseHeaders.TryGetValue(key, out List<string> valueList))
        {
            valueList.Add(value);
        }
        else
        {
            ResponseHeaders.Add(key, new List<string> { value });
        }
    }
}
