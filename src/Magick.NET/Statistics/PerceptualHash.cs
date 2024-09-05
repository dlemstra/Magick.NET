// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageMagick;

/// <summary>
/// Contains the perceptual hash of one or more image channels.
/// </summary>
public sealed partial class PerceptualHash : IPerceptualHash
{
    private readonly Dictionary<PixelChannel, ChannelPerceptualHash> _channels = new();

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
        Throw.IfFalse(nameof(hash), hash.Length == 3 * length, "Invalid hash size.");

        _channels[PixelChannel.Red] = new ChannelPerceptualHash(PixelChannel.Red, colorSpaces, hash.Substring(0, length));
        _channels[PixelChannel.Green] = new ChannelPerceptualHash(PixelChannel.Green, colorSpaces, hash.Substring(length, length));
        _channels[PixelChannel.Blue] = new ChannelPerceptualHash(PixelChannel.Blue, colorSpaces, hash.Substring(length + length, length));
    }

    internal PerceptualHash(IMagickImage image, ColorSpace[] colorSpaces, IntPtr list)
    {
        if (list == IntPtr.Zero)
            return;

        AddChannel(image, colorSpaces, list, PixelChannel.Red);
        AddChannel(image, colorSpaces, list, PixelChannel.Green);
        AddChannel(image, colorSpaces, list, PixelChannel.Blue);
    }

    internal static ColorSpace[] DefaultColorSpaces { get; } = [ColorSpace.XyY, ColorSpace.HSB];

    internal bool Isvalid
        => _channels.ContainsKey(PixelChannel.Red) &&
           _channels.ContainsKey(PixelChannel.Green) &&
           _channels.ContainsKey(PixelChannel.Blue);

    /// <summary>
    /// Returns the perceptual hash for the specified channel.
    /// </summary>
    /// <param name="channel">The channel to get the has for.</param>
    /// <returns>The perceptual hash for the specified channel.</returns>
    public IChannelPerceptualHash? GetChannel(PixelChannel channel)
    {
        _channels.TryGetValue(channel, out var perceptualHash);
        return perceptualHash;
    }

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="PerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    public double SumSquaredDistance(IPerceptualHash other)
    {
        Throw.IfNull(nameof(other), other);

        var red = other.GetChannel(PixelChannel.Red);
        var green = other.GetChannel(PixelChannel.Green);
        var blue = other.GetChannel(PixelChannel.Blue);

        if (red is null || green is null || blue is null)
            throw new NotSupportedException("The other perceptual hash should contain a red, green and blue channel.");

        return
          _channels[PixelChannel.Red].SumSquaredDistance(red) +
          _channels[PixelChannel.Green].SumSquaredDistance(green) +
          _channels[PixelChannel.Blue].SumSquaredDistance(blue);
    }

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A <see cref="string"/>.</returns>
    public override string ToString()
        => _channels[PixelChannel.Red].ToString() +
           _channels[PixelChannel.Green].ToString() +
           _channels[PixelChannel.Blue].ToString();

    internal static void ValidateColorSpaces(ColorSpace[] colorSpaces)
    {
        Throw.IfNull(nameof(colorSpaces), colorSpaces);
        Throw.IfOutOfRange(nameof(colorSpaces), 1, 6, colorSpaces.Length, "Invalid number of colorspaces, the minimum is 1 and the maximum is 6.");
        Throw.IfFalse(nameof(colorSpaces), colorSpaces.Distinct().Count() == colorSpaces.Length, "Specifying the same colorspace more than once is not allowed.");
    }

    internal static void DisposeList(IntPtr list)
    {
        if (list != IntPtr.Zero)
            NativePerceptualHash.DisposeList(list);
    }

    private static ChannelPerceptualHash? CreateChannelPerceptualHash(IMagickImage image, ColorSpace[] colorSpaces, IntPtr list, PixelChannel channel)
    {
        var instance = NativePerceptualHash.GetInstance(image, list, channel);
        return new ChannelPerceptualHash(channel, colorSpaces, instance);
    }

    private void AddChannel(IMagickImage image, ColorSpace[] colorSpaces, IntPtr list, PixelChannel channel)
    {
        var instance = CreateChannelPerceptualHash(image, colorSpaces, list, channel);
        if (instance is not null)
            _channels.Add(instance.Channel, instance);
    }
}
