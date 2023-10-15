// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal sealed partial class MagickRectangle
{
    public MagickRectangle(int x, int y, int width, int height)
    {
        Throw.IfNegative(nameof(width), width);
        Throw.IfNegative(nameof(height), height);

        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    private MagickRectangle(NativeMagickRectangle instance)
    {
        X = instance.X;
        Y = instance.Y;
        Width = instance.Width;
        Height = instance.Height;
    }

    public int Height { get; set; }

    public int Width { get; set; }

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
            width = image.Width * new Percentage(geometry.Width);
            height = image.Height * new Percentage(geometry.Height);
        }

        return new MagickRectangle(geometry.X, geometry.Y, width, height);
    }

    internal static INativeInstance CreateInstance()
        => new NativeMagickRectangle();

    internal static MagickRectangle CreateInstance(INativeInstance nativeInstance)
    {
        if (nativeInstance is not NativeMagickRectangle instance)
            throw new InvalidOperationException();

        return new MagickRectangle(instance);
    }

    private NativeMagickRectangle CreateNativeInstance()
        => new NativeMagickRectangle
        {
            X = X,
            Y = Y,
            Width = Width,
            Height = Height,
        };
}
