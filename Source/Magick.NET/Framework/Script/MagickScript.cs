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
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "No harm in doing that here.")]
        private static XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

#if Q8
            string resourcePath = "Magick.NET.Resources.ReleaseQ8";
#elif Q16
            string resourcePath = "Magick.NET.Resources.ReleaseQ16";
#elif Q16HDRI
            string resourcePath = "Magick.NET.Resources.ReleaseQ16_HDRI";
#else
#error Not implemented!
#endif
            using (Stream resourceStream = TypeHelper.GetManifestResourceStream(typeof(MagickScript), resourcePath, "MagickScript.xsd"))
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