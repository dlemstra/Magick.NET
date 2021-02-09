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

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create various instances.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickFactory<TQuantumType> : IMagickFactory
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickColorFactory{TQuantumType}"/> instances.
        /// </summary>
        IMagickColorFactory<TQuantumType> Color { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IDrawables{QuantumType}"/> instances.
        /// </summary>
        IDrawablesFactory<TQuantumType> Drawables { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImage{TQuantumType}"/> instances.
        /// </summary>
        IMagickImageFactory<TQuantumType> Image { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImageCollection{TQuantumType}"/> instances.
        /// </summary>
        IMagickImageCollectionFactory<TQuantumType> ImageCollection { get; }

        /// <summary>
        /// Gets the quantum information of this image.
        /// </summary>
        IQuantumInfo<TQuantumType> QuantumInfo { get; }

        /// <summary>
        /// Gets a factory that can be used to create various settings.
        /// </summary>
        ISettingsFactory<TQuantumType> Settings { get; }
    }
}
