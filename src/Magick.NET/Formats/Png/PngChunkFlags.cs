// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Formats;

/// <summary>
/// Specifies the chunks to be included or excluded in the PNG image.
/// This is a flags enumeration, allowing a bitwise combination of its member values.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "The lowercase names are consistent with the naming conventions of PNG chunk types as defined in the PNG specification.")]
[Flags]
public enum PngChunkFlags
{
    /// <summary>
    /// No chunks specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Include or exclude all chunks.
    /// </summary>
    All = bKGD | cHRM | EXIF | gAMA | iCCP | iTXt | sRGB | tEXt | zCCP | zTXt | date,

    /// <summary>
    /// Include or exclude bKGD chunk.
    /// </summary>
    bKGD = 1,

    /// <summary>
    /// Include or exclude cHRM chunk.
    /// </summary>
    cHRM = 2,

    /// <summary>
    /// Include or exclude EXIF chunk.
    /// </summary>
    EXIF = 4,

    /// <summary>
    /// Include or exclude gAMA chunk.
    /// </summary>
    gAMA = 8,

    /// <summary>
    /// Include or exclude iCCP chunk.
    /// </summary>
    iCCP = 16,

    /// <summary>
    /// Include or exclude iTXt chunk.
    /// </summary>
    iTXt = 32,

    /// <summary>
    /// Include or exclude sRGB chunk.
    /// </summary>
    sRGB = 64,

    /// <summary>
    /// Include or exclude tEXt chunk.
    /// </summary>
    tEXt = 128,

    /// <summary>
    /// Include or exclude zCCP chunk.
    /// </summary>
    zCCP = 256,

    /// <summary>
    /// Include or exclude zTXt chunk.
    /// </summary>
    zTXt = 512,

    /// <summary>
    /// Include or exclude date chunk.
    /// </summary>
    date = 1024,
}
