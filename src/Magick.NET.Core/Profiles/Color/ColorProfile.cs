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
