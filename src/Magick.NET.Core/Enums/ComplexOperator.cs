// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies a kind of complex operator.
/// </summary>
public enum ComplexOperator
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Add.
    /// </summary>
    Add,

    /// <summary>
    /// Conjugate.
    /// </summary>
    Conjugate,

    /// <summary>
    /// Divide.
    /// </summary>
    Divide,

    /// <summary>
    /// Magnitude phase.
    /// </summary>
    MagnitudePhase,

    /// <summary>
    /// Multiply.
    /// </summary>
    Multiply,

    /// <summary>
    /// Real imaginary.
    /// </summary>
    RealImaginary,

    /// <summary>
    /// Subtract.
    /// </summary>
    Subtract,
}
