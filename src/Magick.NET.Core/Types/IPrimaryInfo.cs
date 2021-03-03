// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// PrimaryInfo information.
    /// </summary>
    public interface IPrimaryInfo : IEquatable<IPrimaryInfo>
    {
        /// <summary>
        /// Gets the X value.
        /// </summary>
        double X { get; }

        /// <summary>
        /// Gets the Y value.
        /// </summary>
        double Y { get; }

        /// <summary>
        /// Gets the Z value.
        /// </summary>
        double Z { get; }
    }
}
