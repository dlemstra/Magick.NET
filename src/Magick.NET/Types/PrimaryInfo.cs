// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// PrimaryInfo information.
/// </summary>
public partial class PrimaryInfo : IPrimaryInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrimaryInfo"/> class.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    public PrimaryInfo(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    private PrimaryInfo(IntPtr instance)
    {
        var nativeInstance = new NativePrimaryInfo(instance);
        X = nativeInstance.X_Get();
        Y = nativeInstance.Y_Get();
        Z = nativeInstance.Z_Get();
    }

    /// <summary>
    /// Gets the X value.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets the Y value.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Gets the Z value.
    /// </summary>
    public double Z { get; }

    /// <summary>
    /// Determines whether the specified <see cref="IPrimaryInfo"/> is equal to the current <see cref="PrimaryInfo"/>.
    /// </summary>
    /// <param name="other">The <see cref="IPrimaryInfo"/> to compare this <see cref="PrimaryInfo"/> with.</param>
    /// <returns>True when the specified <see cref="IPrimaryInfo"/> is equal to the current <see cref="PrimaryInfo"/>.</returns>
    public bool Equals(IPrimaryInfo? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return
          X == other.X &&
          Y == other.Y &&
          Z == other.Z;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return
          X.GetHashCode() ^
          Y.GetHashCode() ^
          Z.GetHashCode();
    }
}
