// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick;

internal partial class ChannelStatistics
{
    [NativeInterop]
    private partial class NativeChannelStatistics : ConstNativeInstance
    {
        public partial nuint Depth_Get();

        public partial double Entropy_Get();

        public partial double Kurtosis_Get();

        public partial double Maximum_Get();

        public partial double Mean_Get();

        public partial double Minimum_Get();

        public partial double Skewness_Get();

        public partial double StandardDeviation_Get();
    }
}
