// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageError exception.
    /// </summary>
    public sealed class MagickImageErrorException : MagickErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImageErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickImageErrorException(string message)
          : base(message)
        {
        }
    }
}
