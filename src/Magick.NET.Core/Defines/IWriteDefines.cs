// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Interface for defines that are used when writing an image.
    /// </summary>
    public interface IWriteDefines : IDefines
    {
        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        MagickFormat Format { get; }
    }
}