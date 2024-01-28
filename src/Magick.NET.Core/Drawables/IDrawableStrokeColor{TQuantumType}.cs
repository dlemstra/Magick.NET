// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Sets the color used for stroking object outlines.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IDrawableStrokeColor<TQuantumType> : IDrawable
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the color to use.
    /// </summary>
    IMagickColor<TQuantumType> Color { get; }
}
