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

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick connected component object.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IConnectedComponent<TQuantumType>
    {
        /// <summary>
        /// Gets the pixel count of the area.
        /// </summary>
        int Area { get; }

        /// <summary>
        /// Gets the centroid of the area.
        /// </summary>
        PointD Centroid { get; }

        /// <summary>
        /// Gets the color of the area.
        /// </summary>
        IMagickColor<TQuantumType> Color { get; }

        /// <summary>
        /// Gets the height of the area.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the id of the area.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the width of the area.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the X offset from origin.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets the Y offset from origin.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Returns the geometry of the area of this connected component.
        /// </summary>
        /// <returns>The geometry of the area of this connected component.</returns>
        IMagickGeometry ToGeometry();

        /// <summary>
        /// Returns the geometry of the area of this connected component.
        /// </summary>
        /// <param name="extent">The number of pixels to extent the image with.</param>
        /// <returns>The geometry of the area of this connected component.</returns>
        IMagickGeometry ToGeometry(int extent);
    }
}
