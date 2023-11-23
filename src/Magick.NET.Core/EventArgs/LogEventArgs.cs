// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// EventArgs for Log events.
/// </summary>
/// <param name="eventType">The type of the log message.</param>
/// <param name="message">The log message.</param>
public sealed class LogEventArgs(LogEvents eventType, string? message) : EventArgs
{
    /// <summary>
    /// Gets the type of the log message.
    /// </summary>
    public LogEvents EventType { get; } = eventType;

    /// <summary>
    /// Gets the log message.
    /// </summary>
    public string? Message { get; } = message;
}
