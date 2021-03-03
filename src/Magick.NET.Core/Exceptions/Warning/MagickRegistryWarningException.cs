// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick RegistryWarning exception.
    /// </summary>
    public sealed class MagickRegistryWarningException : MagickWarningException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickRegistryWarningException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MagickRegistryWarningException(string message)
          : base(message)
        {
        }
    }
}
