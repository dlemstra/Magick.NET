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

namespace ImageMagick
{
  ///<summary>
  /// Class that contains setting for the montage operation.
  ///</summary>
  public sealed class MontageSettings
  {
    private Wrapper.MontageSettings _Instance;

    internal static Wrapper.MontageSettings GetInstance(MontageSettings value)
    {
      if (value == null)
        return null;

      return value._Instance;
    }

    ///<summary>
    /// Initializes a new instance of the MagickReadSettings class.
    ///</summary>
    public MontageSettings()
    {
      _Instance = new Wrapper.MontageSettings();
    }

    ///<summary>
    /// Initializes a new instance of the MagickReadSettings class.
    ///</summary>
    public MontageSettings(MontageMode mode)
    {
      _Instance = new Wrapper.MontageSettings(mode);
    }

    ///<summary>
    /// Color that thumbnails are composed on
    ///</summary>
    public MagickColor BackgroundColor
    {
      get
      {
        return MagickColor.Create(_Instance.BackgroundColor);
      }
      set
      {
        _Instance.BackgroundColor = MagickColor.GetInstance(value);
      }
    }

    ///<summary>
    /// Frame border color
    ///</summary>
    public MagickColor BorderColor
    {
      get
      {
        return MagickColor.Create(_Instance.BorderColor);
      }
      set
      {
        _Instance.BorderColor = MagickColor.GetInstance(value);
      }
    }

    ///<summary>
    /// Pixels between thumbnail and surrounding frame
    ///</summary>
    public int BorderWidth
    {
      get
      {
        return _Instance.BorderWidth;
      }
      set
      {
        _Instance.BorderWidth = value;
      }
    }

    ///<summary>
    /// Composition algorithm to use (e.g. ReplaceCompositeOp)
    ///</summary>
    public CompositeOperator Compose
    {
      get
      {
        return _Instance.Compose;
      }
      set
      {
        _Instance.Compose = value;
      }
    }

    ///<summary>
    /// Fill color
    ///</summary>
    public MagickColor FillColor
    {
      get
      {
        return MagickColor.Create(_Instance.FillColor);
      }
      set
      {
        _Instance.FillColor = MagickColor.GetInstance(value);
      }
    }

    ///<summary>
    /// Label font
    ///</summary>
    public string Font
    {

      get
      {
        return _Instance.Font;
      }
      set
      {
        _Instance.Font = value;
      }
    }

    ///<summary>
    /// Font point size
    ///</summary>
    public int FontPointsize
    {
      get
      {
        return _Instance.FontPointsize;
      }
      set
      {
        _Instance.FontPointsize = value;
      }
    }

    ///<summary>
    /// Frame geometry (width &amp; height frame thickness)
    ///</summary>
    public MagickGeometry FrameGeometry
    {
      get
      {
        return MagickGeometry.Create(_Instance.FrameGeometry);
      }
      set
      {
        _Instance.FrameGeometry = MagickGeometry.GetInstance(value);
      }
    }

    ///<summary>
    /// Thumbnail width &amp; height plus border width &amp; height
    ///</summary>
    public MagickGeometry Geometry
    {
      get
      {
        return MagickGeometry.Create(_Instance.Geometry);
      }
      set
      {
        _Instance.Geometry = MagickGeometry.GetInstance(value);
      }
    }

    ///<summary>
    /// Thumbnail position (e.g. SouthWestGravity)
    ///</summary>
    public Gravity Gravity
    {
      get
      {
        return _Instance.Gravity;
      }
      set
      {
        _Instance.Gravity = value;
      }
    }

    ///<summary>
    /// Thumbnail label (applied to image prior to montage)
    ///</summary>
    public string Label
    {
      get
      {
        return _Instance.Label;
      }
      set
      {
        _Instance.Label = value;
      }
    }

    ///<summary>
    /// Enable drop-shadows on thumbnails
    ///</summary>
    public bool Shadow
    {
      get
      {
        return _Instance.Shadow;
      }
      set
      {
        _Instance.Shadow = value;
      }
    }

    ///<summary>
    /// Outline color
    ///</summary>
    public MagickColor StrokeColor
    {
      get
      {
        return MagickColor.Create(_Instance.StrokeColor);
      }
      set
      {
        _Instance.StrokeColor = MagickColor.GetInstance(value);
      }
    }

    ///<summary>
    /// Background texture image
    ///</summary>
    public string TextureFileName
    {
      get
      {
        return _Instance.TextureFileName;
      }
      set
      {
        _Instance.TextureFileName = value;
      }
    }

    ///<summary>
    /// Frame geometry (width &amp; height frame thickness)
    ///</summary>
    public MagickGeometry TileGeometry
    {

      get
      {
        return MagickGeometry.Create(_Instance.TileGeometry);
      }
      set
      {
        _Instance.TileGeometry = MagickGeometry.GetInstance(value);
      }
    }

    ///<summary>
    /// Montage title
    ///</summary>
    public string Title
    {
      get
      {
        return _Instance.Title;
      }
      set
      {
        _Instance.Title = value;
      }
    }

    ///<summary>
    /// Transparent color
    ///</summary>
    public MagickColor TransparentColor
    {
      get
      {
        return MagickColor.Create(_Instance.TransparentColor);
      }
      set
      {
        _Instance.TransparentColor = MagickColor.GetInstance(value);
      }
    }
  }
}