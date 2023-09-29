// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace LoggingTestingSample;

public class TestLoggingService
{
    private readonly ILogger<TestLoggingService> logger;

    public record Order(int Id, string Description);

    public TestLoggingService(ILogger<TestLoggingService> logger)
    {
        this.logger = logger;
    }

    public Order CheckOrder(int orderId, string orderDescription)
    {
        logger.LogInformation("Checking Order {Id} - {Description}", orderId, orderDescription);

        try
        {
            return orderId < 0
                ? throw new ArgumentOutOfRangeException(nameof(orderId), "Invalid Order ID")
                : new Order(Id: orderId, Description: orderDescription);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to check order {Id} - {Message}", orderId, ex.Message);

            throw;
        }
    }
}
