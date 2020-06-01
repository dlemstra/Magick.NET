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
    /// Interface that can be used to access the individual pixels of an image.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IUnsafePixelCollection<TQuantumType> : IPixelCollection<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Returns a pointer to the pixels of the specified area.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <returns>A pointer to the pixels of the specified area.</returns>
        IntPtr GetAreaPointer(int x, int y, int width, int height);

        /// <summary>
        /// Returns a pointer to the pixels of the specified area.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <returns>A pointer to the pixels of the specified area.</returns>
        IntPtr GetAreaPointer(IMagickGeometry geometry);
    }
}
