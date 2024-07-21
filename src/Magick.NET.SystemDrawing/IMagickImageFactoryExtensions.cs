// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;

namespace ImageMagick.Factories;

/// <summary>
/// Extension methods for the <see cref="IMagickImageFactory{QuantumType}"/> interface.
/// </summary>
public static class IMagickImageFactoryExtensions
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickImageFactory{TQuantumType}"/>.
    /// </summary>
    /// <param name="self">The image factory.</param>
    /// <param name="bitmap">The bitmap to use.</param>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>A new <see cref="IMagickImage{QuantumType}"/> instance.</returns>
    public static IMagickImage<TQuantumType> Create<TQuantumType>(this IMagickImageFactory<TQuantumType> self, Bitmap bitmap)
        where TQuantumType : struct, IConvertible
    {
        Throw.IfNull(nameof(self), self);

        var image = self.Create();
        image.Read(bitmap);

        return image;
    }
}
