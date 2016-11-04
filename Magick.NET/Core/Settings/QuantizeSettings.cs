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

namespace ImageMagick
{
  /// <summary>
  /// Class that contains setting for quantize operations.
  /// </summary>
  public sealed partial class QuantizeSettings
  {
    private INativeInstance CreateNativeInstance()
    {
      NativeQuantizeSettings instance = new NativeQuantizeSettings();
      instance.SetColors(Colors);
      instance.SetColorSpace(ColorSpace);
      instance.SetDitherMethod(DitherMethod.HasValue ? DitherMethod.Value : ImageMagick.DitherMethod.No);
      instance.SetMeasureErrors(MeasureErrors);
      instance.SetTreeDepth(TreeDepth);

      return instance;
    }

    /// <summary>
    /// Initializes a new instance of the QuantizeSettings class.
    /// </summary>
    public QuantizeSettings()
    {
      Colors = 1;
      DitherMethod = ImageMagick.DitherMethod.Riemersma;
    }

    /// <summary>
    /// Maximum number of colors to quantize to.
    /// </summary>
    public int Colors
    {
      get;
      set;
    }

    /// <summary>
    /// Colorspace to quantize in.
    /// </summary>
    public ColorSpace ColorSpace
    {
      get;
      set;
    }

    /// <summary>
    /// Dither method to use.
    /// </summary>
    public DitherMethod? DitherMethod
    {
      get;
      set;
    }

    /// <summary>
    /// Measure errors.
    /// </summary>
    public bool MeasureErrors
    {
      get;
      set;
    }

    /// <summary>
    /// Quantization tree-depth.
    /// </summary>
    public int TreeDepth
    {
      get;
      set;
    }
  }
}