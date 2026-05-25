// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the BadFaxLines exif tag.
    /// </summary>
    public static ExifTag<Number> BadFaxLines { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.BadFaxLines);

    /// <summary>
    /// Gets the ConsecutiveBadFaxLines exif tag.
    /// </summary>
    public static ExifTag<Number> ConsecutiveBadFaxLines { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.ConsecutiveBadFaxLines);

    /// <summary>
    /// Gets the ImageLength exif tag.
    /// </summary>
    public static ExifTag<Number> ImageLength { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.ImageLength);

    /// <summary>
    /// Gets the ImageWidth exif tag.
    /// </summary>
    public static ExifTag<Number> ImageWidth { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.ImageWidth);

    /// <summary>
    /// Gets the PixelXDimension exif tag.
    /// </summary>
    public static ExifTag<Number> PixelXDimension { get; } = new ExifTag<Number>(ExifIfds.Exif, ExifTagValue.PixelXDimension);

    /// <summary>
    /// Gets the PixelYDimension exif tag.
    /// </summary>
    public static ExifTag<Number> PixelYDimension { get; } = new ExifTag<Number>(ExifIfds.Exif, ExifTagValue.PixelYDimension);

    /// <summary>
    /// Gets the RowsPerStrip exif tag.
    /// </summary>
    public static ExifTag<Number> RowsPerStrip { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.RowsPerStrip);

    /// <summary>
    /// Gets the StripByteCounts exif tag.
    /// </summary>
    public static ExifTag<Number> StripByteCounts { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.StripByteCounts);

    /// <summary>
    /// Gets the TileLength exif tag.
    /// </summary>
    public static ExifTag<Number> TileLength { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.TileLength);

    /// <summary>
    /// Gets the TileWidth exif tag.
    /// </summary>
    public static ExifTag<Number> TileWidth { get; } = new ExifTag<Number>(ExifIfds.Ifd0, ExifTagValue.TileWidth);
}
