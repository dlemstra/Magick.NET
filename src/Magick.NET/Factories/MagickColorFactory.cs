// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
public sealed class MagickColorFactory : IMagickColorFactory<QuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> Create()
        => new MagickColor();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    /// <param name="color">The color to use.</param>
    public IMagickColor<QuantumType> Create(IMagickColor<QuantumType> color)
        => new MagickColor(color);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> Create(QuantumType red, QuantumType green, QuantumType blue)
        => new MagickColor(red, green, blue);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <param name="red">Red component value of this color.</param>
    /// <param name="green">Green component value of this color.</param>
    /// <param name="blue">Blue component value of this color.</param>
    /// <param name="alpha">Alpha component value of this color.</param>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> Create(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
        => new MagickColor(red, green, blue, alpha);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <param name="cyan">Cyan component value of this color.</param>
    /// <param name="magenta">Magenta component value of this color.</param>
    /// <param name="yellow">Yellow component value of this color.</param>
    /// <param name="black">Black component value of this color.</param>
    /// <param name="alpha">Alpha component value of this colors.</param>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> Create(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType black, QuantumType alpha)
        => new MagickColor(cyan, magenta, yellow, black, alpha);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
    /// </summary>
    /// <param name="color">The RGBA/CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
    /// For example: #F000, #FF000000, #FFFF000000000000.</param>
    /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> Create(string color)
        => new MagickColor(color);
}
