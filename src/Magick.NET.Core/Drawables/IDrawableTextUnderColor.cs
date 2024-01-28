// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Specifies the color of a background rectangle to place under text annotations.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IDrawableTextUnderColor<TQuantumType> : IDrawable
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the color to use.
    /// </summary>
    IMagickColor<TQuantumType> Color { get; }
}
