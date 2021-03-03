// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create various instances.
    /// </summary>
    public interface IMagickFactory
    {
        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickGeometry"/> instances.
        /// </summary>
        IMagickGeometryFactory Geometry { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImageInfo"/> instances.
        /// </summary>
        IMagickImageInfoFactory ImageInfo { get; }

        /// <summary>
        /// Gets a factory that can be used to create various matrix instances.
        /// </summary>
        IMatrixFactory Matrix { get; }
    }
}
