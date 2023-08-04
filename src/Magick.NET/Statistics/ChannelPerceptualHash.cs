// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Contains the he perceptual hash of one image channel.
/// </summary>
public partial class ChannelPerceptualHash : IChannelPerceptualHash
{
    private readonly double[] _srgbHuPhash;
    private readonly double[] _hclpHuPhash;
    private string _hash;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChannelPerceptualHash"/> class.
    /// </summary>
    /// <param name="channel">The channel.</param>
    /// <param name="srgbHuPhash">SRGB hu perceptual hash.</param>
    /// <param name="hclpHuPhash">Hclp hu perceptual hash.</param>
    /// <param name="hash">A string representation of this hash.</param>
    public ChannelPerceptualHash(PixelChannel channel, double[] srgbHuPhash, double[] hclpHuPhash, string hash)
    {
        Channel = channel;
        _srgbHuPhash = srgbHuPhash;
        _hclpHuPhash = hclpHuPhash;
        _hash = hash;
    }

    internal ChannelPerceptualHash(PixelChannel channel)
    {
        Channel = channel;
        _hclpHuPhash = new double[7];
        _srgbHuPhash = new double[7];
        _hash = string.Empty;
    }

    internal ChannelPerceptualHash(PixelChannel channel, IntPtr instance)
      : this(channel)
    {
        var nativeInstance = new NativeChannelPerceptualHash(instance);
        SetSrgbHuPhash(nativeInstance);
        SetHclpHuPhash(nativeInstance);
        SetHash();
    }

    internal ChannelPerceptualHash(PixelChannel channel, string hash)
      : this(channel)
        => ParseHash(hash);

    /// <summary>
    /// Gets the channel.
    /// </summary>
    public PixelChannel Channel { get; }

    /// <summary>
    /// SRGB hu perceptual hash.
    /// </summary>
    /// <param name="index">The index to use.</param>
    /// <returns>The SRGB hu perceptual hash.</returns>
    public double SrgbHuPhash(int index)
    {
        Throw.IfOutOfRange(nameof(index), index, 7);

        return _srgbHuPhash[index];
    }

    /// <summary>
    /// Hclp hu perceptual hash.
    /// </summary>
    /// <param name="index">The index to use.</param>
    /// <returns>The Hclp hu perceptual hash.</returns>
    public double HclpHuPhash(int index)
    {
        Throw.IfOutOfRange(nameof(index), index, 7);

        return _hclpHuPhash[index];
    }

    /// <summary>
    /// Returns the sum squared difference between this hash and the other hash.
    /// </summary>
    /// <param name="other">The <see cref="ChannelPerceptualHash"/> to get the distance of.</param>
    /// <returns>The sum squared difference between this hash and the other hash.</returns>
    public double SumSquaredDistance(IChannelPerceptualHash other)
    {
        Throw.IfNull(nameof(other), other);

        var ssd = 0.0;

        for (var i = 0; i < 7; i++)
        {
            ssd += (_srgbHuPhash[i] - other.SrgbHuPhash(i)) * (_srgbHuPhash[i] - other.SrgbHuPhash(i));
            ssd += (_hclpHuPhash[i] - other.HclpHuPhash(i)) * (_hclpHuPhash[i] - other.HclpHuPhash(i));
        }

        return ssd;
    }

    /// <summary>
    /// Returns a string representation of this hash.
    /// </summary>
    /// <returns>A string representation of this hash.</returns>
    public override string ToString()
        => _hash;

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

    private void ParseHash(string hash)
    {
        _hash = hash;

        for (int i = 0, offset = 0; i < 14; i++, offset += 5)
        {
            if (!int.TryParse(hash.Substring(offset, 5), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var hex))
                throw new ArgumentException("Invalid hash specified", nameof(hash));

            var value = (ushort)hex / PowerOfTen(hex >> 17);
            if ((hex & (1 << 16)) != 0)
                value = -value;
            if (i < 7)
                _srgbHuPhash[i] = value;
            else
                _hclpHuPhash[i - 7] = value;
        }
    }

    private void SetHash()
    {
        _hash = string.Empty;
        for (var i = 0; i < 14; i++)
        {
            double value;
            if (i < 7)
                value = _srgbHuPhash[i];
            else
                value = _hclpHuPhash[i - 7];

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

    private void SetHclpHuPhash(NativeChannelPerceptualHash instance)
    {
        for (var i = 0; i < 7; i++)
            _hclpHuPhash[i] = instance.GetHclpHuPhash(i);
    }

    private void SetSrgbHuPhash(NativeChannelPerceptualHash instance)
    {
        for (var i = 0; i < 7; i++)
            _srgbHuPhash[i] = instance.GetSrgbHuPhash(i);
    }
}
