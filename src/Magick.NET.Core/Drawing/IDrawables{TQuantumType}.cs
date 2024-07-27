// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using ImageMagick.SourceGenerator;

namespace ImageMagick.Drawing;

/// <summary>
/// Class that can be used to chain draw actions.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
[Drawables]
public partial interface IDrawables<TQuantumType> : IEnumerable<IDrawable>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Applies the DrawableComposite operation to the <see cref="IDrawables{TQuantumType}" />.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="image">The image to draw.</param>
    /// <returns>The <see cref="IDrawables{TQuantumType}" /> instance.</returns>
    IDrawables<TQuantumType> Composite(double x, double y, CompositeOperator compose, IMagickImage<TQuantumType> image);

    /// <summary>
    /// Encapsulation of the DrawableDensity object.
    /// </summary>
    /// <param name="density">Gets the vertical and horizontal resolution.</param>
    /// <returns>The <see cref="IDrawables{TQuantumType}" /> instance.</returns>
    IDrawables<TQuantumType> Density(double density);

    /// <summary>
    /// Draw on the specified image.
    /// </summary>
    /// <param name="image">The image to draw on.</param>
    /// <returns>The current instance.</returns>
    IDrawables<TQuantumType> Draw(IMagickImage<TQuantumType> image);

    /// <summary>
    /// Applies the DrawableFont operation to the <see cref="IDrawables{TQuantumType}" />.
    /// </summary>
    /// <param name="family">The font family or the full path to the font file.</param>
    /// <returns>The <see cref="IDrawables{TQuantumType}" /> instance.</returns>
    IDrawables<TQuantumType> Font(string family);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text, bool ignoreNewlines);

    /// <summary>
    /// Creates a new <see cref="Paths"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Paths"/> instance.</returns>
    IPaths<TQuantumType> Paths();
}
