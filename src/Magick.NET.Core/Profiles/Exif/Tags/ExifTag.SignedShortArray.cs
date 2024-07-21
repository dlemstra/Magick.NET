// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the TimeZoneOffset exif tag.
    /// </summary>
    public static ExifTag<short[]> TimeZoneOffset { get; } = new ExifTag<short[]>(ExifTagValue.TimeZoneOffset);
}
