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

using System.Globalization;

namespace ImageMagick
{
  /// <summary>
  /// Class that contains setting for when an image is being read.
  /// </summary>
  public sealed class MagickReadSettings : MagickSettings
  {
    private string GetScenes()
    {
      if (!FrameIndex.HasValue && !FrameCount.HasValue)
        return null;

      if (FrameIndex.HasValue && (!FrameCount.HasValue || FrameCount.Value == 1))
        return FrameIndex.Value.ToString(CultureInfo.InvariantCulture);

      int frame = FrameIndex.HasValue ? FrameIndex.Value : 0;
      return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", frame, frame + FrameCount.Value);
    }

    private void ApplyDefines(MagickReadSettings settings)
    {
      if (settings.Defines == null)
        return;

      foreach (IDefine define in settings.Defines.Defines)
      {
        SetOption(GetDefineKey(define), define.Value);
      }
    }

    private void ApplyDimensions(MagickReadSettings settings)
    {
      if (settings.Width.HasValue && settings.Height.HasValue)
        Size = settings.Width + "x" + settings.Height;
      else if (settings.Width.HasValue)
        Size = settings.Width + "x";
      else if (settings.Height.HasValue)
        Size = "x" + settings.Height;
    }

    private void ApplyFrame(MagickReadSettings settings)
    {
      if (!settings.FrameIndex.HasValue && !settings.FrameCount.HasValue)
        return;

      Scenes = settings.GetScenes();
      Scene = settings.FrameIndex.HasValue ? settings.FrameIndex.Value : 0;
      NumberScenes = settings.FrameCount.HasValue ? settings.FrameCount.Value : 1;
    }

    private static string GetDefineKey(IDefine define)
    {
      if (define.Format == MagickFormat.Unknown)
        return define.Name;

      return EnumHelper.GetName(define.Format) + ":" + define.Name;
    }

    internal MagickReadSettings(MagickSettings settings)
    {
      Copy(settings);
    }

    internal MagickReadSettings(MagickReadSettings settings)
    {
      Copy(settings);

      ApplyDefines(settings);
      ApplyDimensions(settings);
      ApplyFrame(settings);

      PixelStorage = settings.PixelStorage?.Clone();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickReadSettings"/> class.
    /// </summary>
    public MagickReadSettings()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickReadSettings"/> class with the specified defines.
    /// </summary>
    /// <param name="readDefines">The read defines to set.</param>
    public MagickReadSettings(IReadDefines readDefines)
    {
      SetDefines(readDefines);
    }

    /// <summary>
    /// Defines that should be set before the image is read.
    /// </summary>
    public IReadDefines Defines
    {
      get;
      set;
    }

    /// <summary>
    /// The index of the image to read from a multi layer/frame image.
    /// </summary>
    public int? FrameIndex
    {
      get;
      set;
    }

    /// <summary>
    /// The number of images to read from a multi layer/frame image.
    /// </summary>
    public int? FrameCount
    {
      get;
      set;
    }

    /// <summary>
    /// The height.
    /// </summary>
    public int? Height
    {
      get;
      set;
    }

    /// <summary>
    /// The settings for pixel storage.
    /// </summary>
    public PixelStorageSettings PixelStorage
    {
      get;
      set;
    }

    /// <summary>
    /// Use monochrome reader. This is supported by: PCL, PDF, PS and XPS.
    /// </summary>
    public bool UseMonochrome
    {
      get
      {
        return Monochrome;
      }
      set
      {
        Monochrome = value;
      }
    }

    /// <summary>
    /// The width.
    /// </summary>
    public int? Width
    {
      get;
      set;
    }
  }
}