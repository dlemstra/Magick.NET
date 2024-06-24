// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class ChannelMoments
{
    internal static ChannelMoments? Create(PixelChannel channel, IntPtr instance)
    {
        var nativeInstance = new NativeChannelMoments(instance);
        return new ChannelMoments(channel, nativeInstance);
    }

    [NativeInterop]
    private partial class NativeChannelMoments : ConstNativeInstance
    {
        public partial PointInfo Centroid_Get();

        public partial double EllipseAngle_Get();

        public partial PointInfo EllipseAxis_Get();

        public partial double EllipseEccentricity_Get();

        public partial double EllipseIntensity_Get();

        public partial double GetHuInvariants(nuint index);
    }
}
