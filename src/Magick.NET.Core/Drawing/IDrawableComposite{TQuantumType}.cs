// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the DrawableCompositeImage object.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IDrawableComposite<TQuantumType> : IDrawable
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the X coordinate.
    /// </summary>
    double X { get; }

    /// <summary>
    /// Gets the Y coordinate.
    /// </summary>
    double Y { get; }

    /// <summary>
    /// Gets the width to scale the image to.
    /// </summary>
    double Width { get; }

    /// <summary>
    /// Gets the height to scale the image to.
    /// </summary>
    double Height { get; }

    /// <summary>
    /// Gets the composition operator.
    /// </summary>
    CompositeOperator Compose { get; }

    /// <summary>
    /// Gets the composite image.
    /// </summary>
    IMagickImage<TQuantumType> Image { get; }
}
