// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Encapsulates a matrix of doubles.
/// </summary>
public interface IDoubleMatrix
{
    /// <summary>
    /// Gets the order of the matrix.
    /// </summary>
    uint Order { get; }

    /// <summary>
    /// Get or set the value at the specified x/y position.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    double this[int x, int y] { get; set; }

    /// <summary>
    /// Gets the value at the specified x/y position.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    /// <returns>The value at the specified x/y position.</returns>
    double GetValue(int x, int y);

    /// <summary>
    /// Set the column at the specified x position.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="values">The values.</param>
    void SetColumn(int x, params double[] values);

    /// <summary>
    /// Set the row at the specified y position.
    /// </summary>
    /// <param name="y">The y position.</param>
    /// <param name="values">The values.</param>
    void SetRow(int y, params double[] values);

    /// <summary>
    /// Set the value at the specified x/y position.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    /// <param name="value">The value.</param>
    void SetValue(int x, int y, double value);

    /// <summary>
    /// Returns a string that represents the current DoubleMatrix.
    /// </summary>
    /// <returns>The double array.</returns>
    double[] ToArray();
}
