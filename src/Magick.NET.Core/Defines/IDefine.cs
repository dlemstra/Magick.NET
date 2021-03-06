// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Interface for a define.
    /// </summary>
    public interface IDefine
    {
        /// <summary>
        /// Gets the format to set the define for.
        /// </summary>
        MagickFormat Format { get; }

        /// <summary>
        /// Gets the name of the define.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the value of the define.
        /// </summary>
        string Value { get; }
    }
}