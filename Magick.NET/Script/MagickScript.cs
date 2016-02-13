//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ImageMagick
{
  public sealed partial class MagickScript
  {
    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    private static XmlReaderSettings CreateXmlReaderSettings()
    {
      XmlReaderSettings settings = new XmlReaderSettings();

      settings.ValidationType = ValidationType.Schema;
      settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
      settings.IgnoreComments = true;
      settings.IgnoreWhitespace = true;

#if Q8
      string resourcePath = "ImageMagick.Resources.ReleaseQ8";
#elif Q16
      string resourcePath = "ImageMagick.Resources.ReleaseQ16";
#elif Q16HDRI
      string resourcePath = "ImageMagick.Resources.ReleaseQ16_HDRI";
#else
#error Not implemented!
#endif
      using (Stream resourceStream = TypeHelper.GetManifestResourceStream(typeof(MagickScript), resourcePath, "MagickScript.xsd"))
      {
        using (XmlReader xmlReader = XmlReader.Create(resourceStream))
        {
          settings.Schemas.Add("", xmlReader);
        }
      }

      return settings;
    }
  }
}