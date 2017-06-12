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
    /// Draws a set of paths
    /// </summary>
    public sealed class DrawablePath : IDrawable, IDrawingWand
    {
        private List<IPath> _Paths;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePath"/> class.
        /// </summary>
        /// <param name="paths">The paths to use.</param>
        public DrawablePath(params IPath[] paths)
        {
            _Paths = new List<IPath>(paths);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePath"/> class.
        /// </summary>
        /// <param name="paths">The paths to use.</param>
        public DrawablePath(IEnumerable<IPath> paths)
        {
            _Paths = new List<IPath>(paths);
        }

        /// <summary>
        /// Gets the paths to use.
        /// </summary>
        public IEnumerable<IPath> Paths
        {
            get
            {
                return _Paths;
            }
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand == null)
                return;

            wand.PathStart();
            foreach (IPath path in _Paths)
                ((IDrawingWand)path).Draw(wand);
            wand.PathFinish();
        }
    }
}