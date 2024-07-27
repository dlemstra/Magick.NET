// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Encapsulates a color matrix in the order of 1 to 6 (1x1 through 6x6).
/// </summary>
public sealed class MagickColorMatrix : DoubleMatrix, IMagickColorMatrix
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColorMatrix"/> class.
    /// </summary>
    /// <param name="order">The order (1 to 6).</param>
    public MagickColorMatrix(uint order)
      : base(order, null)
    {
        CheckOrder(order);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickColorMatrix"/> class.
    /// </summary>
    /// <param name="order">The order (1 to 6).</param>
    /// <param name="values">The values to initialize the matrix with.</param>
    public MagickColorMatrix(uint order, params double[] values)
      : base(order, values)
    {
        CheckOrder(order);
    }

    private static void CheckOrder(uint order)
        => Throw.IfTrue(nameof(order), order > 6, "Invalid order specified, range 1-6.");
}
