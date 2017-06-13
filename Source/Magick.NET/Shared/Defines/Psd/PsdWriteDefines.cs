//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick
{
    /// <summary>
    /// Class for defines that are used when a psd image is written.
    /// </summary>
    public sealed class PsdWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PsdWriteDefines"/> class.
        /// </summary>
        public PsdWriteDefines()
          : base(MagickFormat.Psd)
        {
        }

        /// <summary>
        /// Gets or sets which additional info should be written to the output file.
        /// </summary>
        public PsdAdditionalInfo AdditionalInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                yield return CreateDefine("additional-info", AdditionalInfo);
            }
        }
    }
}