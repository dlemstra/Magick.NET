// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Xml;
using System.Xml.XPath;

#if !NET20
using System.Xml.Linq;
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that contains an XMP profile.
    /// </summary>
    public interface IXmpProfile : IImageProfile
    {
        /// <summary>
        /// Creates a XmlReader that can be used to read the data of the profile.
        /// </summary>
        /// <returns>A <see cref="XmlReader"/>.</returns>
        XmlReader CreateReader();

        /// <summary>
        /// Converts this instance to an IXPathNavigable.
        /// </summary>
        /// <returns>A <see cref="IXPathNavigable"/>.</returns>
        IXPathNavigable ToIXPathNavigable();

#if !NET20
        /// <summary>
        /// Converts this instance to a XDocument.
        /// </summary>
        /// <returns>A <see cref="XDocument"/>.</returns>
        XDocument ToXDocument();
#endif
    }
}