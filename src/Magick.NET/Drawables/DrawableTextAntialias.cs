// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Controls whether text is antialiased. Text is antialiased by default.
    /// </summary>
    public sealed class DrawableTextAntialias : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextAntialias"/> class.
        /// </summary>
        /// <param name="isEnabled">True if text antialiasing is enabled otherwise false.</param>
        public DrawableTextAntialias(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether text antialiasing is enabled or disabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.TextAntialias(IsEnabled);
    }
}