// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;

namespace ImageMagick;

/// <summary>
/// Contains the perceptual hash of one or more image channels.
/// </summary>
public sealed partial class PerceptualHash : IPerceptualHash
{
    private readonly ChannelPerceptualHash _red;
    private readonly ChannelPerceptualHash _green;
    private readonly ChannelPerceptualHash _blue;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerceptualHash"/> class.
    /// </summary>
    /// <param name="hash">The hash.</param>
    public PerceptualHash(string hash)
        : this(hash, DefaultColorSpaces)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerceptualHash"/> class.
    /// </summary>
    /// <param name="hash">The hash.</param>
    /// <param name="colorSpaces">The colorspaces that were used to create this hash.</param>
    public PerceptualHash(string hash, params ColorSpace[] colorSpaces)
    {
        Throw.IfNullOrEmpty(nameof(hash), hash);
        ValidateColorSpaces(colorSpaces);

        var length = 35 * colorSpaces.Length;
        Throw.IfFalse(hash.Length == 3 * length, nameof(hash), "Invalid hash size.");

        _red = new ChannelPerceptualHash(PixelChannel.Red, colorSpaces, hash.Substring(0, length));
        _green = new ChannelPerceptualHash(PixelChannel.Green, colorSpaces, hash.Substring(length, length));
        _blue = new ChannelPerceptualHash(PixelChannel.Blue, colorSpaces, hash.Substring(length + length, length));
    }

    private PerceptualHash(ChannelPerceptualHash red, ChannelPerceptualHash green, ChannelPerceptualHash blue)
    {
        _red = red;
        _green = green;
        _blue = blue;
    }

    internal static ColorSpace[] DefaultColorSpaces { get; } = [ColorSpace.XyY, ColorSpace.HSB];

    /// <summary>
    /// Returns the perceptual hash for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the hash for.</param>
    /// <returns>The perceptual hash for the specified channel.</returns>
    public IChannelPerceptualHash? GetChannel(PixelChannel channel)
        => channel switch
        {
            PixelChannel.Red => _red,
            PixelChannel.Green => _green,
            PixelChannel.Blue => _blue,
            _ => null,
        };

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="PerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    public double SumSquaredDistance(IPerceptualHash other)
    {
        Throw.IfNull(other);

        var red = other.GetChannel(PixelChannel.Red);
        var green = other.GetChannel(PixelChannel.Green);
        var blue = other.GetChannel(PixelChannel.Blue);

        if (red is null || green is null || blue is null)
            throw new NotSupportedException("The other perceptual hash should contain a red, green and blue channel.");

        return
          _red.SumSquaredDistance(red) +
          _green.SumSquaredDistance(green) +
          _blue.SumSquaredDistance(blue);
    }

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A <see cref="string"/>.</returns>
    public override string ToString()
        => _red.ToString() +
           _green.ToString() +
           _blue.ToString();

    internal static PerceptualHash? Create(IMagickImage image, ColorSpace[] colorSpaces, IntPtr list)
    {
        if (list == IntPtr.Zero)
            return null;

        var red = CreateChannel(image, colorSpaces, list, PixelChannel.Red);
        var green = CreateChannel(image, colorSpaces, list, PixelChannel.Green);
        var blue = CreateChannel(image, colorSpaces, list, PixelChannel.Blue);
        return new PerceptualHash(red, green, blue);
    }

    internal static void DisposeList(IntPtr list)
    {
        if (list != IntPtr.Zero)
            NativePerceptualHash.DisposeList(list);
    }

    internal static void ValidateColorSpaces(ColorSpace[] colorSpaces)
    {
        Throw.IfNull(colorSpaces);
        Throw.IfOutOfRange(1, 6, colorSpaces.Length, nameof(colorSpaces), "Invalid number of colorspaces, the minimum is 1 and the maximum is 6.");
        Throw.IfFalse(colorSpaces.Distinct().Count() == colorSpaces.Length, nameof(colorSpaces), "Specifying the same colorspace more than once is not allowed.");
    }

    private static ChannelPerceptualHash CreateChannel(IMagickImage image, ColorSpace[] colorSpaces, IntPtr list, PixelChannel channel)
    {
        var nativeInstance = NativePerceptualHash.GetInstance(image, list, channel);
        return new ChannelPerceptualHash(channel, colorSpaces, nativeInstance);
    }
}
