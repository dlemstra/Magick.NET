// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the 8bim profile.
    /// </summary>
    public interface IEightBimValue : IEquatable<IEightBimValue>
    {
        /// <summary>
        /// Gets the ID of the 8bim value.
        /// </summary>
        short ID { get; }

        /// <summary>
        /// Converts this instance to a byte array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray();

        /// <summary>
        /// Returns a string that represents the current value with the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <returns>A string that represents the current value with the specified encoding.</returns>
        string ToString(Encoding encoding);
    }
}
