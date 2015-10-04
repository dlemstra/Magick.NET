//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.IO;
using System.Xml;
using System.Xml.XPath;

#if !(NET20)
using System.Xml.Linq;
#endif

namespace ImageMagick
{
  ///<summary>
  /// Class that contains an XMP profile.
  ///</summary>
  public sealed class XmpProfile : ImageProfile
  {
    private static byte[] CheckTrailingNULL(byte[] data)
    {
      Throw.IfNull("data", data);

      int length = data.Length;

      while (length > 2)
      {
        if (data[length - 1] != '\0')
          break;

        length--;
      }

      if (length == data.Length)
        return data;

      byte[] result = new byte[length];
      Buffer.BlockCopy(data, 0, result, 0, length);
      return result;
    }

    ///<summary>
    /// Initializes a new instance of the XmpProfile class.
    ///</summary>
    ///<param name="data">A byte array containing the profile.</param>
    public XmpProfile(Byte[] data)
      : base("xmp", CheckTrailingNULL(data))
    {
    }

    ///<summary>
    /// Initializes a new instance of the XmpProfile class.
    ///</summary>
    ///<param name="document">A document containing the profile.</param>
    public XmpProfile(IXPathNavigable document)
      : base("xmp")
    {

      Throw.IfNull("document", document);

      MemoryStream memStream = new MemoryStream();
      using (XmlWriter writer = XmlWriter.Create(memStream))
      {
        document.CreateNavigator().WriteSubtree(writer);
        writer.Flush();
        Data = memStream.ToArray();
      }
    }

#if !(NET20)
    ///<summary>
    /// Initializes a new instance of the XmpProfile class.
    ///</summary>
    ///<param name="document">A document containing the profile.</param>
    public XmpProfile(XDocument document)
      : base("xmp")
    {
      Throw.IfNull("document", document);

      MemoryStream memStream = new MemoryStream();
      using (XmlWriter writer = XmlWriter.Create(memStream))
      {
        document.WriteTo(writer);
        writer.Flush();
        Data = memStream.ToArray();
      }
    }
#endif

    ///<summary>
    /// Initializes a new instance of the XmpProfile class.
    ///</summary>
    ///<param name="stream">A stream containing the profile.</param>
    public XmpProfile(Stream stream)
      : base("xmp", stream)
    {
    }

    ///<summary>
    /// Initializes a new instance of the XmpProfile class.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public XmpProfile(string fileName)
      : base("xmp", fileName)
    {
    }

    ///<summary>
    /// Creates a XmlReader that can be used to read the data of the profile.
    ///</summary>
    public XmlReader CreateReader()
    {
      MemoryStream memStream = new MemoryStream(Data, 0, Data.Length);
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.CloseInput = true;
      return XmlReader.Create(memStream, settings);
    }

    ///<summary>
    /// Creates an instance from the specified IXPathNavigable.
    ///</summary>
    ///<param name="document">A document containing the profile.</param>
    public static XmpProfile FromIXPathNavigable(IXPathNavigable document)
    {
      return new XmpProfile(document);
    }

#if !(NET20)
    ///<summary>
    /// Creates an instance from the specified IXPathNavigable.
    ///</summary>
    ///<param name="document">A document containing the profile.</param>
    public static XmpProfile FromXDocument(XDocument document)
    {
      return new XmpProfile(document);
    }
#endif

    ///<summary>
    /// Converts this instance to an IXPathNavigable.
    ///</summary>
    public IXPathNavigable ToIXPathNavigable()
    {
      using (XmlReader reader = CreateReader())
      {
        XmlDocument result = new XmlDocument();
        result.Load(reader);
        return result;
      }
    }

#if !(NET20)
    ///<summary>
    /// Converts this instance to a XDocument.
    ///</summary>
    public XDocument ToXDocument()
    {
      using (XmlReader reader = CreateReader())
      {
        return XDocument.Load(reader);
      }
    }
#endif
  }
}