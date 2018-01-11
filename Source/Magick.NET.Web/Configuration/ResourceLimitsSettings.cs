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

using System.Configuration;

namespace ImageMagick.Web
{
    /// <summary>
    /// Class that contains the settings for the resource limits.
    /// </summary>
    public class ResourceLimitsSettings : ConfigurationElement
    {
        /// <summary>
        /// Gets the maximum height of an image.
        /// </summary>
        [ConfigurationProperty("height")]
        public int? Height
        {
            get
            {
                return (int?)this["height"];
            }
        }

        /// <summary>
        /// Gets the maximum width of an image.
        /// </summary>
        [ConfigurationProperty("width")]
        public int? Width
        {
            get
            {
                return (int?)this["width"];
            }
        }
    }
}
