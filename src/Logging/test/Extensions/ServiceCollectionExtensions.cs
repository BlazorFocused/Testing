// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Testing.Logging.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace BlazorFocused.Testing.Logging.Test.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceProvider BuildProviderWithTestLogger<T>(
        this IServiceCollection serviceCollection,
        ITestOutputHelper testOutputHelper)
    {
        ServiceDescriptor previousLogger = serviceCollection.FirstOrDefault(descriptor =>
            descriptor.ServiceType == typeof(ILogger<>));

        serviceCollection.Remove(previousLogger);

        serviceCollection.AddTestLoggerToCollection<T>(testOutputHelper);

        return serviceCollection.BuildServiceProvider();
    }

    public static IServiceProvider BuildProviderWithTestLoggers(
        this IServiceCollection serviceCollection,
        Action<IServiceCollection> testLoggers)
    {
        ServiceDescriptor previousLogger = serviceCollection.FirstOrDefault(descriptor =>
            descriptor.ServiceType == typeof(ILogger<>));

        serviceCollection.Remove(previousLogger);

        testLoggers(serviceCollection);

        return serviceCollection.BuildServiceProvider();
    }

    public static void AddTestLoggerToCollection<T>(
        this IServiceCollection serviceCollection,
        ITestOutputHelper testOutputHelper)
    {
        void logAction(LogLevel level, string message, Exception exception)
        {
            testOutputHelper.WriteTestLoggerMessage(level, message, exception);
        }

        serviceCollection.AddSingleton<ILogger<T>>(new TestLogger<T>(logAction));
    }
}
