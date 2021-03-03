// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    /// <typeparam name="TValueType">The type of the value.</typeparam>
    public interface IExifValue<TValueType> : IExifValue
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        TValueType Value { get; set; }
    }
}
