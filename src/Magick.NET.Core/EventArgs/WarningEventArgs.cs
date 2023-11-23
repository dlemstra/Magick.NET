// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Arguments for the Warning event.
/// </summary>
/// <param name="exception">The warning that was raised.</param>
public sealed class WarningEventArgs(MagickWarningException exception) : EventArgs
{
    /// <summary>
    /// Gets the message of the exception.
    /// </summary>
    public string Message
        => Exception.Message;

    /// <summary>
    /// Gets the warning that was raised.
    /// </summary>
    public MagickWarningException Exception { get; } = exception;
}
