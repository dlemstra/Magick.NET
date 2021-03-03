// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick StreamError exception.
    /// </summary>
    public sealed class MagickStreamErrorException : MagickErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickStreamErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickStreamErrorException(string message)
          : base(message)
        {
        }
    }
}
