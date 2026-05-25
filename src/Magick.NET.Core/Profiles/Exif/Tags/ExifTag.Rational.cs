// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the Acceleration exif tag.
    /// </summary>
    public static ExifTag<Rational> Acceleration { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.Acceleration);

    /// <summary>
    /// Gets the ApertureValue exif tag.
    /// </summary>
    public static ExifTag<Rational> ApertureValue { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.ApertureValue);

    /// <summary>
    /// Gets the BatteryLevel exif tag.
    /// </summary>
    public static ExifTag<Rational> BatteryLevel { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.BatteryLevel);

    /// <summary>
    /// Gets the CompressedBitsPerPixel exif tag.
    /// </summary>
    public static ExifTag<Rational> CompressedBitsPerPixel { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.CompressedBitsPerPixel);

    /// <summary>
    /// Gets the DigitalZoomRatio exif tag.
    /// </summary>
    public static ExifTag<Rational> DigitalZoomRatio { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.DigitalZoomRatio);

    /// <summary>
    /// Gets the ExposureIndex exif tag.
    /// </summary>
    public static ExifTag<Rational> ExposureIndex { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.ExposureIndex);

    /// <summary>
    /// Gets the ExposureIndex2 exif tag.
    /// </summary>
    public static ExifTag<Rational> ExposureIndex2 { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.ExposureIndex2);

    /// <summary>
    /// Gets the ExposureTime exif tag.
    /// </summary>
    public static ExifTag<Rational> ExposureTime { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.ExposureTime);

    /// <summary>
    /// Gets the FlashEnergy exif tag.
    /// </summary>
    public static ExifTag<Rational> FlashEnergy { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FlashEnergy);

    /// <summary>
    /// Gets the FlashEnergy2 exif tag.
    /// </summary>
    public static ExifTag<Rational> FlashEnergy2 { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FlashEnergy2);

    /// <summary>
    /// Gets the FocalLength exif tag.
    /// </summary>
    public static ExifTag<Rational> FocalLength { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FocalLength);

    /// <summary>
    /// Gets the FocalPlaneXResolution exif tag.
    /// </summary>
    public static ExifTag<Rational> FocalPlaneXResolution { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FocalPlaneXResolution);

    /// <summary>
    /// Gets the FocalPlaneXResolution2 exif tag.
    /// </summary>
    public static ExifTag<Rational> FocalPlaneXResolution2 { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FocalPlaneXResolution2);

    /// <summary>
    /// Gets the FocalPlaneYResolution exif tag.
    /// </summary>
    public static ExifTag<Rational> FocalPlaneYResolution { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FocalPlaneYResolution);

    /// <summary>
    /// Gets the FocalPlaneYResolution2 exif tag.
    /// </summary>
    public static ExifTag<Rational> FocalPlaneYResolution2 { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FocalPlaneYResolution2);

    /// <summary>
    /// Gets the FNumber exif tag.
    /// </summary>
    public static ExifTag<Rational> FNumber { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.FNumber);

    /// <summary>
    /// Gets the GPSAltitude exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSAltitude { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSAltitude);

    /// <summary>
    /// Gets the GPSDestBearing exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSDestBearing { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSDestBearing);

    /// <summary>
    /// Gets the GPSDestDistance exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSDestDistance { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSDestDistance);

    /// <summary>
    /// Gets the GPSImgDirection exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSImgDirection { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSImgDirection);

    /// <summary>
    /// Gets the GPSDOP exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSDOP { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSDOP);

    /// <summary>
    /// Gets the GPSSpeed exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSSpeed { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSSpeed);

    /// <summary>
    /// Gets the GPSTrack exif tag.
    /// </summary>
    public static ExifTag<Rational> GPSTrack { get; } = new ExifTag<Rational>(ExifIfds.Gps, ExifTagValue.GPSTrack);

    /// <summary>
    /// Gets the Humidity exif tag.
    /// </summary>
    public static ExifTag<Rational> Humidity { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.Humidity);

    /// <summary>
    /// Gets the MaxApertureValue exif tag.
    /// </summary>
    public static ExifTag<Rational> MaxApertureValue { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.MaxApertureValue);

    /// <summary>
    /// Gets the MDScalePixel exif tag.
    /// </summary>
    public static ExifTag<Rational> MDScalePixel { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.MDScalePixel);

    /// <summary>
    /// Gets the Pressure exif tag.
    /// </summary>
    public static ExifTag<Rational> Pressure { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.Pressure);

    /// <summary>
    /// Gets the SubjectDistance exif tag.
    /// </summary>
    public static ExifTag<Rational> SubjectDistance { get; } = new ExifTag<Rational>(ExifIfds.Exif, ExifTagValue.SubjectDistance);

    /// <summary>
    /// Gets the XPosition exif tag.
    /// </summary>
    public static ExifTag<Rational> XPosition { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.XPosition);

    /// <summary>
    /// Gets the XResolution exif tag.
    /// </summary>
    public static ExifTag<Rational> XResolution { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.XResolution);

    /// <summary>
    /// Gets the YPosition exif tag.
    /// </summary>
    public static ExifTag<Rational> YPosition { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.YPosition);

    /// <summary>
    /// Gets the YResolution exif tag.
    /// </summary>
    public static ExifTag<Rational> YResolution { get; } = new ExifTag<Rational>(ExifIfds.Ifd0, ExifTagValue.YResolution);
}
