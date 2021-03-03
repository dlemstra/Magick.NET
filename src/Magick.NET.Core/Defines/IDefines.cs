// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Interface for an object that specifies defines for an image.
    /// </summary>
    public interface IDefines
    {
        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        IEnumerable<IDefine> Defines { get; }
    }
}