// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Interface that describes an 8bim profile.
    /// </summary>
    public interface IEightBimProfile : IImageProfile
    {
        /// <summary>
        /// Gets the clipping paths this image contains.
        /// </summary>
        IEnumerable<IClipPath> ClipPaths { get; }

        /// <summary>
        /// Gets the values of this 8bim profile.
        /// </summary>
        IEnumerable<IEightBimValue> Values { get; }
    }
}