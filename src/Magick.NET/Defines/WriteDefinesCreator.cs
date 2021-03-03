// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Defines
{
    /// <summary>
    /// Base class that can create write defines.
    /// </summary>
    public abstract class WriteDefinesCreator : DefinesCreator, IWriteDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteDefinesCreator"/> class.
        /// </summary>
        /// <param name="format">The format where the defines are for.</param>
        protected WriteDefinesCreator(MagickFormat format)
          : base(format)
        {
        }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        MagickFormat IWriteDefines.Format => Format;
    }
}