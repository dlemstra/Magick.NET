// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the ClipPath exif tag.
    /// </summary>
    public static ExifTag<byte[]> ClipPath => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.ClipPath);

    /// <summary>
    /// Gets the CFAPattern2 exif tag.
    /// </summary>
    public static ExifTag<byte[]> CFAPattern2 => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.CFAPattern2);

    /// <summary>
    /// Gets the GPSVersionID exif tag.
    /// </summary>
    public static ExifTag<byte[]> GPSVersionID => new ExifTag<byte[]>(ExifIfds.Gps, ExifTagValue.GPSVersionID);

    /// <summary>
    /// Gets the TIFFEPStandardID exif tag.
    /// </summary>
    public static ExifTag<byte[]> TIFFEPStandardID => new ExifTag<byte[]>(ExifIfds.Exif, ExifTagValue.TIFFEPStandardID);

    /// <summary>
    /// Gets the VersionYear exif tag.
    /// </summary>
    public static ExifTag<byte[]> VersionYear => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.VersionYear);

    /// <summary>
    /// Gets the XMP exif tag.
    /// </summary>
    public static ExifTag<byte[]> XMP => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XMP);

    /// <summary>
    /// Gets the XPAuthor exif tag.
    /// </summary>
    public static ExifTag<byte[]> XPAuthor => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XPAuthor);

    /// <summary>
    /// Gets the XPComment exif tag.
    /// </summary>
    public static ExifTag<byte[]> XPComment => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XPComment);

    /// <summary>
    /// Gets the XPKeywords exif tag.
    /// </summary>
    public static ExifTag<byte[]> XPKeywords => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XPKeywords);

    /// <summary>
    /// Gets the XPSubject exif tag.
    /// </summary>
    public static ExifTag<byte[]> XPSubject => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XPSubject);

    /// <summary>
    /// Gets the XPTitle exif tag.
    /// </summary>
    public static ExifTag<byte[]> XPTitle => new ExifTag<byte[]>(ExifIfds.Ifd0, ExifTagValue.XPTitle);
}
