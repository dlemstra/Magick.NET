// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick.Web
{
    /// <summary>
    /// Class that contains the settings for the url resolvers.
    /// </summary>
    [ConfigurationCollection(typeof(UrlResolverSettings), AddItemName = "urlResolver")]
    [SuppressMessage("Design", "CA1010:Collections should implement generic interface", Justification = "The interface will not used.")]
    public class UrlResolverSettingsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlResolverSettings"/> class.
        /// </summary>
        /// <returns>A new instance of the <see cref="UrlResolverSettings"/>  class.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new UrlResolverSettings();
        }

        /// <summary>
        /// Gets the element key for a specified <see cref="UrlResolverSettings"/> element.
        /// </summary>
        /// <param name="element">The <see cref="UrlResolverSettings"/>  to return the key for.</param>
        /// <returns>The element key for a specified <see cref="UrlResolverSettings"/> element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UrlResolverSettings)element).TypeName;
        }
    }
}
