// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class TypeMetric
{
    internal static TypeMetric CreateInstance(IntPtr instance)
    {
        var nativeInstance = new NativeTypeMetric(instance);
        return new TypeMetric(nativeInstance);
    }

    [NativeInterop(StaticDispose = true)]
    private partial class NativeTypeMetric : NativeInstance
    {
        public partial double Ascent_Get();

        public partial double Descent_Get();

        public partial double MaxHorizontalAdvance_Get();

        public partial double TextHeight_Get();

        public partial double TextWidth_Get();

        public partial double UnderlinePosition_Get();

        public partial double UnderlineThickness_Get();
    }
}
