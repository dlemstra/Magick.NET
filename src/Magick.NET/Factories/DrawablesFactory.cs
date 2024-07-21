// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create <see cref="IMagickColor{QuantumType}"/> instances.
/// </summary>
public sealed class DrawablesFactory : IDrawablesFactory<QuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IDrawables{QuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IDrawables{QuantumType}"/> instance.</returns>
    public IDrawables<QuantumType> Create()
         => new Drawables();
}
