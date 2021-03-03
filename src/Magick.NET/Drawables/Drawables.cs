// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to chain draw actions.
    /// </summary>
    [SuppressMessage("Naming", "CA1710", Justification = "No need to use Collection suffix.")]
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
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public ITypeMetric FontTypeMetrics(string text)
            => FontTypeMetrics(text, false);

        /// <summary>
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public ITypeMetric FontTypeMetrics(string text, bool ignoreNewlines)
        {
            using (var image = new MagickImage(MagickColors.Transparent, 1, 1))
            {
                using (DrawingWand wand = new DrawingWand(image))
                {
                    wand.Draw(this);

                    return wand.FontTypeMetrics(text, ignoreNewlines);
                }
            }
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
}
