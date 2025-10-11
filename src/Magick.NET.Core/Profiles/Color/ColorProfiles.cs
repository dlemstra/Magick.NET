// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick;

/// <summary>
/// Class that contains ICM/ICC color profiles.
/// </summary>
public sealed class ColorProfiles
{
    private static readonly Dictionary<string, ColorProfile> _profiles = [];
    private static readonly object _syncRoot = new object();

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
}
