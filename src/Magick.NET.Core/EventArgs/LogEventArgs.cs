// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// EventArgs for Log events.
    /// </summary>
    public sealed class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="eventType">The type of the log message.</param>
        /// <param name="message">The log message.</param>
        public LogEventArgs(LogEvents eventType, string? message)
        {
            EventType = eventType;
            Message = message;
        }

        /// <summary>
        /// Gets the type of the log message.
        /// </summary>
        public LogEvents EventType { get; }

        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string? Message { get; }
    }
}