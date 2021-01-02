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

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickImageFactory{QuantumType}"/> interface.
    /// </summary>
    public static class IMagickImageFactoryExtensions
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageFactory{TQuantumType}"/>.
        /// </summary>
        /// <param name="self">The image factory.</param>
        /// <param name="bitmap">The bitmap to use.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        /// <returns>A new <see cref="IMagickImage{QuantumType}"/> instance.</returns>
        public static IMagickImage<TQuantumType> Create<TQuantumType>(this IMagickImageFactory<TQuantumType> self, Bitmap bitmap)
            where TQuantumType : struct
        {
            Throw.IfNull(nameof(self), self);

            var image = self.Create();
            image.Read(bitmap);

            return image;
        }
    }
}