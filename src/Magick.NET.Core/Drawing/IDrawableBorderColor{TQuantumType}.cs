// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Sets the border color to be used for drawing bordered objects.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IDrawableBorderColor<TQuantumType> : IDrawable
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the color to use.
    /// </summary>
    IMagickColor<TQuantumType> Color { get; }
}
