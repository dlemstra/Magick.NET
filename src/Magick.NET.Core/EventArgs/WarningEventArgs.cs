// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Arguments for the Warning event.
    /// </summary>
    public sealed class WarningEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarningEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The MagickWarningException that was thrown.</param>
        public WarningEventArgs(MagickWarningException exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets the message of the exception.
        /// </summary>
        public string Message => Exception.Message;

        /// <summary>
        /// Gets the MagickWarningException that was thrown.
        /// </summary>
        public MagickWarningException Exception { get; }
    }
}
