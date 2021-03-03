// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick BlobWarning exception.
    /// </summary>
    public sealed class MagickBlobWarningException : MagickWarningException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickBlobWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickBlobWarningException(string message)
          : base(message)
        {
        }
    }
}
