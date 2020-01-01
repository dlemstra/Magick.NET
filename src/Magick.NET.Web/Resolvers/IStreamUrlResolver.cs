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
using System.IO;

namespace ImageMagick.Web
{
    /// <summary>
    /// Defines an interface that is used to resolve a file and script from the specified request.
    /// </summary>
    public interface IStreamUrlResolver : IUrlResolver
    {
        /// <summary>
        /// Gets the unqiue ID of the image.
        /// </summary>
        string ImageId
        {
            get;
        }

        /// <summary>
        /// Gets the time the image was last modified.
        /// </summary>
        DateTime ModifiedTimeUtc
        {
            get;
        }

        /// <summary>
        /// Returns a stream that can be used to get the data of the image. This stream will be
        /// disposed after it has been used.
        /// </summary>
        /// <returns>A stream that can be used to get the data of the image.</returns>
        Stream OpenStream();
    }
}