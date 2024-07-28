// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that can be used to access an Exif profile.
/// </summary>
public sealed class ExifProfile : ImageProfile, IExifProfile
{
    private ExifData? _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    public ExifProfile()
      : base("exif")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the exif profile from.</param>
    public ExifProfile(byte[] data)
      : base("exif", data)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the exif profile file, or the relative
    /// exif profile file name.</param>
    public ExifProfile(string fileName)
      : base("exif", fileName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExifProfile"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the exif profile from.</param>
    public ExifProfile(Stream stream)
      : base("exif", stream)
    {
    }

    /// <summary>
    /// Gets or sets which parts will be written when the profile is added to an image.
    /// </summary>
    public ExifParts Parts { get; set; } = ExifParts.All;

    /// <summary>
    /// Gets the tags that where found but contained an invalid value.
    /// </summary>
    public IReadOnlyList<ExifTag> InvalidTags
    {
        get
        {
            InitializeValues();
            return _data.InvalidTags;
        }
    }

    /// <summary>
    /// Gets the length of the thumbnail data in the <see cref="byte"/> array of the profile.
    /// </summary>
    public uint ThumbnailLength
    {
        get
        {
            InitializeValues();
            return _data.ThumbnailLength;
        }
    }

    /// <summary>
    /// Gets the offset of the thumbnail data in the <see cref="byte"/> array of the profile.
    /// </summary>
    public uint ThumbnailOffset
    {
        get
        {
            InitializeValues();
            return _data.ThumbnailOffset;
        }
    }

    /// <summary>
    /// Gets the values of this exif profile.
    /// </summary>
    public IReadOnlyList<IExifValue> Values
    {
        get
        {
            InitializeValues();
            return _data.Values;
        }
    }

    /// <summary>
    /// Returns the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>The value with the specified tag.</returns>
    /// <typeparam name="TValueType">The data type of the tag.</typeparam>
    public IExifValue<TValueType>? GetValue<TValueType>(ExifTag<TValueType> tag)
    {
        foreach (var exifValue in Values)
        {
            if (exifValue.Tag == tag)
                return (IExifValue<TValueType>)exifValue;
        }

        return null;
    }

    /// <summary>
    /// Removes the thumbnail in the exif profile.
    /// </summary>
    public void RemoveThumbnail()
    {
        // The values need to be initialized to make sure the thumbnail is not written.
        InitializeValues();

        _data.ThumbnailLength = 0;
        _data.ThumbnailOffset = 0;
    }

    /// <summary>
    /// Removes the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>True when the value was fount and removed.</returns>
    public bool RemoveValue(ExifTag tag)
    {
        InitializeValues();

        for (var i = 0; i < _data.Values.Count; i++)
        {
            if (_data.Values[i].Tag == tag)
            {
                _data.Values.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Loads the data from the profile and rewrites the profile data. This can be used
    /// to fix an incorrect profile. It can also be used for products that require a
    /// specific exif structure.
    /// </summary>
    public void Rewrite()
    {
        InitializeValues();
        UpdateData();
    }

    /// <summary>
    /// Sets the value of the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="TValueType">The data type of the tag.</typeparam>
    public void SetValue<TValueType>(ExifTag<TValueType> tag, TValueType value)
    {
        Throw.IfNull(nameof(value), value);

        InitializeValues();
        foreach (var exifValue in _data.Values)
        {
            if (exifValue.Tag == tag)
            {
                exifValue.SetValue(value);
                return;
            }
        }

        var newExifValue = ExifValues.Create(tag);
        if (newExifValue is null)
            throw new NotSupportedException();

        newExifValue.SetValue(value);
        _data.Values.Add(newExifValue);
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected override void UpdateData()
    {
        if (_data is null)
        {
            return;
        }

        if (_data.Values.Count == 0)
        {
            SetData(null);
            return;
        }

        var writer = new ExifWriter(Parts);
        SetData(writer.Write(_data.Values));
    }

    [MemberNotNull(nameof(_data))]
    private void InitializeValues()
    {
        if (_data is not null)
            return;

        var data = GetData();
        if (data is null)
        {
            _data = new ExifData();
            return;
        }

        _data = ExifReader.Read(data);
    }
}
