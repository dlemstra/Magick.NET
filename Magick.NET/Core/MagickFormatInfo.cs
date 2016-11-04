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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ImageMagick
{
  /// <summary>
  /// Class that contains information about an image format.
  /// </summary>
  public sealed partial class MagickFormatInfo : IEquatable<MagickFormatInfo>
  {
    private static readonly Dictionary<MagickFormat, MagickFormatInfo> _All = LoadFormats();

    private MagickFormatInfo()
    {
    }

    private static MagickFormatInfo Create(NativeMagickFormatInfo instance)
    {
      MagickFormatInfo formatInfo = new MagickFormatInfo();
      formatInfo.Format = GetFormat(instance.Format);
      formatInfo.Description = instance.Description;
      formatInfo.CanReadMultithreaded = instance.CanReadMultithreaded;
      formatInfo.CanWriteMultithreaded = instance.CanWriteMultithreaded;
      formatInfo.IsMultiFrame = instance.IsMultiFrame;
      formatInfo.IsReadable = instance.IsReadable;
      formatInfo.IsWritable = instance.IsWritable;
      formatInfo.MimeType = instance.MimeType;
      formatInfo.Module = GetFormat(instance.Module);

      return formatInfo;
    }

    private static MagickFormatInfo Create(NativeMagickFormatInfo instance, string name)
    {
      instance.GetInfoByName(name);
      return Create(instance);
    }

    private static MagickFormat GetFormat(string format)
    {
      format = format.Replace("-", "");
      if (format == "3FR")
        format = "ThreeFr";

      return EnumHelper.Parse(format, MagickFormat.Unknown);
    }

    private static Dictionary<MagickFormat, MagickFormatInfo> LoadFormats()
    {
      Dictionary<MagickFormat, MagickFormatInfo> result = new Dictionary<MagickFormat, MagickFormatInfo>();

      IntPtr list = IntPtr.Zero;
      UIntPtr length = (UIntPtr)0;
      MagickFormatInfo formatInfo;
      NativeMagickFormatInfo instance = new NativeMagickFormatInfo();

      try
      {
        list = instance.CreateList(out length);

        IntPtr ptr = list;
        for (int i = 0; i < (int)length; i++)
        {
          instance.GetInfo(list, i);

          formatInfo = Create(instance);
          result[formatInfo.Format] = formatInfo;

          ptr = new IntPtr(ptr.ToInt64() + i);
        }

        /* stealth coders */

        formatInfo = Create(instance, "DIB");
        result[formatInfo.Format] = formatInfo;

        formatInfo = Create(instance, "TIF");
        result[formatInfo.Format] = formatInfo;

      }
      finally
      {
        if (list != IntPtr.Zero)
          NativeMagickFormatInfo.DisposeList(list, (int)length);
      }

      return result;
    }

    internal static IEnumerable<MagickFormatInfo> All
    {
      get
      {
        return _All.Values;
      }
    }

    /// <summary>
    /// Determines whether the specified MagickFormatInfo instances are considered equal.
    /// </summary>
    /// <param name="left">The first MagickFormatInfo to compare.</param>
    /// <param name="right"> The second MagickFormatInfo to compare.</param>
    /// <returns></returns>
    public static bool operator ==(MagickFormatInfo left, MagickFormatInfo right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified MagickFormatInfo instances are not considered equal.
    /// </summary>
    /// <param name="left">The first MagickFormatInfo to compare.</param>
    /// <param name="right"> The second MagickFormatInfo to compare.</param>
    /// <returns></returns>
    public static bool operator !=(MagickFormatInfo left, MagickFormatInfo right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// The format can read multithreaded.
    /// </summary>
    public bool CanReadMultithreaded
    {
      get;
      private set;
    }

    /// <summary>
    /// The format can write multithreaded.
    /// </summary>
    public bool CanWriteMultithreaded
    {
      get;
      private set;
    }

    /// <summary>
    /// The description of the format.
    /// </summary>
    public string Description
    {
      get;
      private set;
    }

    /// <summary>
    /// The format.
    /// </summary>
    public MagickFormat Format
    {
      get;
      private set;
    }

    /// <summary>
    /// Format supports multiple frames.
    /// </summary>
    public bool IsMultiFrame
    {
      get;
      private set;
    }

    /// <summary>
    /// Format is readable.
    /// </summary>
    public bool IsReadable
    {
      get;
      private set;
    }

    /// <summary>
    /// Format is writable.
    /// </summary>
    public bool IsWritable
    {
      get;
      private set;
    }

    /// <summary>
    /// The mime type.
    /// </summary>
    public string MimeType
    {
      get;
      private set;
    }

    /// <summary>
    /// The module.
    /// </summary>
    public MagickFormat Module
    {
      get;
      private set;
    }

    /// <summary>
    /// Returns the format information. The extension of the supplied file is used to determine
    /// the format.
    /// </summary>
    /// <param name="file">The file to check.</param>
    public static MagickFormatInfo Create(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      MagickFormat? format = null;
      if (file.Extension != null && file.Extension.Length > 1)
        format = EnumHelper.Parse<MagickFormat>(file.Extension.Substring(1));

      if (format == null)
        return null;

      return Create(format.Value);
    }

    /// <summary>
    /// Returns the format information of the specified format.
    /// </summary>
    /// <param name="format">The image format.</param>
    public static MagickFormatInfo Create(MagickFormat format)
    {
      if (!_All.ContainsKey(format))
        return null;

      return _All[format];
    }

    /// <summary>
    /// Returns the format information. The extension of the supplied file name is used to
    /// determine the format.
    /// </summary>
    /// <param name="fileName">The name of the file to check.</param>
    public static MagickFormatInfo Create(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfNull(nameof(fileName), filePath);

      return Create(new FileInfo(filePath));
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current MagickFormatInfo.
    /// </summary>
    /// <param name="obj">The object to compare this MagickFormatInfo with.</param>
    public override bool Equals(object obj)
    {
      return Equals(obj as MagickFormatInfo);
    }

    /// <summary>
    /// Determines whether the specified MagickFormatInfo is equal to the current MagickFormatInfo.
    /// </summary>
    /// <param name="other">The MagickFormatInfo to compare this MagickFormatInfo with.</param>
    public bool Equals(MagickFormatInfo other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return Format == other.Format;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    public override int GetHashCode()
    {
      return Module.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current format.
    /// </summary>
    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2}R{3}W{4}M)", Format, Description,
        IsReadable ? "+" : "-", IsWritable ? "+" : "-", IsMultiFrame ? "+" : "-");
    }

    /// <summary>
    /// Unregisters this format.
    /// </summary>
    public bool Unregister()
    {
      return NativeMagickFormatInfo.Unregister(EnumHelper.GetName(Format));
    }
  }
}