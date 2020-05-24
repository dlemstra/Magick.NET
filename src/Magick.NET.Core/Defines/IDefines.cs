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

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Interface for an object that specifies defines for an image.
    /// </summary>
    public interface IDefines
    {
        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        IEnumerable<IDefine> Defines { get; }
    }
}