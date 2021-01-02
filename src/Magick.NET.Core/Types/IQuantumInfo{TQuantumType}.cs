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
    /// Class that can be used to acquire information about the quantum.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IQuantumInfo<TQuantumType>
    {
        /// <summary>
        /// Gets the quantum depth.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets the maximum value of the quantum.
        /// </summary>
        TQuantumType Max { get; }

        /// <summary>
        /// Returns an instance that has a double as the quantum type.
        /// </summary>
        /// <returns>An instance that has a double as the quantum type.</returns>
        IQuantumInfo<double> ToDouble();
    }
}
