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

using System.Configuration;

namespace ImageMagick.Web
{
    /// <summary>
    /// Class that contains the settings for the image optimization.
    /// </summary>
    public class OptimizationSettings : ConfigurationElement
    {
        /// <summary>
        /// Gets a value indicating whether the images should be optimized.
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool IsEnabled
        {
            get
            {
                return (bool)this["enabled"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether the images should be compress with any quality loss.
        /// </summary>
        [ConfigurationProperty("lossless", DefaultValue = true)]
        public bool Lossless
        {
            get
            {
                return (bool)this["lossless"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether various compression types will be used to find the smallest
        /// file. This process will take extra time because the file has to be written multiple times.
        /// </summary>
        [ConfigurationProperty("optimalCompression", DefaultValue = false)]
        public bool OptimalCompression
        {
            get
            {
                return (bool)this["optimalCompression"];
            }
        }
    }
}
