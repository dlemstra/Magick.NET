// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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