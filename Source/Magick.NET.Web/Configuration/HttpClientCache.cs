//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;
using System.Configuration;

namespace ImageMagick.Web
{
    /// <summary>
    /// Class that contains the client cache settings.
    /// </summary>
    public sealed class HttpClientCache : ConfigurationElement
    {
        /// <summary>
        /// Gets the mode to use for client caching.
        /// </summary>
        [ConfigurationProperty("cacheControlMode", DefaultValue = CacheControlMode.UseMaxAge)]
        public CacheControlMode CacheControlMode
        {
            get
            {
                return (CacheControlMode)this["cacheControlMode"];
            }
        }

        /// <summary>
        /// Gets the HTTP 1.1 cache control maximum age value
        /// </summary>
        [ConfigurationProperty("cacheControlMaxAge", DefaultValue = "1.00:00:00")]
        public TimeSpan CacheControlMaxAge
        {
            get
            {
                return (TimeSpan)this["cacheControlMaxAge"];
            }
        }
    }
}
