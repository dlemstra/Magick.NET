// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal sealed partial class ChannelStatistics : IChannelStatistics
{
    private ChannelStatistics(PixelChannel channel, IntPtr instance)
    {
        Channel = channel;

        var nativeInstance = new NativeChannelStatistics(instance);
        Depth = nativeInstance.Depth;
        Entropy = nativeInstance.Entropy;
        Kurtosis = nativeInstance.Kurtosis;
        Maximum = nativeInstance.Maximum;
        Mean = nativeInstance.Mean;
        Minimum = nativeInstance.Minimum;
        Skewness = nativeInstance.Skewness;
        StandardDeviation = nativeInstance.StandardDeviation;
    }

    public PixelChannel Channel { get; }

    public int Depth { get; }

    public double Entropy { get; }

    public double Kurtosis { get; }

    public double Maximum { get; }

    public double Mean { get; }

    public double Minimum { get; }

    public double Skewness { get; }

    public double StandardDeviation { get; }

    internal static ChannelStatistics? Create(PixelChannel channel, IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        return new ChannelStatistics(channel, instance);
    }
}
