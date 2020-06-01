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
    /// Class that can be used to create various settings.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface ISettingsFactory<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="ICompareSettings{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="ICompareSettings{TQuantumType}"/> instance.</returns>
        ICompareSettings<TQuantumType> CreateCompareSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IComplexSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IComplexSettings"/> instance.</returns>
        IComplexSettings CreateComplexSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IConnectedComponentsSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IConnectedComponentsSettings"/> instance.</returns>
        IConnectedComponentsSettings CreateConnectedComponentsSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IDeskewSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IDeskewSettings"/> instance.</returns>
        IDeskewSettings CreateDeskewSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IDistortSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IDistortSettings"/> instance.</returns>
        IDistortSettings CreateDistortSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IKmeansSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IKmeansSettings"/> instance.</returns>
        IKmeansSettings CreateKmeansSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickReadSettings{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
        IMagickReadSettings<TQuantumType> CreateMagickReadSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMontageSettings{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
        IMontageSettings<TQuantumType> CreateMontageSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMorphologySettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IMorphologySettings"/> instance.</returns>
        IMorphologySettings CreateMorphologySettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IPixelReadSettings{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IPixelReadSettings{TQuantumType}"/> instance.</returns>
        IPixelReadSettings<TQuantumType> CreatePixelReadSettings();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IQuantizeSettings"/>.
        /// </summary>
        /// <returns>A new <see cref="IQuantizeSettings"/> instance.</returns>
        IQuantizeSettings CreateQuantizeSettings();
    }
}
