// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    public sealed class PathMoveToAbs : IPath, IDrawingWand
    {
        private readonly PointD _coordinate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public PathMoveToAbs(double x, double y)
         : this(new PointD(x, y))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
        /// </summary>
        /// <param name="coordinate">The coordinate to use.</param>
        public PathMoveToAbs(PointD coordinate)
        {
            _coordinate = coordinate;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.PathMoveToAbs(_coordinate.X, _coordinate.Y);
    }
}