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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageMagick
{
  ///<summary>
  /// Class that contains setting for when an image is being read.
  ///</summary>
  public sealed class MagickReadSettings
  {
    private Collection<IDefine> _Defines;

    ///<summary>
    /// Initializes a new instance of the MagickReadSettings class.
    ///</summary>
    public MagickReadSettings()
    {
      _Defines = new Collection<IDefine>();
    }

    /// <summary>
    /// Returns all the defines that are set in this MagickReadSettings instance.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IDefine> AllDefines
    {
      get
      {
        foreach (IDefine define in _Defines)
        {
          yield return define;
        }

        if (Defines != null)
        {
          foreach (IDefine define in Defines.Defines)
          {
            yield return define;
          }
        }
      }
    }

    ///<summary>
    /// Color space.
    ///</summary>
    public ColorSpace? ColorSpace
    {
      get;
      set;
    }

    ///<summary>
    /// Defines that should be set before the image is read.
    ///</summary>
    public IReadDefines Defines
    {
      get;
      set;
    }

    ///<summary>
    /// Vertical and horizontal resolution in pixels.
    ///</summary>
    public PointD? Density
    {
      get;
      set;
    }

    ///<summary>
    /// The format of the image.
    ///</summary>
    public MagickFormat? Format
    {
      get;
      set;
    }

    ///<summary>
    /// The index of the image to read from a multi layer/frame image.
    ///</summary>
    public int? FrameIndex
    {
      get;
      set;
    }

    ///<summary>
    /// The number of images to read from a multi layer/frame image.
    ///</summary>
    public int? FrameCount
    {
      get;
      set;
    }

    ///<summary>
    /// The height.
    ///</summary>
    public int? Height
    {
      get;
      set;
    }

    ///<summary>
    /// The settings for pixel storage.
    ///</summary>
    public PixelStorageSettings PixelStorage
    {
      get;
      set;
    }

    ///<summary>
    /// Use monochrome reader.
    ///</summary>
    public bool? UseMonochrome
    {
      get;
      set;
    }

    ///<summary>
    /// The width.
    ///</summary>
    public int? Width
    {
      get;
      set;
    }

    ///<summary>
    /// Sets a format-specific option.
    ///</summary>
    ///<param name="format">The format to set the option for.</param>
    ///<param name="name">The name of the option.</param>
    ///<param name="value">The value of the option.</param>
    public void SetDefine(MagickFormat format, string name, string value)
    {
      Throw.IfNullOrEmpty("name", name);
      Throw.IfNull("value", value);

      _Defines.Add(new MagickDefine(format, name, value));
    }
  }
}