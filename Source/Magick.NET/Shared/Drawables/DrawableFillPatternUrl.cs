// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Sets the URL to use as a fill pattern for filling objects. Only local URLs("#identifier") are
    /// supported at this time. These local URLs are normally created by defining a named fill pattern
    /// with DrawablePushPattern/DrawablePopPattern.
    /// </summary>
    public sealed class DrawableFillPatternUrl : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableFillPatternUrl"/> class.
        /// </summary>
        /// <param name="url">Url specifying pattern ID (e.g. "#pattern_id").</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "Url won't work in all situations.")]
        public DrawableFillPatternUrl(string url)
        {
            Url = url;
        }

        /// <summary>
        /// Gets or sets the url specifying pattern ID (e.g. "#pattern_id").
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Url won't work in all situations.")]
        public string Url
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
                wand.FillPatternUrl(Url);
        }
    }
}