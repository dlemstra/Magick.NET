// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Colors;

/// <summary>
/// Base class for colors.
/// </summary>
public abstract class ColorBase : IEquatable<ColorBase?>, IComparable<ColorBase?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBase"/> class.
    /// </summary>
    /// <param name="color">The color to use.</param>
    protected ColorBase(IMagickColor<QuantumType> color)
    {
        Throw.IfNull(nameof(color), color);

        Color = color;
    }

    /// <summary>
    /// Gets the actual color of this instance.
    /// </summary>
    protected IMagickColor<QuantumType> Color { get; }

    /// <summary>
    /// Determines whether the specified <see cref="ColorBase"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator ==(ColorBase? left, ColorBase? right) => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="ColorBase"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator !=(ColorBase? left, ColorBase? right) => !Equals(left, right);

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is more than the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator >(ColorBase? left, ColorBase? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is less than the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator <(ColorBase? left, ColorBase? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is more than or equal to the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator >=(ColorBase? left, ColorBase? right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is less than or equal to the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right">The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator <=(ColorBase? left, ColorBase? right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this color with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(ColorBase? other)
    {
        if (other is null)
            return 1;

        UpdateColor();
        other.UpdateColor();

        return Color.CompareTo(other.Color);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare this color with.</param>
    /// <returns>True when the specified object is equal to the current instance.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as ColorBase);

    /// <summary>
    /// Determines whether the specified color is equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <returns>True when the specified color is equal to the current instance.</returns>
    public bool Equals(ColorBase? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        UpdateColor();
        other.UpdateColor();

        return Color.Equals(other.Color);
    }

    /// <summary>
    /// Determines whether the specified color is fuzzy equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <param name="fuzz">The fuzz factor.</param>
    /// <returns>True when the specified color is fuzzy equal to the current instance.</returns>
    public bool FuzzyEquals(ColorBase? other, Percentage fuzz)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        UpdateColor();
        other.UpdateColor();

        return Color.FuzzyEquals(other.Color, fuzz);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        UpdateColor();

        return Color.GetHashCode();
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="IMagickColor{QuantumType}"/>.
    /// </summary>
    /// <returns>A <see cref="IMagickColor{QuantumType}"/> instance.</returns>
    public IMagickColor<QuantumType> ToMagickColor()
    {
        UpdateColor();

        return new MagickColor(Color);
    }

    /// <summary>
    /// Converts the value of this instance to a hexadecimal string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
        => ToMagickColor().ToString();

    /// <summary>
    /// Updates the color value from an inherited class.
    /// </summary>
    protected virtual void UpdateColor()
    {
    }
}
