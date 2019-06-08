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
using System.IO;

namespace ImageMagick.Web
{
    /// <summary>
    /// Defines an interface that is used to get the image data.
    /// </summary>
    internal interface IImageData
    {
        /// <summary>
        /// Gets the format information of the image.
        /// </summary>
        MagickFormatInfo FormatInfo
        {
            get;
        }

        /// <summary>
        /// Gets the unique identifier of the image.
        /// </summary>
        string ImageId
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the image data is valid.
        /// </summary>
        bool IsValid
        {
            get;
        }

        /// <summary>
        /// Gets the modification time of the image.
        /// </summary>
        DateTime ModifiedTimeUtc
        {
            get;
        }

        /// <summary>
        /// Reads the image.
        /// </summary>
        /// <returns>The stream containing the image.</returns>
        Stream ReadImage();

        /// <summary>
        /// Reads the image.
        /// </summary>
        /// <param name="settings">The settings to use when reading the image.</param>
        /// <returns>An image.</returns>
        MagickImage ReadImage(MagickReadSettings settings);

        /// <summary>
        /// Saves the image to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to write the image to.</param>
        void SaveImage(string fileName);
    }
}