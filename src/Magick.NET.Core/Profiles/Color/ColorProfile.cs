// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that contains an ICM/ICC color profile.
/// </summary>
public sealed class ColorProfile : ImageProfile, IColorProfile
{
    private ColorProfileData? _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="data">A byte array containing the profile.</param>
    public ColorProfile(byte[] data)
      : base("icc", data)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="stream">A stream containing the profile.</param>
    public ColorProfile(Stream stream)
      : base("icc", stream)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public ColorProfile(string fileName)
      : base("icc", fileName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the color profile (e.g. icc or icm).</param>
    /// <param name="data">A byte array containing the profile.</param>
    [Obsolete("This constructor will be removed in the next major release (only 'icc' will be used in the future).")]
    public ColorProfile(string name, byte[] data)
      : base(name, data)
    {
    }

    /// <summary>
    /// Gets the AdobeRGB1998 profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.AdobeRGB1998)} instead.")]
    public static ColorProfile AdobeRGB1998
        => ColorProfiles.AdobeRGB1998;

    /// <summary>
    /// Gets the AppleRGB profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.AppleRGB)} instead.")]
    public static ColorProfile AppleRGB
        => ColorProfiles.AppleRGB;

    /// <summary>
    /// Gets the CoatedFOGRA39 profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.CoatedFOGRA39)} instead.")]
    public static ColorProfile CoatedFOGRA39
        => ColorProfiles.CoatedFOGRA39;

    /// <summary>
    /// Gets the ColorMatchRGB profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.ColorMatchRGB)} instead.")]
    public static ColorProfile ColorMatchRGB
        => ColorProfiles.ColorMatchRGB;

    /// <summary>
    /// Gets the sRGB profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.SRGB)} instead.")]
    public static ColorProfile SRGB
        => ColorProfiles.SRGB;

    /// <summary>
    /// Gets the USWebCoatedSWOP profile.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release. Use {nameof(ColorProfiles)}.{nameof(ColorProfiles.USWebCoatedSWOP)} instead.")]
    public static ColorProfile USWebCoatedSWOP
        => ColorProfiles.USWebCoatedSWOP;

    /// <summary>
    /// Gets the color space of the profile.
    /// </summary>
    public ColorSpace ColorSpace
    {
        get
        {
            Initialize();
            return _data.ColorSpace;
        }
    }

    /// <summary>
    /// Gets the copyright of the profile.
    /// </summary>
    public string? Copyright
    {
        get
        {
            Initialize();
            return _data.Copyright;
        }
    }

    /// <summary>
    /// Gets the description of the profile.
    /// </summary>
    public string? Description
    {
        get
        {
            Initialize();
            return _data.Description;
        }
    }

    /// <summary>
    /// Gets the manufacturer of the profile.
    /// </summary>
    public string? Manufacturer
    {
        get
        {
            Initialize();
            return _data.Manufacturer;
        }
    }

    /// <summary>
    /// Gets the model of the profile.
    /// </summary>
    public string? Model
    {
        get
        {
            Initialize();
            return _data.Model;
        }
    }

    [MemberNotNull(nameof(_data))]
    private void Initialize()
        => _data = ColorProfileReader.Read(GetData());
}
