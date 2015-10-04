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
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

#if !(NET20)
using System.Xml.Linq;
#endif

namespace ImageMagick
{
  ///<summary>
  /// Class that can be used to execute a Magick Script Language file.
  ///</summary>
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
  public sealed partial class MagickScript
  {
    private static XmlReaderSettings _ReaderSettings = CreateXmlReaderSettings();

    private XmlDocument _Script;

    private MagickImage CreateMagickImage(XmlElement element)
    {
      Throw.IfNull("element", element);

      MagickImage image = null;

      MagickReadSettings settings = CreateMagickReadSettings((XmlElement)element.SelectSingleNode("settings"));

      string fileName = element.GetAttribute("fileName");
      if (!string.IsNullOrEmpty(fileName))
      {
        if (settings != null)
          image = new MagickImage(fileName, settings);
        else
          image = new MagickImage(fileName);
      }
      else
      {
        if (Read == null)
          throw new InvalidOperationException("The Read event should be bound when the fileName attribute is not set.");

        string id = element.GetAttribute("id");

        ScriptReadEventArgs eventArgs = new ScriptReadEventArgs(id, settings);

        Read(this, eventArgs);

        if (eventArgs.Image == null)
          throw new InvalidOperationException("The Image property should not be null after the Read event has been raised.");

        image = eventArgs.Image;
      }

      Execute(element, image);

      return image;
    }

    private Collection<IPath> CreatePaths(XmlElement element)
    {
      Collection<IPath> paths = new Collection<IPath>();

      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        ExecuteIPath(elem, paths);
      }

      return paths;
    }

    private ImageProfile CreateProfile(XmlElement element)
    {
      XmlElement elem = (XmlElement)element.SelectSingleNode("*");

      if (elem.Name == "imageProfile")
        return CreateImageProfile(elem);
      else if (elem.Name == "colorProfile")
        return CreateColorProfile(elem);

      throw new NotImplementedException(elem.Name);
    }

    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    private static XmlReaderSettings CreateXmlReaderSettings()
    {
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.ValidationType = ValidationType.Schema;
      settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
      settings.IgnoreComments = true;
      settings.IgnoreWhitespace = true;

#if Q8
      string resourceName = "ImageMagick.Resources.ReleaseQ8.MagickScript.xsd";
#elif Q16
      string resourceName = "ImageMagick.Resources.ReleaseQ16.MagickScript.xsd";
#elif Q16HDRI
      string resourceName = "ImageMagick.Resources.ReleaseQ16_HDRI.MagickScript.xsd";
#else
#error Not implemented!
#endif
      using (Stream resourceStream = Assembly.GetAssembly(typeof(MagickScript)).GetManifestResourceStream(resourceName))
      {
        using (XmlReader xmlReader = XmlReader.Create(resourceStream))
        {
          settings.Schemas.Add("", xmlReader);
        }
      }

      return settings;
    }

    private void Execute(XmlElement element, MagickImage image)
    {
      foreach (XmlElement elem in element.SelectNodes("*[name() != 'settings']"))
      {
        ExecuteImage(elem, image);
      }
    }

    private MagickImage Execute(XmlElement element, MagickImageCollection collection)
    {
      if (element.Name == "read")
      {
        collection.Add(CreateMagickImage(element));
        return null;
      }

      if (element.Name == "write")
      {
        string fileName_ = XmlHelper.GetAttribute<string>(element, "fileName");
        collection.Write(fileName_);
        return null;
      }

      return ExecuteCollection(element, collection);
    }

    private void ExecuteClone(XmlElement element, MagickImage image)
    {
      Execute(element, image.Clone());
    }

    private MagickImage ExecuteCollection(XmlElement element)
    {
      using (MagickImageCollection collection = new MagickImageCollection())
      {
        foreach (XmlElement elem in element.SelectNodes("*"))
        {
          MagickImage result = Execute(elem, collection);
          if (result != null)
            return result;
        }
        return null;
      }
    }

    private void ExecuteDraw(XmlElement element, MagickImage image)
    {
      Collection<IDrawable> drawables = new Collection<IDrawable>();

      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        ExecuteIDrawable(elem, drawables);
      }

      image.Draw(drawables);
    }

    private void ExecuteWrite(XmlElement element, MagickImage image)
    {
      string fileName = element.GetAttribute("fileName");
      if (!string.IsNullOrEmpty(fileName))
      {
        image.Write(fileName);
      }
      else
      {
        if (Write == null)
          throw new InvalidOperationException("The Write event should be bound when the fileName attribute is not set.");

        string id = element.GetAttribute("id");

        ScriptWriteEventArgs eventArgs = new ScriptWriteEventArgs(id, image);
        Write(this, eventArgs);
      }
    }

    private void Initialize(Stream stream)
    {
      Throw.IfNull("stream", stream);

      using (XmlReader xmlReader = XmlReader.Create(stream, _ReaderSettings))
      {
        _Script = new XmlDocument();
        _Script.Load(xmlReader);
      }

      Variables = new ScriptVariables(_Script);
    }

    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    private void Initialize(XPathNavigator navigator)
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        using (XmlWriter writer = XmlWriter.Create(memStream))
        {
          navigator.WriteSubtree(writer);
          writer.Flush();
          memStream.Position = 0;
          Initialize(memStream);
        }
      }
    }

    private static bool OnlyContains(Hashtable arguments, params object[] keys)
    {
      if (arguments.Count != keys.Length)
        return false;

      foreach (object key in keys)
      {
        if (!arguments.ContainsKey(key))
          return false;
      }

      return true;
    }

    ///<summary>
    /// Initializes a new instance of the MagickScript class using the specified IXPathNavigable.
    ///</summary>
    ///<param name="xml">The IXPathNavigable that contains the script.</param>
    public MagickScript(IXPathNavigable xml)
    {
      Throw.IfNull("xml", xml);
      Initialize(xml.CreateNavigator());
    }

    ///<summary>
    /// Initializes a new instance of the MagickScript class using the specified filename.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the script file, or the relative script file name.</param>
    public MagickScript(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      using (FileStream stream = File.OpenRead(filePath))
      {
        Initialize(stream);
      }
    }

    ///<summary>
    /// Initializes a new instance of the MagickScript class using the specified stream.
    ///</summary>
    ///<param name="stream">The stream to read the script data from.</param>
    public MagickScript(Stream stream)
    {
      Initialize(stream);
    }

#if !(NET20)
    ///<summary>
    /// Initializes a new instance of the MagickScript class using the specified XElement.
    ///</summary>
    ///<param name="xml">The XElement that contains the script.</param>
    public MagickScript(XElement xml)
    {
      Throw.IfNull("xml", xml);

      Initialize(System.Xml.XPath.Extensions.CreateNavigator(xml));
    }
#endif

    ///<summary>
    /// The variables of this script.
    ///</summary>
    public ScriptVariables Variables
    {
      get;
      private set;
    }

    ///<summary>
    /// Event that will be raised when the script needs an image to be read.
    ///</summary>
    public event EventHandler<ScriptReadEventArgs> Read;

    ///<summary>
    /// Event that will be raised when the script needs an image to be written.
    ///</summary>
    public event EventHandler<ScriptWriteEventArgs> Write;

    ///<summary>
    /// Executes the script and returns the resulting image.
    ///</summary>
    public MagickImage Execute()
    {
      XmlElement element = (XmlElement)_Script.SelectSingleNode("/msl/*");

      if (element.Name == "read")
        return CreateMagickImage(element);
      else if (element.Name == "collection")
        return ExecuteCollection(element);
      else
        throw new NotImplementedException(element.Name);
    }

    ///<summary>
    /// Executes the script using the specified image.
    ///</summary>
    ///<param name="image">The image to execute the script on.</param>
    public void Execute(MagickImage image)
    {
      Throw.IfNull("image", image);

      XmlElement element = (XmlElement)_Script.SelectSingleNode("/msl/read");
      if (element == null)
        throw new InvalidOperationException("This method only works with a script that contains a single read operation.");

      Execute(element, image);
    }
  }
}