// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Used to obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    public interface ITypeMetric
    {
        /// <summary>
        /// Gets the ascent, the distance in pixels from the text baseline to the highest/upper grid coordinate
        /// used to place an outline point.
        /// </summary>
        double Ascent { get; }

        /// <summary>
        /// Gets the descent, the distance in pixels from the baseline to the lowest grid coordinate used to
        /// place an outline point. Always a negative value.
        /// </summary>
        double Descent { get; }

        /// <summary>
        /// Gets the maximum horizontal advance in pixels.
        /// </summary>
        double MaxHorizontalAdvance { get; }

        /// <summary>
        /// Gets the text height in pixels.
        /// </summary>
        double TextHeight { get; }

        /// <summary>
        /// Gets the text width in pixels.
        /// </summary>
        double TextWidth { get; }

        /// <summary>
        /// Gets the underline position.
        /// </summary>
        double UnderlinePosition { get; }

        /// <summary>
        /// Gets the underline thickness.
        /// </summary>
        double UnderlineThickness { get; }
    }
}