// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that implements IDefine
    /// </summary>
    public sealed class MagickDefine : IDefine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(string name, string value)
        {
            Format = MagickFormat.Unknown;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(MagickFormat format, string name, string value)
        {
            Format = format;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the format to set the define for.
        /// </summary>
        public MagickFormat Format
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the define.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the define.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }
    }
}