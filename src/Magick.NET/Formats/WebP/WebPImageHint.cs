// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies WebP image hint options.
    /// </summary>
    public enum WebPImageHint
    {
        /// <summary>
        /// Default preset.
        /// </summary>
        Default,

        /// <summary>
        /// Digital picture, like portrait, inner shot.
        /// </summary>
        Photo,

        /// <summary>
        /// Outdoor photograph, with natural lighting.
        /// </summary>
        Picture,

        /// <summary>
        /// Discrete tone image (graph, map-tile etc).
        /// </summary>
        Graph,
    }
}
