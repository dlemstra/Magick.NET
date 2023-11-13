// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Represents a percentage value.
/// </summary>
public readonly struct Percentage : IEquatable<Percentage>, IComparable<Percentage>
{
    private readonly double _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Percentage"/> struct.
    /// </summary>
    /// <param name="value">The value (0% = 0.0, 100% = 100.0).</param>
    public Percentage(double value)
        => _value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Percentage"/> struct.
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100).</param>
    public Percentage(int value)
        => _value = value;

    /// <summary>
    /// Converts the specified double to an instance of this type.
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100).</param>
    public static explicit operator Percentage(double value)
        => new Percentage(value);

    /// <summary>
    /// Converts the specified int to an instance of this type.
    /// </summary>
    /// <param name="value">The value (0% = 0, 100% = 100).</param>
    public static explicit operator Percentage(int value)
        => new Percentage(value);

    /// <summary>
    /// Converts the specified <see cref="Percentage"/> to a double.
    /// </summary>
    /// <param name="percentage">The <see cref="Percentage"/> to convert.</param>
    public static explicit operator double(Percentage percentage)
        => percentage.ToDouble();

    /// <summary>
    /// Converts the <see cref="Percentage"/> to a quantum type.
    /// </summary>
    /// <param name="percentage">The <see cref="Percentage"/> to convert.</param>
    public static explicit operator int(Percentage percentage)
        => percentage.ToInt32();

    /// <summary>
    /// Determines whether the specified <see cref="Percentage"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator ==(Percentage left, Percentage right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="Percentage"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator !=(Percentage left, Percentage right)
        => !Equals(left, right);

    /// <summary>
    /// Determines whether the first <see cref="Percentage"/> is more than the second <see cref="Percentage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator >(Percentage left, Percentage right)
        => left.CompareTo(right) == 1;

    /// <summary>
    /// Determines whether the first <see cref="Percentage"/> is less than the second <see cref="Percentage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator <(Percentage left, Percentage right)
        => left.CompareTo(right) == -1;

    /// <summary>
    /// Determines whether the first <see cref="Percentage"/> is less than or equal to the second <see cref="Percentage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator >=(Percentage left, Percentage right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the first <see cref="Percentage"/> is less than or equal to the second <see cref="Percentage"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Percentage"/> to compare.</param>
    /// <param name="right">The second <see cref="Percentage"/> to compare.</param>
    public static bool operator <=(Percentage left, Percentage right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Multiplies the value by the <see cref="Percentage"/>.
    /// </summary>
    /// <param name="value">The value to use.</param>
    /// <param name="percentage">The <see cref="Percentage"/> to use.</param>
    public static double operator *(double value, Percentage percentage)
        => percentage.Multiply(value);

    /// <summary>
    /// Multiplies the value by the <see cref="Percentage"/>.
    /// </summary>
    /// <param name="value">The value to use.</param>
    /// <param name="percentage">The <see cref="Percentage"/> to use.</param>
    public static int operator *(int value, Percentage percentage)
        => percentage.Multiply(value);

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this <see cref="Percentage"/> with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(Percentage other)
        => _value.CompareTo(other._value);

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Percentage"/>.
    /// </summary>
    /// <param name="obj">The object to compare this <see cref="Percentage"/> with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="Percentage"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is Percentage percentage)
            return Equals(percentage);

        if (obj is double doubleObj)
            return _value.Equals(doubleObj);

        if (obj is int intObj)
            return intObj.Equals((int)_value);

        return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Percentage"/> is equal to the current <see cref="Percentage"/>.
    /// </summary>
    /// <param name="other">The <see cref="Percentage"/> to compare this <see cref="Percentage"/> with.</param>
    /// <returns>True when the specified <see cref="Percentage"/> is equal to the current <see cref="Percentage"/>.</returns>
    public bool Equals(Percentage other)
        => _value.Equals(other._value);

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
        => _value.GetHashCode();

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    /// <param name="value">The value to use.</param>
    /// <returns>the new value.</returns>
    public double Multiply(double value)
        => (value * _value) / 100.0;

    /// <summary>
    /// Multiplies the value by the percentage.
    /// </summary>
    /// <param name="value">The value to use.</param>
    /// <returns>the new value.</returns>
    public int Multiply(int value)
        => (int)((value * _value) / 100.0);

    /// <summary>
    /// Returns a double that represents the current percentage.
    /// </summary>
    /// <returns>A double that represents the current percentage.</returns>
    public double ToDouble()
        => _value;

    /// <summary>
    /// Returns an integer that represents the current percentage.
    /// </summary>
    /// <returns>An integer that represents the current percentage.</returns>
    public int ToInt32()
        => (int)Math.Round(_value, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Returns a string that represents the current percentage.
    /// </summary>
    /// <returns>A string that represents the current percentage.</returns>
    public override string ToString()
        => string.Format(CultureInfo.InvariantCulture, "{0:0.##}%", _value);
}
