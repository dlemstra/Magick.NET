// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Class that contains the same colors as System.Drawing.Colors.
/// </summary>
[MagickColors]
public partial class MagickColors : IMagickColors<QuantumType>
{
    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #663399FF.
    /// </summary>
    public static MagickColor RebeccaPurple
        => MagickColor.FromRgba(102, 51, 153, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #663399FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.RebeccaPurple
        => RebeccaPurple;
}
