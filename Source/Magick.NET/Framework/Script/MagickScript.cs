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

#if !NETSTANDARD1_3

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public sealed partial class MagickScript
    {
        private static XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings,
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };

            using (Stream resourceStream = TypeHelper.GetManifestResourceStream(typeof(MagickScript), "Magick.NET.Resources", "MagickScript.xsd"))
            {
                using (XmlReader xmlReader = XmlReader.Create(resourceStream))
                {
                    settings.Schemas.Add(string.Empty, xmlReader);
                }
            }

            return settings;
        }
    }
}

#endif