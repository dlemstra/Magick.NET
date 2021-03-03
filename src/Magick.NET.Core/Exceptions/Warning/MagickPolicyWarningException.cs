// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick PolicyWarning exception.
    /// </summary>
    public sealed class MagickPolicyWarningException : MagickErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickPolicyWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickPolicyWarningException(string message)
          : base(message)
        {
        }
    }
}
