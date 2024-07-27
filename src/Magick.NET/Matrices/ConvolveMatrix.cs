// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Encapsulates a convolution kernel.
/// </summary>
public sealed class ConvolveMatrix : DoubleMatrix, IConvolveMatrix
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConvolveMatrix"/> class.
    /// </summary>
    /// <param name="order">The order (odd number).</param>
    public ConvolveMatrix(uint order)
      : base(order, null)
    {
        CheckOrder(order);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConvolveMatrix"/> class.
    /// </summary>
    /// <param name="order">The order (odd number).</param>
    /// <param name="values">The values to initialize the matrix with.</param>
    public ConvolveMatrix(uint order, params double[] values)
      : base(order, values)
    {
        CheckOrder(order);
    }

    private static void CheckOrder(uint order)
        => Throw.IfTrue(nameof(order), order % 2 == 0, "Order must be an odd number.");
}
