// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick Error exception.
    /// </summary>
    public class MagickErrorException : MagickException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickErrorException(string message)
          : base(message)
        {
        }
    }
}
