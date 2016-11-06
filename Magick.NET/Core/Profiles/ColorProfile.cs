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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace ImageMagick
{
  /// <summary>
  /// Class that contains an ICM/ICC color profile.
  /// </summary>
  public sealed class ColorProfile : ImageProfile
  {
    private static readonly object _SyncRoot = new object();
    private static Dictionary<string, ColorProfile> _Profiles = new Dictionary<string, ColorProfile>();

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
    private static ColorSpace DetermineColorSpace(string colorSpace)
    {
      switch (colorSpace)
      {
        case "CMY":
          return ColorSpace.CMY;
        case "CMYK":
          return ColorSpace.CMYK;
        case "GRAY":
          return ColorSpace.Gray;
        case "HLS":
          return ColorSpace.HSL;
        case "HSV":
          return ColorSpace.HSV;
        case "Lab":
          return ColorSpace.Lab;
        case "Luv":
          return ColorSpace.YUV;
        case "RGB":
          return ColorSpace.sRGB;
        case "XYZ":
          return ColorSpace.XYZ;
        case "YCbr":
          return ColorSpace.YCbCr;
        case "Yxy":
          return ColorSpace.XyY;
        default:
          throw new NotSupportedException(colorSpace);
      }
    }

    private void Initialize()
    {
      ColorSpace = ColorSpace.Undefined;
      if (Data.Length < 20)
        return;

      string colorSpace = Encoding.ASCII.GetString(Data, 16, 4).TrimEnd();
      ColorSpace = DetermineColorSpace(colorSpace);
    }

    private static ColorProfile Load(string resourcePath, string resourceName)
    {
      lock (_SyncRoot)
      {
        if (!_Profiles.ContainsKey(resourceName))
        {
          using (Stream stream = TypeHelper.GetManifestResourceStream(typeof(ColorProfile), resourcePath, resourceName))
          {
            _Profiles[resourceName] = new ColorProfile(stream);
          }
        }
      }

      return _Profiles[resourceName];
    }

    internal ColorProfile(string name, byte[] data)
      : base(name, data)
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="data">A byte array containing the profile.</param>
    public ColorProfile(Byte[] data)
      : base("icc", data)
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="stream">A stream containing the profile.</param>
    public ColorProfile(Stream stream)
      : base("icc", stream)
    {
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public ColorProfile(string fileName)
      : base("icc", fileName)
    {
    }

    /// <summary>
    /// Gets the color space of the profile.
    /// </summary>
    public ColorSpace ColorSpace
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the AdobeRGB1998 profile.
    /// </summary>
    public static ColorProfile AdobeRGB1998
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.RGB", "AdobeRGB1998.icc");
      }
    }

    /// <summary>
    /// Gets the AppleRGB profile.
    /// </summary>
    public static ColorProfile AppleRGB
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.RGB", "AppleRGB.icc");
      }
    }

    /// <summary>
    /// Gets the CoatedFOGRA39 profile.
    /// </summary>
    public static ColorProfile CoatedFOGRA39
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.CMYK", "CoatedFOGRA39.icc");
      }
    }

    /// <summary>
    /// Gets the ColorMatchRGB profile.
    /// </summary>
    public static ColorProfile ColorMatchRGB
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.RGB", "ColorMatchRGB.icc");
      }
    }

    /// <summary>
    /// Gets the sRGB profile.
    /// </summary>
    public static ColorProfile SRGB
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.RGB", "SRGB.icm");
      }
    }

    /// <summary>
    /// Gets the USWebCoatedSWOP profile.
    /// </summary>
    public static ColorProfile USWebCoatedSWOP
    {
      get
      {
        return Load("ImageMagick.Resources.ColorProfiles.CMYK", "USWebCoatedSWOP.icc");
      }
    }
  }
}