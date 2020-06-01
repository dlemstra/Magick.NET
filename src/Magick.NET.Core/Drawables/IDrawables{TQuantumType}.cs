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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to chain draw actions.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    [SuppressMessage("Naming", "CA1710", Justification = "No need to use Collection suffix.")]
    public partial interface IDrawables<TQuantumType> : IEnumerable<IDrawable>
        where TQuantumType : struct
    {
        /// <summary>
        /// Draw on the specified image.
        /// </summary>
        /// <param name="image">The image to draw on.</param>
        /// <returns>The current instance.</returns>
        IDrawables<TQuantumType> Draw(IMagickImage<TQuantumType> image);

        /// <summary>
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        ITypeMetric FontTypeMetrics(string text);

        /// <summary>
        /// Obtain font metrics for text string given current font, pointsize, and density settings.
        /// </summary>
        /// <param name="text">The text to get the font metrics for.</param>
        /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
        /// <returns>The font metrics for text.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        ITypeMetric FontTypeMetrics(string text, bool ignoreNewlines);

        /// <summary>
        /// Creates a new <see cref="Paths"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Paths"/> instance.</returns>
        IPaths<TQuantumType> Paths();
    }
}
