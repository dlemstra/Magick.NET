// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the CodingMethods exif tag.
    /// </summary>
    public static ExifTag<uint> CodingMethods { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.CodingMethods);

    /// <summary>
    /// Gets the FaxRecvParams exif tag.
    /// </summary>
    public static ExifTag<uint> FaxRecvParams { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.FaxRecvParams);

    /// <summary>
    /// Gets the FaxRecvTime exif tag.
    /// </summary>
    public static ExifTag<uint> FaxRecvTime { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.FaxRecvTime);

    /// <summary>
    /// Gets the GPSIFDOffset exif tag.
    /// </summary>
    public static ExifTag<uint> GPSIFDOffset { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.GPSIFDOffset);

    /// <summary>
    /// Gets the ImageNumber exif tag.
    /// </summary>
    public static ExifTag<uint> ImageNumber { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.ImageNumber);

    /// <summary>
    /// Gets the ISOSpeed exif tag.
    /// </summary>
    public static ExifTag<uint> ISOSpeed { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.ISOSpeed);

    /// <summary>
    /// Gets the ISOSpeedLatitudeyyy exif tag.
    /// </summary>
    public static ExifTag<uint> ISOSpeedLatitudeyyy { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.ISOSpeedLatitudeyyy);

    /// <summary>
    /// Gets the ISOSpeedLatitudezzz exif tag.
    /// </summary>
    public static ExifTag<uint> ISOSpeedLatitudezzz { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.ISOSpeedLatitudezzz);

    /// <summary>
    /// Gets the JPEGInterchangeFormat exif tag.
    /// </summary>
    public static ExifTag<uint> JPEGInterchangeFormat { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.JPEGInterchangeFormat);

    /// <summary>
    /// Gets the JPEGInterchangeFormatLength exif tag.
    /// </summary>
    public static ExifTag<uint> JPEGInterchangeFormatLength { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.JPEGInterchangeFormatLength);

    /// <summary>
    /// Gets the MDFileTag exif tag.
    /// </summary>
    public static ExifTag<uint> MDFileTag { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.MDFileTag);

    /// <summary>
    /// Gets the ProfileType exif tag.
    /// </summary>
    public static ExifTag<uint> ProfileType { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.ProfileType);

    /// <summary>
    /// Gets the RecommendedExposureIndex exif tag.
    /// </summary>
    public static ExifTag<uint> RecommendedExposureIndex { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.RecommendedExposureIndex);

    /// <summary>
    /// Gets the StandardOutputSensitivity exif tag.
    /// </summary>
    public static ExifTag<uint> StandardOutputSensitivity { get; } = new ExifTag<uint>(ExifIfds.Exif, ExifTagValue.StandardOutputSensitivity);

    /// <summary>
    /// Gets the SubfileType exif tag.
    /// </summary>
    public static ExifTag<uint> SubfileType { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.SubfileType);

    /// <summary>
    /// Gets the SubIFDOffset exif tag.
    /// </summary>
    public static ExifTag<uint> SubIFDOffset { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.SubIFDOffset);

    /// <summary>
    /// Gets the T4Options exif tag.
    /// </summary>
    public static ExifTag<uint> T4Options { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.T4Options);

    /// <summary>
    /// Gets the T6Options exif tag.
    /// </summary>
    public static ExifTag<uint> T6Options { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.T6Options);

    /// <summary>
    /// Gets the T82ptions exif tag.
    /// </summary>
    public static ExifTag<uint> T82ptions { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.T82ptions);

    /// <summary>
    /// Gets the XClipPathUnits exif tag.
    /// </summary>
    public static ExifTag<uint> XClipPathUnits { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.XClipPathUnits);

    /// <summary>
    /// Gets the YClipPathUnits exif tag.
    /// </summary>
    public static ExifTag<uint> YClipPathUnits { get; } = new ExifTag<uint>(ExifIfds.Ifd0, ExifTagValue.YClipPathUnits);
}
