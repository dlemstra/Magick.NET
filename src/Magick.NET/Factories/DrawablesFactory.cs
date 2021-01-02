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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickColor{QuantumType}"/> instances.
    /// </summary>
    public sealed class DrawablesFactory : IDrawablesFactory<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IDrawables{QuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IDrawables{QuantumType}"/> instance.</returns>
        public IDrawables<QuantumType> Create()
             => new Drawables();
    }
}
