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

using System.Xml.XPath;

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    public sealed class ClipPath : IClipPath
    {
        internal ClipPath(string name, IXPathNavigable path)
        {
            Name = name;
            Path = path;
        }

        /// <summary>
        /// Gets the name of the clipping path.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the path of the clipping path.
        /// </summary>
        public IXPathNavigable Path { get; }
    }
}
