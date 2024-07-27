// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal sealed partial class MagickRectangle
{
    public MagickRectangle(int x, int y, uint width, uint height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    private MagickRectangle(NativeMagickRectangle instance)
    {
        X = (int)instance.X_Get();
        Y = (int)instance.Y_Get();
        Width = (uint)instance.Width_Get();
        Height = (uint)instance.Height_Get();
    }

    public uint Height { get; set; }

    public uint Width { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public static MagickRectangle? FromPageSize(string pageSize)
        => NativeMagickRectangle.FromPageSize(pageSize);

    public static MagickRectangle FromGeometry(IMagickGeometry geometry, MagickImage image)
    {
        Throw.IfNull(nameof(geometry), geometry);

        var width = geometry.Width;
        var height = geometry.Height;

        if (geometry.IsPercentage)
        {
            width = (uint)(image.Width * new Percentage(geometry.Width));
            height = (uint)(image.Height * new Percentage(geometry.Height));
        }

        return new MagickRectangle(geometry.X, geometry.Y, width, height);
    }

    internal static INativeInstance CreateInstance()
        => NativeMagickRectangle.Create();

    internal static MagickRectangle CreateInstance(INativeInstance nativeInstance)
    {
        if (nativeInstance is not NativeMagickRectangle instance)
            throw new InvalidOperationException();

        return new MagickRectangle(instance);
    }
}
