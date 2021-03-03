// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick PolicyError exception.
    /// </summary>
    public sealed class MagickPolicyErrorException : MagickErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickPolicyErrorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickPolicyErrorException(string message)
          : base(message)
        {
        }
    }
}
