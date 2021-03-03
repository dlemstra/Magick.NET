// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Defines
{
    /// <summary>
    /// Base class that can create read defines.
    /// </summary>
    public abstract class ReadDefinesCreator : DefinesCreator, IReadDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadDefinesCreator"/> class.
        /// </summary>
        /// <param name="format">The format where the defines are for.</param>
        protected ReadDefinesCreator(MagickFormat format)
          : base(format)
        {
        }
    }
}