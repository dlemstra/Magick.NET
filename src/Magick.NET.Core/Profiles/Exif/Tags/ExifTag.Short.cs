// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <content/>
public abstract partial class ExifTag
{
    /// <summary>
    /// Gets the CellLength exif tag.
    /// </summary>
    public static ExifTag<ushort> CellLength { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.CellLength);

    /// <summary>
    /// Gets the CellWidth exif tag.
    /// </summary>
    public static ExifTag<ushort> CellWidth { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.CellWidth);

    /// <summary>
    /// Gets the CleanFaxData exif tag.
    /// </summary>
    public static ExifTag<ushort> CleanFaxData { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.CleanFaxData);

    /// <summary>
    /// Gets the ColorSpace exif tag.
    /// </summary>
    public static ExifTag<ushort> ColorSpace { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.ColorSpace);

    /// <summary>
    /// Gets the Compression exif tag.
    /// </summary>
    public static ExifTag<ushort> Compression { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.Compression);

    /// <summary>
    /// Gets the Contrast exif tag.
    /// </summary>
    public static ExifTag<ushort> Contrast { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.Contrast);

    /// <summary>
    /// Gets the CustomRendered exif tag.
    /// </summary>
    public static ExifTag<ushort> CustomRendered { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.CustomRendered);

    /// <summary>
    /// Gets the DotRange exif tag.
    /// </summary>
    public static ExifTag<ushort> DotRange { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.DotRange);

    /// <summary>
    /// Gets the ExposureMode exif tag.
    /// </summary>
    public static ExifTag<ushort> ExposureMode { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.ExposureMode);

    /// <summary>
    /// Gets the ExposureProgram exif tag.
    /// </summary>
    public static ExifTag<ushort> ExposureProgram { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.ExposureProgram);

    /// <summary>
    /// Gets the FillOrder exif tag.
    /// </summary>
    public static ExifTag<ushort> FillOrder { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.FillOrder);

    /// <summary>
    /// Gets the Flash exif tag.
    /// </summary>
    public static ExifTag<ushort> Flash { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.Flash);

    /// <summary>
    /// Gets the FocalLengthIn35mmFilm exif tag.
    /// </summary>
    public static ExifTag<ushort> FocalLengthIn35mmFilm { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.FocalLengthIn35mmFilm);

    /// <summary>
    /// Gets the FocalPlaneResolutionUnit exif tag.
    /// </summary>
    public static ExifTag<ushort> FocalPlaneResolutionUnit { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.FocalPlaneResolutionUnit);

    /// <summary>
    /// Gets the FocalPlaneResolutionUnit2 exif tag.
    /// </summary>
    public static ExifTag<ushort> FocalPlaneResolutionUnit2 { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.FocalPlaneResolutionUnit2);

    /// <summary>
    /// Gets the GainControl exif tag.
    /// </summary>
    public static ExifTag<ushort> GainControl { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.GainControl);

    /// <summary>
    /// Gets the GPSDifferential exif tag.
    /// </summary>
    public static ExifTag<ushort> GPSDifferential { get; } = new ExifTag<ushort>(ExifIfds.Gps, ExifTagValue.GPSDifferential);

    /// <summary>
    /// Gets the GrayResponseUnit exif tag.
    /// </summary>
    public static ExifTag<ushort> GrayResponseUnit { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.GrayResponseUnit);

    /// <summary>
    /// Gets the Indexed exif tag.
    /// </summary>
    public static ExifTag<ushort> Indexed { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.Indexed);

    /// <summary>
    /// Gets the Interlace exif tag.
    /// </summary>
    public static ExifTag<ushort> Interlace { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.Interlace);

    /// <summary>
    /// Gets the InkSet exif tag.
    /// </summary>
    public static ExifTag<ushort> InkSet { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.InkSet);

    /// <summary>
    /// Gets the JPEGProc exif tag.
    /// </summary>
    public static ExifTag<ushort> JPEGProc { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.JPEGProc);

    /// <summary>
    /// Gets the JPEGRestartInterval exif tag.
    /// </summary>
    public static ExifTag<ushort> JPEGRestartInterval { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.JPEGRestartInterval);

    /// <summary>
    /// Gets the LightSource exif tag.
    /// </summary>
    public static ExifTag<ushort> LightSource { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.LightSource);

    /// <summary>
    /// Gets the MeteringMode exif tag.
    /// </summary>
    public static ExifTag<ushort> MeteringMode { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.MeteringMode);

    /// <summary>
    /// Gets the NumberOfInks exif tag.
    /// </summary>
    public static ExifTag<ushort> NumberOfInks { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.NumberOfInks);

    /// <summary>
    /// Gets the OldSubfileType exif tag.
    /// </summary>
    public static ExifTag<ushort> OldSubfileType { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.OldSubfileType);

    /// <summary>
    /// Gets the OPIProxy exif tag.
    /// </summary>
    public static ExifTag<ushort> OPIProxy { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.OPIProxy);

    /// <summary>
    /// Gets the Orientation exif tag.
    /// </summary>
    public static ExifTag<ushort> Orientation { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.Orientation);

    /// <summary>
    /// Gets the PlanarConfiguration exif tag.
    /// </summary>
    public static ExifTag<ushort> PlanarConfiguration { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.PlanarConfiguration);

    /// <summary>
    /// Gets the PhotometricInterpretation exif tag.
    /// </summary>
    public static ExifTag<ushort> PhotometricInterpretation { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.PhotometricInterpretation);

    /// <summary>
    /// Gets the Rating exif tag.
    /// </summary>
    public static ExifTag<ushort> Rating { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.Rating);

    /// <summary>
    /// Gets the RatingPercent exif tag.
    /// </summary>
    public static ExifTag<ushort> RatingPercent { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.RatingPercent);

    /// <summary>
    /// Gets the ResolutionUnit exif tag.
    /// </summary>
    public static ExifTag<ushort> ResolutionUnit { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.ResolutionUnit);

    /// <summary>
    /// Gets the SamplesPerPixel exif tag.
    /// </summary>
    public static ExifTag<ushort> SamplesPerPixel { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.SamplesPerPixel);

    /// <summary>
    /// Gets the Saturation exif tag.
    /// </summary>
    public static ExifTag<ushort> Saturation { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.Saturation);

    /// <summary>
    /// Gets the SceneCaptureType exif tag.
    /// </summary>
    public static ExifTag<ushort> SceneCaptureType { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SceneCaptureType);

    /// <summary>
    /// Gets the SelfTimerMode exif tag.
    /// </summary>
    public static ExifTag<ushort> SelfTimerMode { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SelfTimerMode);

    /// <summary>
    /// Gets the SensitivityType exif tag.
    /// </summary>
    public static ExifTag<ushort> SensitivityType { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SensitivityType);

    /// <summary>
    /// Gets the SensingMethod exif tag.
    /// </summary>
    public static ExifTag<ushort> SensingMethod { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SensingMethod);

    /// <summary>
    /// Gets the SensingMethod2 exif tag.
    /// </summary>
    public static ExifTag<ushort> SensingMethod2 { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SensingMethod2);

    /// <summary>
    /// Gets the Sharpness exif tag.
    /// </summary>
    public static ExifTag<ushort> Sharpness { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.Sharpness);

    /// <summary>
    /// Gets the SubjectDistanceRange exif tag.
    /// </summary>
    public static ExifTag<ushort> SubjectDistanceRange { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.SubjectDistanceRange);

    /// <summary>
    /// Gets the Thresholding exif tag.
    /// </summary>
    public static ExifTag<ushort> Thresholding { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.Thresholding);

    /// <summary>
    /// Gets the WhiteBalance exif tag.
    /// </summary>
    public static ExifTag<ushort> WhiteBalance { get; } = new ExifTag<ushort>(ExifIfds.Exif, ExifTagValue.WhiteBalance);

    /// <summary>
    /// Gets the YCbCrPositioning exif tag.
    /// </summary>
    public static ExifTag<ushort> YCbCrPositioning { get; } = new ExifTag<ushort>(ExifIfds.Ifd0, ExifTagValue.YCbCrPositioning);
}
