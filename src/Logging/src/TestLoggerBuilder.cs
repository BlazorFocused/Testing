// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Testing.Logging.Logger;
using Microsoft.Extensions.Logging;

namespace BlazorFocused.Testing.Logging;

/// <summary>
/// Provides implementations of <see cref="ITestLogger{T}"/> for testing
/// </summary>
public static class TestLoggerBuilder
{
    /// <summary>
    /// Provides implementations of <see cref="ITestLogger{T}"/> for testing
    /// </summary>
    /// <typeparam name="T">Logger class used in test (<see cref="ILogger{T}"/>)</typeparam>
    /// <param name="logAction">Optional: Execute this action every time a log occurs.
    /// This is useful when using test output helpers (i.e. XUnit ITestOutputHelper)
    /// or event tracking
    /// </param>
    /// <returns><see cref="ITestLogger{T}"/> used to verify logging checks/tests</returns>
    public static ITestLogger<T> CreateTestLogger<T>(
        Action<LogLevel, string, Exception> logAction = null) => new TestLogger<T>(logAction);
}
