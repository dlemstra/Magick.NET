// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick;

/// <summary>
/// Specifies a kind of color space.
/// </summary>
public enum ColorSpace
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// CMY.
    /// </summary>
    CMY,

    /// <summary>
    /// CMYK.
    /// </summary>
    CMYK,

    /// <summary>
    /// Gray.
    /// </summary>
    Gray,

    /// <summary>
    /// HCL.
    /// </summary>
    HCL,

    /// <summary>
    /// HCLp.
    /// </summary>
    HCLp,

    /// <summary>
    /// HSB.
    /// </summary>
    HSB,

    /// <summary>
    /// HSI.
    /// </summary>
    HSI,

    /// <summary>
    /// HSL.
    /// </summary>
    HSL,

    /// <summary>
    /// HSV.
    /// </summary>
    HSV,

    /// <summary>
    /// HWB.
    /// </summary>
    HWB,

    /// <summary>
    /// Lab.
    /// </summary>
    Lab,

    /// <summary>
    /// LCH.
    /// </summary>
    LCH,

    /// <summary>
    /// LCHab.
    /// </summary>
    LCHab,

    /// <summary>
    /// LCHuv.
    /// </summary>
    LCHuv,

    /// <summary>
    /// Log.
    /// </summary>
    Log,

    /// <summary>
    /// LMS.
    /// </summary>
    LMS,

    /// <summary>
    /// Luv.
    /// </summary>
    Luv,

    /// <summary>
    /// OHTA.
    /// </summary>
    OHTA,

    /// <summary>
    /// Rec601YCbCr.
    /// </summary>
    Rec601YCbCr,

    /// <summary>
    /// Rec709YCbCr.
    /// </summary>
    Rec709YCbCr,

    /// <summary>
    /// RGB.
    /// </summary>
    RGB,

    /// <summary>
    /// scRGB.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element must begin with upper-case letter", Justification = "Special case that starts lowercase.")]
    scRGB,

    /// <summary>
    /// sRGB.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element must begin with upper-case letter", Justification = "Special case that starts lowercase.")]
    sRGB,

    /// <summary>
    /// Transparent.
    /// </summary>
    Transparent,

    /// <summary>
    /// XyY.
    /// </summary>
    XyY,

    /// <summary>
    /// XYZ.
    /// </summary>
    XYZ,

    /// <summary>
    /// YCbCr.
    /// </summary>
    YCbCr,

    /// <summary>
    /// YCC.
    /// </summary>
    YCC,

    /// <summary>
    /// YDbDr.
    /// </summary>
    YDbDr,

    /// <summary>
    /// YIQ.
    /// </summary>
    YIQ,

    /// <summary>
    /// YPbPr.
    /// </summary>
    YPbPr,

    /// <summary>
    /// YUV.
    /// </summary>
    YUV,

    /// <summary>
    /// LinearGray.
    /// </summary>
    LinearGray,

    /// <summary>
    /// Jzazbz.
    /// </summary>
    Jzazbz,

    /// <summary>
    /// DisplayP3.
    /// </summary>
    DisplayP3,

    /// <summary>
    /// Adobe98.
    /// </summary>
    Adobe98,

    /// <summary>
    /// ProPhoto.
    /// </summary>
    ProPhoto,

    /// <summary>
    /// Oklab.
    /// </summary>
    Oklab,

    /// <summary>
    /// Oklch.
    /// </summary>
    Oklch,
}
