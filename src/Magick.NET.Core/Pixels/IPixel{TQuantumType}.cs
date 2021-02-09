// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to access an individual pixel of an image.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IPixel<TQuantumType> : IEquatable<IPixel<TQuantumType>>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets the number of channels that the pixel contains.
        /// </summary>
        int Channels { get; }

        /// <summary>
        /// Gets the X coordinate of the pixel.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets the Y coordinate of the pixel.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Returns the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the value for.</param>
        TQuantumType this[int channel] { get; set; }

        /// <summary>
        /// Returns the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to get the value of.</param>
        /// <returns>The value of the specified channel.</returns>
        TQuantumType GetChannel(int channel);

        /// <summary>
        /// Set the value of the specified channel.
        /// </summary>
        /// <param name="channel">The channel to set the value of.</param>
        /// <param name="value">The value.</param>
        void SetChannel(int channel, TQuantumType value);

        /// <summary>
        /// Sets the values of this pixel.
        /// </summary>
        /// <param name="values">The values.</param>
        void SetValues(TQuantumType[] values);

        /// <summary>
        /// Returns the value of this pixel as an array.
        /// </summary>
        /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
        TQuantumType[] ToArray();

        /// <summary>
        /// Converts the pixel to a color. Assumes the pixel is RGBA.
        /// </summary>
        /// <returns>A <see cref="IMagickColor{TQuantumType}"/> instance.</returns>
        IMagickColor<TQuantumType> ToColor();
    }
}
