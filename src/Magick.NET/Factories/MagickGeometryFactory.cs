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
    /// Class that can be used to create <see cref="IMagickGeometry"/> instances.
    /// </summary>
    public sealed class MagickGeometryFactory : IMagickGeometryFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create()
            => new MagickGeometry();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="widthAndHeight">The width and height.</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(int widthAndHeight)
            => new MagickGeometry(widthAndHeight);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(int width, int height)
            => new MagickGeometry(width, height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="x">The X offset from origin.</param>
        /// <param name="y">The Y offset from origin.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(int x, int y, int width, int height)
            => new MagickGeometry(x, y, width, height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="percentageWidth">The percentage of the width.</param>
        /// <param name="percentageHeight">The percentage of the  height.</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(Percentage percentageWidth, Percentage percentageHeight)
            => new MagickGeometry(percentageWidth, percentageHeight);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="x">The X offset from origin.</param>
        /// <param name="y">The Y offset from origin.</param>
        /// <param name="percentageWidth">The percentage of the width.</param>
        /// <param name="percentageHeight">The percentage of the  height.</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(int x, int y, Percentage percentageWidth, Percentage percentageHeight)
            => new MagickGeometry(x, y, percentageWidth, percentageHeight);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickGeometry"/>.
        /// </summary>
        /// <param name="value">Geometry specifications in the form: &lt;width&gt;x&lt;height&gt;
        /// {+-}&lt;xoffset&gt;{+-}&lt;yoffset&gt; (where width, height, xoffset, and yoffset are numbers).</param>
        /// <returns>A new <see cref="IMagickGeometry"/> instance.</returns>
        public IMagickGeometry Create(string value)
            => new MagickGeometry(value);
    }
}
