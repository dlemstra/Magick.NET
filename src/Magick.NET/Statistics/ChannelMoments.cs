// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// The normalized moments of one image channels.
/// </summary>
public sealed partial class ChannelMoments : IChannelMoments
{
    private double[] _huInvariants;

    private ChannelMoments(PixelChannel channel, IntPtr instance)
    {
        Channel = channel;

        var nativeInstance = new NativeChannelMoments(instance);
        Centroid = nativeInstance.Centroid_Get().ToPointD();
        EllipseAngle = nativeInstance.EllipseAngle_Get();
        EllipseAxis = nativeInstance.EllipseAxis_Get().ToPointD();
        EllipseEccentricity = nativeInstance.EllipseEccentricity_Get();
        EllipseIntensity = nativeInstance.EllipseIntensity_Get();
        _huInvariants = GetHuInvariants(nativeInstance);
    }

    /// <summary>
    /// Gets the centroid.
    /// </summary>
    public PointD Centroid { get; }

    /// <summary>
    /// Gets the channel of this moment.
    /// </summary>
    public PixelChannel Channel { get; }

    /// <summary>
    /// Gets the ellipse axis.
    /// </summary>
    public PointD EllipseAxis { get; }

    /// <summary>
    /// Gets the ellipse angle.
    /// </summary>
    public double EllipseAngle { get; }

    /// <summary>
    /// Gets the ellipse eccentricity.
    /// </summary>
    public double EllipseEccentricity { get; }

    /// <summary>
    /// Gets the ellipse intensity.
    /// </summary>
    public double EllipseIntensity { get; }

    /// <summary>
    /// Returns the Hu invariants.
    /// </summary>
    /// <param name="index">The index to use.</param>
    /// <returns>The Hu invariants.</returns>
    public double HuInvariants(int index)
    {
        Throw.IfOutOfRange(nameof(index), index, 8);

        return _huInvariants[index];
    }

    internal static ChannelMoments? Create(PixelChannel channel, IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        return new ChannelMoments(channel, instance);
    }

    private static double[] GetHuInvariants(NativeChannelMoments nativeInstance)
    {
        var huInvariants = new double[8];

        for (var i = 0U; i < 8U; i++)
            huInvariants[i] = nativeInstance.GetHuInvariants(i);

        return huInvariants;
    }
}
