// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Http.Simulated;

/// <summary>
/// Exception given when request was not verified with
/// <see cref="ISimulatedHttp"/>
/// </summary>
internal class SimulatedHttpTestException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="SimulatedHttpTestException"/>
    /// with exception message
    /// </summary>
    /// <param name="message">Description of error that occurred</param>
    public SimulatedHttpTestException(string message)
        : base(message) { }
}
