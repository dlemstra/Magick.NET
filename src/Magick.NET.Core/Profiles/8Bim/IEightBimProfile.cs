// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that describes an 8bim profile.
/// </summary>
public interface IEightBimProfile : IImageProfile
{
    /// <summary>
    /// Gets the clipping paths this image contains.
    /// </summary>
    IReadOnlyCollection<IClipPath> ClipPaths { get; }

    /// <summary>
    /// Gets or sets the exif profile inside the 8bim profile.
    /// </summary>
    IExifProfile? ExifProfile { get; set; }

    /// <summary>
    /// Gets the values of this 8bim profile.
    /// </summary>
    IReadOnlyCollection<IEightBimValue> Values { get; }

    /// <summary>
    /// Gets the iptc profile inside the 8bim profile.
    /// </summary>
    /// <returns>The iptc profile.</returns>
    IIptcProfile? GetIptcProfile();

    /// <summary>
    /// Gets or sets the xmp profile inside the 8bim profile.
    /// </summary>
    /// <returns>The xmp profile.</returns>
    IXmpProfile? GetXmpProfile();

    /// <summary>
    /// Sets the iptc profile inside the 8bim profile.
    /// </summary>
    /// <param name="profile">The iptc profile.</param>
    void SetIptcProfile(IIptcProfile? profile);

    /// <summary>
    /// Sets the xmp profile inside the 8bim profile.
    /// </summary>
    /// <param name="profile">The xmp profile.</param>
    void SetXmpProfile(IXmpProfile? profile);
}
