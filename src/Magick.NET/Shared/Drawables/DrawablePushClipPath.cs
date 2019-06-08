// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Starts a clip path definition which is comprized of any number of drawing commands and
    /// terminated by a DrawablePopClipPath.
    /// </summary>
    public sealed class DrawablePushClipPath : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePushClipPath"/> class.
        /// </summary>
        /// <param name="clipPath">The ID of the clip path.</param>
        public DrawablePushClipPath(string clipPath)
        {
            ClipPath = clipPath;
        }

        /// <summary>
        /// Gets or sets the ID of the clip path.
        /// </summary>
        public string ClipPath { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.PushClipPath(ClipPath);
    }
}