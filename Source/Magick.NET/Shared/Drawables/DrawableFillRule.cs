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

namespace ImageMagick
{
    /// <summary>
    /// Sets the fill rule to use while drawing polygons.
    /// </summary>
    public sealed class DrawableFillRule : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableFillRule"/> class.
        /// </summary>
        /// <param name="fillRule">The rule to use when filling drawn objects.</param>
        public DrawableFillRule(FillRule fillRule)
        {
            FillRule = fillRule;
        }

        /// <summary>
        /// Gets or sets the rule to use when filling drawn objects.
        /// </summary>
        public FillRule FillRule
        {
            get;
            set;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.FillRule(FillRule);
        }
    }
}