// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Struct for a threshold with a minimum and maximum.
/// </summary>
public readonly struct Threshold : IEquatable<Threshold>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Threshold"/> struct.
    /// </summary>
    /// <param name="minimum">The minimum of the threshold.</param>
    public Threshold(double minimum)
    {
        Minimum = minimum;
        Maximum = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Threshold"/> struct.
    /// </summary>
    /// <param name="minimum">The minimum of the threshold.</param>
    /// <param name="maximum">The maximum of the threshold.</param>
    public Threshold(double minimum, double maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    /// <summary>
    /// Gets the minimum of this <see cref="Threshold"/>.
    /// </summary>
    public double Minimum { get; }

    /// <summary>
    /// Gets the y-coordinate of this <see cref="Threshold"/>.
    /// </summary>
    public double Maximum { get; }

    /// <summary>
    /// Determines whether the specified <see cref="Threshold"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Threshold"/> to compare.</param>
    /// <param name="right">The second <see cref="Threshold"/> to compare.</param>
    public static bool operator ==(Threshold left, Threshold right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="Threshold"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Threshold"/> to compare.</param>
    /// <param name="right">The second <see cref="Threshold"/> to compare.</param>
    public static bool operator !=(Threshold left, Threshold right)
        => !Equals(left, right);

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Threshold"/>.
    /// </summary>
    /// <param name="obj">The object to compare this <see cref="Threshold"/> with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="Threshold"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Threshold other)
            return false;

        return Equals(other);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Threshold"/> is equal to the current <see cref="Threshold"/>.
    /// </summary>
    /// <param name="other">The <see cref="Threshold"/> to compare this <see cref="Threshold"/> with.</param>
    /// <returns>True when the specified <see cref="Threshold"/> is equal to the current <see cref="Threshold"/>.</returns>
    public bool Equals(Threshold other)
        => Minimum == other.Minimum && Maximum == other.Maximum;

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
        => Minimum.GetHashCode() ^ Maximum.GetHashCode();

    /// <summary>
    /// Returns a string that represents the current PointD.
    /// </summary>
    /// <returns>A string that represents the current PointD.</returns>
    public override string ToString()
    {
        if (Maximum == 0)
            return Minimum.ToString(CultureInfo.InvariantCulture);
        else
            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", Minimum, Maximum);
    }
}
