// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick CoderWarning exception.
    /// </summary>
    public sealed class MagickCoderWarningException : MagickWarningException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickCoderWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickCoderWarningException(string message)
          : base(message)
        {
        }
    }
}
