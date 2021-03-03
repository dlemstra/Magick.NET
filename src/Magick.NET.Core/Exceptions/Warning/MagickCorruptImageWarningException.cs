// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick CorruptImageWarning exception.
    /// </summary>
    public sealed class MagickCorruptImageWarningException : MagickWarningException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickCorruptImageWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickCorruptImageWarningException(string message)
          : base(message)
        {
        }
    }
}
