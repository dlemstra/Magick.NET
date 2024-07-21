// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create various matrix instances.
/// </summary>
public sealed class MatrixFactory : IMatrixFactory
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColorMatrix"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickColorMatrix"/> instance.</returns>
    /// <param name="order">The order (1 to 6).</param>
    public IMagickColorMatrix CreateColorMatrix(int order)
         => new MagickColorMatrix(order);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickColorMatrix"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickColorMatrix"/> instance.</returns>
    /// <param name="order">The order (1 to 6).</param>
    /// <param name="values">The values to initialize the matrix with.</param>
    public IMagickColorMatrix CreateColorMatrix(int order, params double[] values)
        => new MagickColorMatrix(order, values);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IConvolveMatrix"/>.
    /// </summary>
    /// <returns>A new <see cref="IConvolveMatrix"/> instance.</returns>
    /// <param name="order">The order (odd number).</param>
    public IConvolveMatrix CreateConvolveMatrix(int order)
        => new ConvolveMatrix(order);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IConvolveMatrix"/>.
    /// </summary>
    /// <returns>A new <see cref="IConvolveMatrix"/> instance.</returns>
    /// <param name="order">The order (odd number).</param>
    /// <param name="values">The values to initialize the matrix with.</param>
    public IConvolveMatrix CreateConvolveMatrix(int order, params double[] values)
        => new ConvolveMatrix(order, values);
}
