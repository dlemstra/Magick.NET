// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Represents a number that can be expressed as a fraction.
/// </summary>
/// <remarks>
/// This is a very simplified implementation of a rational number designed for use with metadata only.
/// </remarks>
public readonly struct Rational : IEquatable<Rational>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Rational"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
    public Rational(double value)
      : this(value, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rational"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
    /// <param name="bestPrecision">Specifies if the instance should be created with the best precision possible.</param>
    public Rational(double value, bool bestPrecision)
    {
        var rational = new BigRational(Math.Abs(value), bestPrecision);

        Numerator = (uint)rational.Numerator;
        Denominator = (uint)rational.Denominator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rational"/> struct.
    /// </summary>
    /// <param name="value">The integer to create the rational from.</param>
    public Rational(uint value)
      : this(value, 1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rational"/> struct.
    /// </summary>
    /// <param name="numerator">The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken.</param>
    /// <param name="denominator">The number below the line in a vulgar fraction; a divisor.</param>
    public Rational(uint numerator, uint denominator)
      : this(numerator, denominator, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rational"/> struct.
    /// </summary>
    /// <param name="numerator">The number above the line in a vulgar fraction showing how many of the parts indicated by the denominator are taken.</param>
    /// <param name="denominator">The number below the line in a vulgar fraction; a divisor.</param>
    /// <param name="simplify">Specified if the rational should be simplified.</param>
    public Rational(uint numerator, uint denominator, bool simplify)
    {
        var rational = new BigRational(numerator, denominator, simplify);

        Numerator = (uint)rational.Numerator;
        Denominator = (uint)rational.Denominator;
    }

    /// <summary>
    /// Gets the numerator of a number.
    /// </summary>
    public uint Numerator { get; }

    /// <summary>
    /// Gets the denominator of a number.
    /// </summary>
    public uint Denominator { get; }

    /// <summary>
    /// Determines whether the specified <see cref="Rational"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rational"/>  to compare.</param>
    /// <param name="right">The second <see cref="Rational"/>  to compare.</param>
    public static bool operator ==(Rational left, Rational right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="Rational"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Rational"/> to compare.</param>
    /// <param name="right">The second <see cref="Rational"/> to compare.</param>
    public static bool operator !=(Rational left, Rational right)
        => !Equals(left, right);

    /// <summary>
    /// Converts the specified <see cref="double"/> to an instance of this type.
    /// </summary>
    /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
    /// <returns>The <see cref="Rational"/>.</returns>
    public static Rational FromDouble(double value)
        => new Rational(value, false);

    /// <summary>
    /// Converts the specified <see cref="double"/> to an instance of this type.
    /// </summary>
    /// <param name="value">The <see cref="double"/> to convert to an instance of this type.</param>
    /// <param name="bestPrecision">Specifies if the instance should be created with the best precision possible.</param>
    /// <returns>The <see cref="Rational"/>.</returns>
    public static Rational FromDouble(double value, bool bestPrecision)
        => new Rational(value, bestPrecision);

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to this <see cref="Rational"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare this <see cref="Rational"/> with.</param>
    /// <returns>True when the specified <see cref="object"/> is equal to this <see cref="Rational"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Rational other)
            return Equals(other);

        return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Rational"/> is equal to this <see cref="Rational"/>.
    /// </summary>
    /// <param name="other">The <see cref="Rational"/> to compare this <see cref="Rational"/> with.</param>
    /// <returns>True when the specified <see cref="Rational"/> is equal to this <see cref="Rational"/>.</returns>
    public bool Equals(Rational other)
    {
        var left = new BigRational(Numerator, Denominator);
        var right = new BigRational(other.Numerator, other.Denominator);

        return left.Equals(right);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        var self = new BigRational(Numerator, Denominator);
        return self.GetHashCode();
    }

    /// <summary>
    /// Converts a rational number to the nearest <see cref="double"/>.
    /// </summary>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    public double ToDouble()
        => Numerator / (double)Denominator;

    /// <summary>
    /// Converts the numeric value of this instance to its equivalent string representation.
    /// </summary>
    /// <returns>A string representation of this value.</returns>
    public override string ToString()
        => ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts the numeric value of this instance to its equivalent string representation using
    /// the specified culture-specific format information.
    /// </summary>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>A string representation of this value.</returns>
    public string ToString(IFormatProvider provider)
    {
        var rational = new BigRational(Numerator, Denominator);
        return rational.ToString(provider);
    }
}
