// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class ChannelMoments
{
    [NativeInterop]
    private partial class NativeChannelMoments : ConstNativeInstance
    {
        public NativeChannelMoments(IntPtr instance)
            => Instance = instance;

        public partial PointInfo Centroid_Get();

        public partial double EllipseAngle_Get();

        public partial PointInfo EllipseAxis_Get();

        public partial double EllipseEccentricity_Get();

        public partial double EllipseIntensity_Get();

        public partial double GetHuInvariants(uint index);
    }
}
