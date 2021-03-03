// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal partial class PointInfo
    {
        private PointInfo(IntPtr instance)
        {
            var nativeInstance = new NativePointInfo(instance);
            X = nativeInstance.X;
            Y = nativeInstance.Y;
        }

        public double X { get; }

        public double Y { get; }

        public static PointInfo CreateInstance(IntPtr instance)
            => new PointInfo(instance);

        public PointD ToPointD()
            => new PointD(X, Y);
    }
}