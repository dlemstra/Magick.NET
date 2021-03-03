// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick CacheError exception.
    /// </summary>
    public sealed class MagickCacheErrorException : MagickErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickCacheErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickCacheErrorException(string message)
          : base(message)
        {
        }
    }
}
