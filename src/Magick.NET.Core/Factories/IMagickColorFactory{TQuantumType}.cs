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

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickColor{TQuantumType}"/> instances.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickColorFactory<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> Create();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        /// <param name="color">The color to use.</param>
        IMagickColor<TQuantumType> Create(IMagickColor<TQuantumType> color);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> Create(TQuantumType red, TQuantumType green, TQuantumType blue);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        /// <param name="alpha">Alpha component value of this color.</param>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> Create(TQuantumType red, TQuantumType green, TQuantumType blue, TQuantumType alpha);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <param name="cyan">Cyan component value of this color.</param>
        /// <param name="magenta">Magenta component value of this color.</param>
        /// <param name="yellow">Yellow component value of this color.</param>
        /// <param name="black">Black component value of this color.</param>
        /// <param name="alpha">Alpha component value of this colors.</param>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> Create(TQuantumType cyan, TQuantumType magenta, TQuantumType yellow, TQuantumType black, TQuantumType alpha);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColor{TQuantumType}"/>.
        /// </summary>
        /// <param name="color">The RGBA/CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
        /// For example: #F000, #FF000000, #FFFF000000000000.</param>
        /// <returns>A new <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> Create(string color);
    }
}
