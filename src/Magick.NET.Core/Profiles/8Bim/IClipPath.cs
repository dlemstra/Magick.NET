// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Xml.XPath;

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    public interface IClipPath
    {
        /// <summary>
        /// Gets the name of the clipping path.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the path of the clipping path.
        /// </summary>
        IXPathNavigable Path { get; }
    }
}
