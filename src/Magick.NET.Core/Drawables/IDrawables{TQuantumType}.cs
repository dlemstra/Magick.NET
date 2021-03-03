// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to chain draw actions.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    [SuppressMessage("Naming", "CA1710", Justification = "No need to use Collection suffix.")]
    public partial interface IDrawables<TQuantumType> : IEnumerable<IDrawable>
        where TQuantumType : struct
    {
        /// <summary>
        /// Draw on the specified image.
        /// </summary>
        /// <param name="image">The image to draw on.</param>
        /// <returns>The current instance.</returns>
        IDrawables<TQuantumType> Draw(IMagickImage<TQuantumType> image);

        /// <summary>
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        ITypeMetric FontTypeMetrics(string text);

        /// <summary>
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        ITypeMetric FontTypeMetrics(string text, bool ignoreNewlines);

        /// <summary>
        /// Creates a new <see cref="Paths"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Paths"/> instance.</returns>
        IPaths<TQuantumType> Paths();
    }
}
