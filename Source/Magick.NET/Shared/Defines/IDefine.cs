﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Interface for a define.
    /// </summary>
    public interface IDefine
    {
        /// <summary>
        /// Gets the format to set the define for.
        /// </summary>
        MagickFormat Format
        {
            get;
        }

        /// <summary>
        /// Gets the name of the define.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the value of the define.
        /// </summary>
        string Value
        {
            get;
        }
    }
}