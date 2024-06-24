// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// The normalized moments of one image channels.
/// </summary>
public sealed partial class ChannelMoments : IChannelMoments
{
    private double[] _huInvariants;

    private ChannelMoments(PixelChannel channel, NativeChannelMoments instance)
    {
        Channel = channel;

        Centroid = instance.Centroid_Get().ToPointD();
        EllipseAngle = instance.EllipseAngle_Get();
        EllipseAxis = instance.EllipseAxis_Get().ToPointD();
        EllipseEccentricity = instance.EllipseEccentricity_Get();
        EllipseIntensity = instance.EllipseIntensity_Get();
        _huInvariants = GetHuInvariants(instance);
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

    private static double[] GetHuInvariants(NativeChannelMoments nativeInstance)
    {
        var huInvariants = new double[8];

        for (var i = 0U; i < 8U; i++)
            huInvariants[i] = nativeInstance.GetHuInvariants(i);

        return huInvariants;
    }
}
