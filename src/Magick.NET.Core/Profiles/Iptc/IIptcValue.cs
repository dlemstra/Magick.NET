// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the iptc profile.
    /// </summary>
    public interface IIptcValue : IEquatable<IIptcValue>
    {
        /// <summary>
        /// Gets the tag of the iptc value.
        /// </summary>
        IptcTag Tag { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Gets the length of the value.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Converts this instance to a byte array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray();

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        /// <returns>A string that represents the current value.</returns>
        string ToString();
    }
}
