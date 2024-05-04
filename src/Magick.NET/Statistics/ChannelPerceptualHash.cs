// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Contains the he perceptual hash of one image channel.
/// </summary>
public partial class ChannelPerceptualHash : IChannelPerceptualHash
{
    private readonly List<HuPhashList> _huPhashes = new();
    private string _hash = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChannelPerceptualHash"/> class.
    /// </summary>
    /// <param name="channel">The channel.</param>
    /// <param name="srgbHuPhash">SRGB hu perceptual hash.</param>
    /// <param name="hclpHuPhash">Hclp hu perceptual hash.</param>
    /// <param name="hash">A string representation of this hash.</param>
    [Obsolete("Will be removed in the next major release.")]
    public ChannelPerceptualHash(PixelChannel channel, double[] srgbHuPhash, double[] hclpHuPhash, string hash)
    {
        Channel = channel;
        _huPhashes.Add(new HuPhashList(ColorSpace.sRGB, srgbHuPhash));
        _huPhashes.Add(new HuPhashList(ColorSpace.HCLp, hclpHuPhash));
        _hash = hash;
    }

    internal ChannelPerceptualHash(PixelChannel channel, ColorSpace[] colorSpaces, IntPtr instance)
    {
        Channel = channel;
        var nativeInstance = new NativeChannelPerceptualHash(instance);
        for (var i = 0; i < colorSpaces.Length; i++)
        {
            AddHuPhash(nativeInstance, colorSpaces[i], i);
        }
    }

    internal ChannelPerceptualHash(PixelChannel channel, ColorSpace[] colorSpaces, string hash)
    {
        Channel = channel;
        ParseHash(colorSpaces, hash);
    }

    /// <summary>
    /// Gets the channel.
    /// </summary>
    public PixelChannel Channel { get; }

    /// <summary>
    /// SRGB hu perceptual hash.
    /// </summary>
    /// <param name="index">The index to use.</param>
    /// <returns>The SRGB hu perceptual hash.</returns>
    [Obsolete("Will be removed in the next major release, use HuPhash(ColorSpace.sRGB, index) instead.")]
    public double SrgbHuPhash(int index)
        => HuPhash(ColorSpace.sRGB, index);

    /// <summary>
    /// Hclp hu perceptual hash.
    /// </summary>
    /// <param name="index">The index to use.</param>
    /// <returns>The Hclp hu perceptual hash.</returns>
    [Obsolete("Will be removed in the next major releas, use HuPhash(ColorSpace.HCLp, index) instead.")]
    public double HclpHuPhash(int index)
        => HuPhash(ColorSpace.HCLp, index);

    /// <summary>
    /// Returns the hu perceptual hash for the specified colorspace.
    /// </summary>
    /// <param name="colorSpace">The colorspace to use.</param>
    /// <param name="index">The index to use.</param>
    /// <returns>The hu perceptual hash for the specified colorspace.</returns>
    public double HuPhash(ColorSpace colorSpace, int index)
    {
        Throw.IfOutOfRange(nameof(index), index, 7);

        var huPhashList = GetHuPhashListByColorSpace(colorSpace);
        if (huPhashList is null)
            throw new ArgumentException("Invalid colorspace specified.", nameof(colorSpace));

        return huPhashList[index];
    }

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="ChannelPerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    public double SumSquaredDistance(IChannelPerceptualHash other)
    {
        Throw.IfNull(nameof(other), other);

        var otherChannelPerceptualHash = other as ChannelPerceptualHash;
        var ssd = 0.0;

        foreach (var huPhashList in _huPhashes)
        {
            var otherHuPhashList = otherChannelPerceptualHash?.GetHuPhashListByColorSpace(huPhashList.ColorSpace);

            for (var i = 0; i < 7; i++)
            {
                var a = huPhashList[i];
                var b = otherHuPhashList is null ? 0 : otherHuPhashList[i];

                ssd += (a - b) * (a - b);
            }
        }

        return ssd;
    }

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A string representation of this hash.</returns>
    public override string ToString()
    {
        if (_hash == string.Empty)
            SetHash();

        return _hash;
    }

    private static double PowerOfTen(int power)
        => power switch
        {
            2 => 100.0,
            3 => 1000.0,
            4 => 10000.0,
            5 => 100000.0,
            6 => 1000000.0,
            _ => 10.0,
        };

    private HuPhashList? GetHuPhashListByColorSpace(ColorSpace colorSpace)
    {
        foreach (var huPhashList in _huPhashes)
        {
            if (huPhashList.ColorSpace == colorSpace)
                return huPhashList;
        }

        return null;
    }

    private void ParseHash(ColorSpace[] colorSpaces, string hash)
    {
        _hash = hash;

        var offset = 0;
        foreach (var colorSpace in colorSpaces)
        {
            var huPhashList = new HuPhashList(colorSpace);
            for (var i = 0; i < 7; i++, offset += 5)
            {
                if (!int.TryParse(hash.Substring(offset, 5), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var hex))
                    throw new ArgumentException("Invalid hash specified", nameof(hash));

                var value = (ushort)hex / PowerOfTen(hex >> 17);
                if ((hex & (1 << 16)) != 0)
                    value = -value;

                huPhashList[i] = value;
            }

            _huPhashes.Add(huPhashList);
        }
    }

    private void SetHash()
    {
        _hash = string.Empty;

        foreach (var huPhashList in _huPhashes)
        {
            for (var i = 0; i < 7; i++)
            {
                var value = huPhashList[i];

                var hex = 0;
                while (hex < 7 && Math.Abs(value * 10) < 65536)
                {
                    value *= 10;
                    hex++;
                }

                hex <<= 1;
                if (value < 0.0)
                    hex |= 1;
                hex = (hex << 16) + (int)(value < 0.0 ? -(value - 0.5) : value + 0.5);
                _hash += hex.ToString("x", CultureInfo.InvariantCulture);
            }
        }
    }

    private void AddHuPhash(NativeChannelPerceptualHash instance, ColorSpace colorSpace, int colorSpaceIndex)
    {
        var huPhashList = new HuPhashList(colorSpace);

        for (var i = 0; i < 7; i++)
            huPhashList[i] = instance.GetHuPhash(colorSpaceIndex, i);

        _huPhashes.Add(huPhashList);
    }

    private sealed class HuPhashList
    {
        private readonly double[] _values;

        public HuPhashList(ColorSpace colorSpace)
            : this(colorSpace, new double[] { 0, 0, 0, 0, 0, 0, 0 })
        {
        }

        public HuPhashList(ColorSpace colorSpace, double[] values)
        {
            ColorSpace = colorSpace;
            _values = values;
        }

        public ColorSpace ColorSpace { get; }

        public double this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }
    }
}
