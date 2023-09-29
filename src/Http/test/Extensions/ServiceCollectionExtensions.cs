// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorFocused.Testing.Http.Test.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        Dictionary<string, string> appSettings = default)
    {
        var configurationBuilder = new ConfigurationBuilder();

        if (appSettings is not null)
        {
            configurationBuilder.AddInMemoryCollection(appSettings);
        }

        return services.AddSingleton<IConfiguration>(configurationBuilder.Build());
    }
}
