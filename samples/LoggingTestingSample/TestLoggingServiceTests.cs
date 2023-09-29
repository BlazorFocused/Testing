// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.Logging;
using BlazorFocused.Testing.Logging;
using Xunit.Abstractions;
using static LoggingTestingSample.TestLoggingService;

namespace LoggingTestingSample;

public class TestLoggingServiceTests
{
    private readonly TestLoggingService testLoggingService;
    private readonly ITestLogger<TestLoggingService> testLogger;

    public TestLoggingServiceTests(ITestOutputHelper testOutputHelper)
    {
        // Optional: You can pass in testOutputHelper to view actual logs being requested
        // Otherwise, just use TestLoggerBuilder.CreateTestLogger<TestLoggingService>()
        testLogger =
            TestLoggerBuilder.CreateTestLogger<TestLoggingService>((level, message, exception) =>
            {
                testOutputHelper.WriteLine("Level: {0}; Message: {1}; Exception: {2}", level, message, exception);
            });

        testLoggingService = new TestLoggingService(testLogger);
    }

    [Fact]
    public void CheckOrder_ShouldLogIdAndDescriptionOfRequest()
    {
        // Arrange
        int orderId = 50;
        string description = "This is a test description";

        // Act
        Order order = testLoggingService.CheckOrder(orderId, description);

        // Assert
        Assert.Equal(order.Id, orderId);
        Assert.Equal(order.Description, description);

        // Ensure proper details of order request logged
        testLogger.VerifyWasCalledWith(LogLevel.Information, "Checking Order 50 - This is a test description");
    }

    [Fact]
    public void CheckOrder_ShouldLogErrorForInvalidId()
    {
        int orderId = -1;
        string description = "This is a test description (request should fail)";

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            testLoggingService.CheckOrder(orderId, description));

        // Internally doing a "contains" on the log error message
        testLogger.VerifyWasCalledWith(LogLevel.Error, "Failed to check order -1 - Invalid Order ID");
    }
}
