// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that can be used to access an Iptc profile.
/// </summary>
public sealed class IptcProfile : ImageProfile, IIptcProfile
{
    private Collection<IIptcValue>? _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="IptcProfile"/> class.
    /// </summary>
    public IptcProfile()
      : base("iptc")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IptcProfile"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the iptc profile from.</param>
    public IptcProfile(byte[] data)
      : base("iptc", data)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IptcProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the iptc profile file, or the relative
    /// iptc profile file name.</param>
    public IptcProfile(string fileName)
      : base("iptc", fileName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IptcProfile"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the iptc profile from.</param>
    public IptcProfile(Stream stream)
      : base("iptc", stream)
    {
    }

    /// <summary>
    /// Gets the values of this iptc profile.
    /// </summary>
    public IReadOnlyList<IIptcValue> Values
    {
        get
        {
            Initialize();

            return _values;
        }
    }

    /// <summary>
    /// Returns the first occurrence of a iptc value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the iptc value.</param>
    /// <returns>The value with the specified tag.</returns>
    public IIptcValue? GetValue(IptcTag tag)
    {
        foreach (var iptcValue in Values)
        {
            if (iptcValue.Tag == tag)
                return iptcValue;
        }

        return null;
    }

    /// <summary>
    /// Returns all values with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the iptc value.</param>
    /// <returns>The values found with the specified tag.</returns>
    public IEnumerable<IIptcValue> GetAllValues(IptcTag tag)
    {
        foreach (var iptcValue in Values)
        {
            if (iptcValue.Tag == tag)
            {
                yield return iptcValue;
            }
        }
    }

    /// <summary>
    /// Removes all values with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the iptc value to remove.</param>
    /// <returns>True when the value was found and removed.</returns>
    public bool RemoveValue(IptcTag tag)
    {
        Initialize();

        var removed = false;
        for (var i = _values.Count - 1; i >= 0; i--)
        {
            if (_values[i].Tag == tag)
            {
                _values.RemoveAt(i);
                removed = true;
            }
        }

        return removed;
    }

    /// <summary>
    /// Removes values with the specified tag and value.
    /// </summary>
    /// <param name="tag">The tag of the iptc value to remove.</param>
    /// <param name="value">The value of the iptc item to remove.</param>
    /// <returns>True when the value was found and removed.</returns>
    public bool RemoveValue(IptcTag tag, string value)
    {
        Initialize();

        var removed = false;
        for (var i = _values.Count - 1; i >= 0; i--)
        {
            if (_values[i].Tag == tag && _values[i].Value.Equals(value, StringComparison.Ordinal))
            {
                _values.RemoveAt(i);
                removed = true;
            }
        }

        return removed;
    }

    /// <summary>
    /// Sets the value of the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the iptc value.</param>
    /// <param name="value">The value.</param>
    public void SetValue(IptcTag tag, string value)
    {
        Initialize();

        if (!IptcTagHelper.IsRepeatable(tag))
        {
            foreach (var iptcValue in _values)
            {
                if (iptcValue.Tag == tag)
                {
                    iptcValue.Value = value;
                    return;
                }
            }
        }

        _values.Add(new IptcValue(tag, value));
    }

    /// <summary>
    /// Makes sure the datetime is formatted according to the iptc specification.
    /// <example>
    /// A date will be formatted as CCYYMMDD, e.g. "19890317" for 17 March 1989.
    /// A time value will be formatted as HHMMSS±HHMM, e.g. "090000+0200" for 9 o'clock Berlin time,
    /// two hours ahead of UTC.
    /// </example>
    /// </summary>
    /// <param name="tag">The tag of the iptc value.</param>
    /// <param name="dateTimeOffset">The datetime.</param>
    public void SetValue(IptcTag tag, DateTimeOffset dateTimeOffset)
    {
        var isDate = IptcTagHelper.IsDate(tag);

        if (!isDate && !IptcTagHelper.IsTime(tag))
        {
            throw new ArgumentException("iptc tag is not a time or date type", nameof(tag));
        }

        var formattedDate = isDate
            ? dateTimeOffset.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            : dateTimeOffset.ToString("HHmmsszzzz", CultureInfo.InvariantCulture).Replace(":", string.Empty);

        SetValue(tag, formattedDate);
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected override void UpdateData()
    {
        var length = 8;
        foreach (var value in Values)
        {
            length += value.Length + 5;
        }

        var data = new byte[length];
        var i = 0;

        // Iptc Envelope that sets the character set to UTF8.
        data[i++] = 0x1c;
        data[i++] = 0x01;
        data[i++] = 0x5a;
        data[i++] = 0x00;
        data[i++] = 0x03;
        data[i++] = 0x1b;
        data[i++] = 0x25;
        data[i++] = 0x47;

        foreach (var value in Values)
        {
            data[i++] = 0x1c;
            data[i++] = 0x02;
            data[i++] = (byte)value.Tag;
            data[i++] = (byte)(value.Length >> 8);
            data[i++] = (byte)value.Length;
            if (value.Length > 0)
            {
                Buffer.BlockCopy(value.ToByteArray(), 0, data, i, value.Length);
                i += value.Length;
            }
        }

        SetData(data);
    }

    [MemberNotNull(nameof(_values))]
    private void Initialize()
    {
        if (_values is not null)
            return;

        _values = new Collection<IIptcValue>();

        var data = GetDataProtected();
        if (data is null || data[0] != 0x1c)
            return;

        var i = 0;
        while (i + 4 < data.Length)
        {
            if (data[i++] != 0x1c)
                continue;

            var tag = IptcTag.Unknown;

            if (data[i++] == 0x02)
                tag = EnumHelper.Parse(data[i++], IptcTag.Unknown);
            else
                i++;

            var count = ByteConverter.ToShort(data, ref i);

            if (tag != IptcTag.Unknown)
            {
                var value = new byte[count];
                if ((count > 0) && (i + count <= data.Length))
                    Buffer.BlockCopy(data, i, value, 0, count);
                _values.Add(new IptcValue(tag, value));
            }

            i += count;
        }
    }
}
