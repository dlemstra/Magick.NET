// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <summary>
/// Interface that contains the same colors as System.Drawing.Colors.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
[MagickColorsGenerator]
public partial interface IMagickColors<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #663399FF.
    /// </summary>
    IMagickColor<TQuantumType> RebeccaPurple { get; }
}
