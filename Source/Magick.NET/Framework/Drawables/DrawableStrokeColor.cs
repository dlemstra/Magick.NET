// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETSTANDARD1_3

using System.Drawing;

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public sealed partial class DrawableStrokeColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableStrokeColor"/> class.
        /// </summary>
        /// <param name="color">The color to use.</param>
        public DrawableStrokeColor(Color color)
        {
            Color = color;
        }
    }
}

#endif