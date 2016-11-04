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

namespace ImageMagick
{
  /// <summary>
  /// Class that contains setting for the montage operation.
  /// </summary>
  public sealed partial class MontageSettings
  {
    private static string Convert(MagickGeometry geometry)
    {
      if (geometry == null)
        return null;

      return geometry.ToString();
    }

    private INativeInstance CreateNativeInstance()
    {
      NativeMontageSettings instance = new NativeMontageSettings();
      instance.SetBackgroundColor(BackgroundColor);
      instance.SetBorderColor(BorderColor);
      instance.SetBorderWidth(BorderWidth);
      instance.SetFillColor(FillColor);
      instance.SetFont(Font);
      instance.SetFontPointsize(FontPointsize);
      instance.SetFrameGeometry(Convert(FrameGeometry));
      instance.SetGeometry(Convert(Geometry));
      instance.SetGravity(Gravity);
      instance.SetShadow(Shadow);
      instance.SetStrokeColor(StrokeColor);
      instance.SetTextureFileName(TextureFileName);
      instance.SetTileGeometry(Convert(TileGeometry));
      instance.SetTitle(Title);

      return instance;
    }

    /// <summary>
    /// Initializes a new instance of the MontageSettings class.
    /// </summary>
    public MontageSettings()
    {
    }

    /// <summary>
    /// Color that thumbnails are composed on
    /// </summary>
    public MagickColor BackgroundColor
    {
      get;
      set;
    }

    /// <summary>
    /// Frame border color
    /// </summary>
    public MagickColor BorderColor
    {
      get;
      set;
    }

    /// <summary>
    /// Pixels between thumbnail and surrounding frame
    /// </summary>
    public int BorderWidth
    {
      get;
      set;
    }

    /// <summary>
    /// Fill color
    /// </summary>
    public MagickColor FillColor
    {
      get;
      set;
    }

    /// <summary>
    /// Label font
    /// </summary>
    public string Font
    {
      get;
      set;
    }

    /// <summary>
    /// Font point size
    /// </summary>
    public int FontPointsize
    {
      get;
      set;
    }

    /// <summary>
    /// Frame geometry (width &amp; height frame thickness)
    /// </summary>
    public MagickGeometry FrameGeometry
    {
      get;
      set;
    }

    /// <summary>
    /// Thumbnail width &amp; height plus border width &amp; height
    /// </summary>
    public MagickGeometry Geometry
    {
      get;
      set;
    }

    /// <summary>
    /// Thumbnail position (e.g. SouthWestGravity)
    /// </summary>
    public Gravity Gravity
    {
      get;
      set;
    }

    /// <summary>
    /// Thumbnail label (applied to image prior to montage)
    /// </summary>
    public string Label
    {
      get;
      set;
    }

    /// <summary>
    /// Enable drop-shadows on thumbnails
    /// </summary>
    public bool Shadow
    {
      get;
      set;
    }

    /// <summary>
    /// Outline color
    /// </summary>
    public MagickColor StrokeColor
    {
      get;
      set;
    }

    /// <summary>
    /// Background texture image
    /// </summary>
    public string TextureFileName
    {
      get;
      set;
    }

    /// <summary>
    /// Frame geometry (width &amp; height frame thickness)
    /// </summary>
    public MagickGeometry TileGeometry
    {
      get;
      set;
    }

    /// <summary>
    /// Montage title
    /// </summary>
    public string Title
    {
      get;
      set;
    }

    /// <summary>
    /// Transparent color
    /// </summary>
    public MagickColor TransparentColor
    {
      get;
      set;
    }
  }
}