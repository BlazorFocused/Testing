// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Testing.Http.Simulated;

namespace BlazorFocused.Testing.Http;

/// <summary>
/// Provides implementations of <see cref="ISimulatedHttp"/> for testing
/// </summary>
public static class SimulatedHttpBuilder
{
    /// <summary>
    /// Provides implementations of <see cref="ISimulatedHttp"/> for testing
    /// </summary>
    /// <param name="baseAddress">Base address of <see cref="System.Net.Http.HttpClient"/> for all requests</param>
    /// <returns><see cref="ISimulatedHttp"/> used to setup/verify Http requests/responses</returns>
    public static ISimulatedHttp CreateSimulatedHttp(string baseAddress = null) => baseAddress is not null ?
            new SimulatedHttp(baseAddress) : new SimulatedHttp();
}
