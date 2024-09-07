// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that contains an ICM/ICC color profile.
/// </summary>
public sealed class ColorProfile : ImageProfile, IColorProfile
{
    private static readonly object _syncRoot = new object();
    private static readonly Dictionary<string, ColorProfile> _profiles = [];

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
    public ColorProfile(string name, byte[] data)
      : base(name, data)
    {
    }

    /// <summary>
    /// Gets the AdobeRGB1998 profile.
    /// </summary>
    public static ColorProfile AdobeRGB1998
        => Load("ImageMagick.Resources.ColorProfiles.RGB", "AdobeRGB1998.icc");

    /// <summary>
    /// Gets the AppleRGB profile.
    /// </summary>
    public static ColorProfile AppleRGB
        => Load("ImageMagick.Resources.ColorProfiles.RGB", "AppleRGB.icc");

    /// <summary>
    /// Gets the CoatedFOGRA39 profile.
    /// </summary>
    public static ColorProfile CoatedFOGRA39
        => Load("ImageMagick.Resources.ColorProfiles.CMYK", "CoatedFOGRA39.icc");

    /// <summary>
    /// Gets the ColorMatchRGB profile.
    /// </summary>
    public static ColorProfile ColorMatchRGB
        => Load("ImageMagick.Resources.ColorProfiles.RGB", "ColorMatchRGB.icc");

    /// <summary>
    /// Gets the sRGB profile.
    /// </summary>
    public static ColorProfile SRGB
        => Load("ImageMagick.Resources.ColorProfiles.RGB", "SRGB.icm");

    /// <summary>
    /// Gets the USWebCoatedSWOP profile.
    /// </summary>
    public static ColorProfile USWebCoatedSWOP
        => Load("ImageMagick.Resources.ColorProfiles.CMYK", "USWebCoatedSWOP.icc");

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

    private static ColorProfile Load(string resourcePath, string resourceName)
    {
        if (!_profiles.ContainsKey(resourceName))
        {
            lock (_syncRoot)
            {
                if (!_profiles.ContainsKey(resourceName))
                {
                    using var stream = TypeHelper.GetManifestResourceStream(typeof(ColorProfile), resourcePath, resourceName);
                    _profiles[resourceName] = new ColorProfile(stream);
                }
            }
        }

        return _profiles[resourceName];
    }

    [MemberNotNull(nameof(_data))]
    private void Initialize()
        => _data = ColorProfileReader.Read(GetData());
}
