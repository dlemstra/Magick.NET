// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ImageMagick.Drawing;

/// <summary>
/// Class that can be used to chain draw actions.
/// </summary>
[DrawablesAttribute]
public sealed partial class Drawables : IDrawables<QuantumType>
{
    private readonly Collection<IDrawable> _drawables;

    /// <summary>
    /// Initializes a new instance of the <see cref="Drawables"/> class.
    /// </summary>
    public Drawables()
    {
        _drawables = new Collection<IDrawable>();
    }

    /// <summary>
    /// Applies the DrawableComposite operation to the <see cref="Drawables" />.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="image">The image to draw.</param>
    /// <returns>The <see cref="Drawables" /> instance.</returns>
    public IDrawables<QuantumType> Composite(double x, double y, CompositeOperator compose, IMagickImage<QuantumType> image)
    {
        _drawables.Add(new DrawableComposite(x, y, compose, image));
        return this;
    }

    /// <summary>
    /// Applies the DrawableDensity operation to the <see cref="Drawables" />.
    /// </summary>
    /// <param name="density">The vertical and horizontal resolution.</param>
    /// <returns>The <see cref="Drawables" /> instance.</returns>
    public IDrawables<QuantumType> Density(double density)
    {
        _drawables.Add(new DrawableDensity(density));
        return this;
    }

    /// <summary>
    /// Draw on the specified image.
    /// </summary>
    /// <param name="image">The image to draw on.</param>
    /// <returns>The current instance.</returns>
    public IDrawables<QuantumType> Draw(IMagickImage<QuantumType> image)
    {
        Throw.IfNull(nameof(image), image);

        image.Draw(this);
        return this;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => _drawables.GetEnumerator();

    /// <summary>
    /// Applies the DrawableFont operation to the <see cref="Drawables" />.
    /// </summary>
    /// <param name="family">The font family or the full path to the font file.</param>
    /// <returns>The <see cref="Drawables" /> instance.</returns>
    public IDrawables<QuantumType> Font(string family)
    {
        _drawables.Add(new DrawableFont(family));
        return this;
    }

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public ITypeMetric? FontTypeMetrics(string text)
        => FontTypeMetrics(text, false);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public ITypeMetric? FontTypeMetrics(string text, bool ignoreNewlines)
    {
        using var image = new MagickImage(MagickColors.Transparent, 1, 1);
        using var wand = new DrawingWand(image);
        wand.Draw(this);

        return wand.FontTypeMetrics(text, ignoreNewlines);
    }

    /// <summary>
    /// Creates a new <see cref="Paths"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Paths"/> instance.</returns>
    public IPaths<QuantumType> Paths()
        => new Paths(this);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator.</returns>
    public IEnumerator<IDrawable> GetEnumerator()
        => _drawables.GetEnumerator();
}
