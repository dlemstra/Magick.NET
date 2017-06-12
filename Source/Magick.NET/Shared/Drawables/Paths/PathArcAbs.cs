//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Draws an elliptical arc from the current point to (X, Y) using absolute coordinates. The size
    /// and orientation of the ellipse are defined by two radii(RadiusX, RadiusY) and an RotationX,
    /// which indicates how the ellipse as a whole is rotated relative to the current coordinate
    /// system. The center of the ellipse is calculated automagically to satisfy the constraints
    /// imposed by the other parameters. UseLargeArc and UseSweep contribute to the automatic
    /// calculations and help determine how the arc is drawn. If UseLargeArc is true then draw the
    /// larger of the available arcs. If UseSweep is true, then draw the arc matching a clock-wise
    /// rotation.
    /// </summary>
    public sealed class PathArcAbs : IPath, IDrawingWand
    {
        private PathArcCoordinates _Coordinates;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathArcAbs"/> class.
        /// </summary>
        /// <param name="pathArcs">The coordinates to use.</param>
        public PathArcAbs(params PathArc[] pathArcs)
        {
            _Coordinates = new PathArcCoordinates(pathArcs);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathArcAbs"/> class.
        /// </summary>
        /// <param name="pathArcs">The coordinates to use.</param>
        public PathArcAbs(IEnumerable<PathArc> pathArcs)
        {
            _Coordinates = new PathArcCoordinates(pathArcs);
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.PathArcAbs(_Coordinates.ToList());
        }
    }
}