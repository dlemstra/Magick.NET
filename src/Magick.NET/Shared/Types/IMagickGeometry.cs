// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Encapsulation of the ImageMagick geometry object.
    /// </summary>
    public interface IMagickGeometry : IEquatable<IMagickGeometry>, IComparable<IMagickGeometry>
    {
        /// <summary>
        /// Gets a value indicating whether the value is an aspect ratio.
        /// </summary>
        bool AspectRatio { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized based on the smallest fitting dimension (^).
        /// </summary>
        bool FillArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized if image is greater than size (&gt;).
        /// </summary>
        bool Greater { get; set; }

        /// <summary>
        /// Gets or sets the height of the geometry.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized without preserving aspect ratio (!).
        /// </summary>
        bool IgnoreAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the width and height are expressed as percentages.
        /// </summary>
        bool IsPercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized if the image is less than size (&lt;).
        /// </summary>
        bool Less { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized using a pixel area count limit (@).
        /// </summary>
        bool LimitPixels { get; set; }

        /// <summary>
        /// Gets or sets the width of the geometry.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the X offset from origin.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Y offset from origin.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Returns a <see cref="PointD"/> that represents the position of the current <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <returns>A <see cref="PointD"/> that represents the position of the current <see cref="IMagickGeometry"/>.</returns>
        PointD ToPoint();

        /// <summary>
        /// Returns a string that represents the current <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="IMagickGeometry"/>.</returns>
        string ToString();
    }
}