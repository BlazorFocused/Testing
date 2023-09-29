// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Testing.Logging.Test;

public class TestServiceLoggerException : Exception
{
    public TestServiceLoggerException(string message) : base(message) { }
}
