// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick Warning exception.
    /// </summary>
    public class MagickWarningException : MagickException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickWarningException(string message)
          : base(message)
        {
        }
    }
}
