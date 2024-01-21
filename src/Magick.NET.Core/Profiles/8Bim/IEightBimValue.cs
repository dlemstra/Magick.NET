// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;

namespace ImageMagick;

/// <summary>
/// A value of the 8bim profile.
/// </summary>
public interface IEightBimValue : IEquatable<IEightBimValue?>
{
    /// <summary>
    /// Gets the id of the 8bim value.
    /// </summary>
    short Id { get; }

    /// <summary>
    /// Gets the ID of the 8bim value.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Id)} instead.")]
    short ID { get; }

    /// <summary>
    /// Gets the name of the 8bim value.
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[] ToByteArray();

    /// <summary>
    /// Returns a string that represents the current value with the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    /// <returns>A string that represents the current value with the specified encoding.</returns>
    string ToString(Encoding encoding);
}
