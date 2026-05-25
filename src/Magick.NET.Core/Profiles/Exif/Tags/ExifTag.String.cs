// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the Artist exif tag.
    /// </summary>
    public static ExifTag<string> Artist { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.Artist);

    /// <summary>
    /// Gets the Copyright exif tag.
    /// </summary>
    public static ExifTag<string> Copyright { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.Copyright);

    /// <summary>
    /// Gets the DateTime exif tag.
    /// </summary>
    public static ExifTag<string> DateTime { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.DateTime);

    /// <summary>
    /// Gets the DateTimeDigitized exif tag.
    /// </summary>
    public static ExifTag<string> DateTimeDigitized { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.DateTimeDigitized);

    /// <summary>
    /// Gets the DateTimeOriginal exif tag.
    /// </summary>
    public static ExifTag<string> DateTimeOriginal { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.DateTimeOriginal);

    /// <summary>
    /// Gets the DocumentName exif tag.
    /// </summary>
    public static ExifTag<string> DocumentName { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.DocumentName);

    /// <summary>
    /// Gets the FaxSubaddress exif tag.
    /// </summary>
    public static ExifTag<string> FaxSubaddress { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.FaxSubaddress);

    /// <summary>
    /// Gets the GPSDateStamp exif tag.
    /// </summary>
    public static ExifTag<string> GPSDateStamp { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSDateStamp);

    /// <summary>
    /// Gets the GPSDestBearingRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSDestBearingRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSDestBearingRef);

    /// <summary>
    /// Gets the GPSDestDistanceRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSDestDistanceRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSDestDistanceRef);

    /// <summary>
    /// Gets the GPSDestLatitudeRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSDestLatitudeRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSDestLatitudeRef);

    /// <summary>
    /// Gets the GPSDestLongitudeRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSDestLongitudeRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSDestLongitudeRef);

    /// <summary>
    /// Gets the GPSImgDirectionRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSImgDirectionRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSImgDirectionRef);

    /// <summary>
    /// Gets the GPSLatitudeRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSLatitudeRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSLatitudeRef);

    /// <summary>
    /// Gets the GPSLongitudeRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSLongitudeRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSLongitudeRef);

    /// <summary>
    /// Gets the GPSMapDatum exif tag.
    /// </summary>
    public static ExifTag<string> GPSMapDatum { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSMapDatum);

    /// <summary>
    /// Gets the GPSMeasureMode exif tag.
    /// </summary>
    public static ExifTag<string> GPSMeasureMode { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSMeasureMode);

    /// <summary>
    /// Gets the GDALMetadata exif tag.
    /// </summary>
    public static ExifTag<string> GDALMetadata { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.GDALMetadata);

    /// <summary>
    /// Gets the GDALNoData exif tag.
    /// </summary>
    public static ExifTag<string> GDALNoData { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.GDALNoData);

    /// <summary>
    /// Gets the GPSSatellites exif tag.
    /// </summary>
    public static ExifTag<string> GPSSatellites { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSSatellites);

    /// <summary>
    /// Gets the GPSSpeedRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSSpeedRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSSpeedRef);

    /// <summary>
    /// Gets the GPSStatus exif tag.
    /// </summary>
    public static ExifTag<string> GPSStatus { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSStatus);

    /// <summary>
    /// Gets the GPSTrackRef exif tag.
    /// </summary>
    public static ExifTag<string> GPSTrackRef { get; } = new ExifTag<string>(ExifIfds.Gps, ExifTagValue.GPSTrackRef);

    /// <summary>
    /// Gets the HostComputer exif tag.
    /// </summary>
    public static ExifTag<string> HostComputer { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.HostComputer);

    /// <summary>
    /// Gets the ImageDescription exif tag.
    /// </summary>
    public static ExifTag<string> ImageDescription { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.ImageDescription);

    /// <summary>
    /// Gets the ImageID exif tag.
    /// </summary>
    public static ExifTag<string> ImageID { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.ImageID);

    /// <summary>
    /// Gets the ImageHistory exif tag.
    /// </summary>
    public static ExifTag<string> ImageHistory { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.ImageHistory);

    /// <summary>
    /// Gets the ImageUniqueID exif tag.
    /// </summary>
    public static ExifTag<string> ImageUniqueID { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.ImageUniqueID);

    /// <summary>
    /// Gets the InkNames exif tag.
    /// </summary>
    public static ExifTag<string> InkNames { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.InkNames);

    /// <summary>
    /// Gets the LensMake exif tag.
    /// </summary>
    public static ExifTag<string> LensMake { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.LensMake);

    /// <summary>
    /// Gets the LensModel exif tag.
    /// </summary>
    public static ExifTag<string> LensModel { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.LensModel);

    /// <summary>
    /// Gets the LensSerialNumber exif tag.
    /// </summary>
    public static ExifTag<string> LensSerialNumber { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.LensSerialNumber);

    /// <summary>
    /// Gets the Make exif tag.
    /// </summary>
    public static ExifTag<string> Make { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.Make);

    /// <summary>
    /// Gets the MDFileUnits exif tag.
    /// </summary>
    public static ExifTag<string> MDFileUnits => new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.MDFileUnits);

    /// <summary>
    /// Gets the MDLabName exif tag.
    /// </summary>
    public static ExifTag<string> MDLabName { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.MDLabName);

    /// <summary>
    /// Gets the MDPrepDate exif tag.
    /// </summary>
    public static ExifTag<string> MDPrepDate { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.MDPrepDate);

    /// <summary>
    /// Gets the MDPrepTime exif tag.
    /// </summary>
    public static ExifTag<string> MDPrepTime { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.MDPrepTime);

    /// <summary>
    /// Gets the MDSampleInfo exif tag.
    /// </summary>
    public static ExifTag<string> MDSampleInfo { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.MDSampleInfo);

    /// <summary>
    /// Gets the Model exif tag.
    /// </summary>
    public static ExifTag<string> Model { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.Model);

    /// <summary>
    /// Gets the OffsetTime exif tag.
    /// </summary>
    public static ExifTag<string> OffsetTime { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.OffsetTime);

    /// <summary>
    /// Gets the OffsetTimeDigitized exif tag.
    /// </summary>
    public static ExifTag<string> OffsetTimeDigitized { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.OffsetTimeDigitized);

    /// <summary>
    /// Gets the OffsetTimeOriginal exif tag.
    /// </summary>
    public static ExifTag<string> OffsetTimeOriginal { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.OffsetTimeOriginal);

    /// <summary>
    /// Gets the OwnerName exif tag.
    /// </summary>
    public static ExifTag<string> OwnerName { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.OwnerName);

    /// <summary>
    /// Gets the PageName exif tag.
    /// </summary>
    public static ExifTag<string> PageName { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.PageName);

    /// <summary>
    /// Gets the RelatedSoundFile exif tag.
    /// </summary>
    public static ExifTag<string> RelatedSoundFile { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.RelatedSoundFile);

    /// <summary>
    /// Gets the SecurityClassification exif tag.
    /// </summary>
    public static ExifTag<string> SecurityClassification { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SecurityClassification);

    /// <summary>
    /// Gets the SEMInfo exif tag.
    /// </summary>
    public static ExifTag<string> SEMInfo { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.SEMInfo);

    /// <summary>
    /// Gets the SpectralSensitivity exif tag.
    /// </summary>
    public static ExifTag<string> SpectralSensitivity { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SpectralSensitivity);

    /// <summary>
    /// Gets the Software exif tag.
    /// </summary>
    public static ExifTag<string> Software { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.Software);

    /// <summary>
    /// Gets the SubsecTime exif tag.
    /// </summary>
    public static ExifTag<string> SubsecTime { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SubsecTime);

    /// <summary>
    /// Gets the SubsecTimeDigitized exif tag.
    /// </summary>
    public static ExifTag<string> SubsecTimeDigitized { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SubsecTimeDigitized);

    /// <summary>
    /// Gets the SubsecTimeOriginal exif tag.
    /// </summary>
    public static ExifTag<string> SubsecTimeOriginal { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SubsecTimeOriginal);

    /// <summary>
    /// Gets the TargetPrinter exif tag.
    /// </summary>
    public static ExifTag<string> TargetPrinter { get; } = new ExifTag<string>(ExifIfds.Ifd0, ExifTagValue.TargetPrinter);

    /// <summary>
    /// Gets the SerialNumber exif tag.
    /// </summary>
    public static ExifTag<string> SerialNumber { get; } = new ExifTag<string>(ExifIfds.Exif, ExifTagValue.SerialNumber);
}
