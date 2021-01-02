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

namespace ImageMagick.Defines
{
    /// <summary>
    /// Base class that can create write defines.
    /// </summary>
    public abstract class WriteDefinesCreator : DefinesCreator, IWriteDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteDefinesCreator"/> class.
        /// </summary>
        /// <param name="format">The format where the defines are for.</param>
        protected WriteDefinesCreator(MagickFormat format)
          : base(format)
        {
        }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        MagickFormat IWriteDefines.Format => Format;
    }
}