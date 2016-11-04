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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
	using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  /// <summary>
  ///  Class that represents a gray color.
  /// </summary>
  public sealed class ColorGray : ColorBase
  {
    private double _Shade;

    private ColorGray(MagickColor color)
      : base(color)
    {
      _Shade = Quantum.ScaleToQuantum(color.R);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateValue()
    {
      QuantumType gray = Quantum.ScaleToQuantum(Shade);
      Value.R = gray;
      Value.G = gray;
      Value.B = gray;
    }

    /// <summary>
    /// Initializes a new instance of the ColorGray class.
    /// </summary>
    /// <param name="shade">Value between 0.0 - 1.0.</param>
    public ColorGray(double shade)
      : base(new MagickColor(0, 0, 0))
    {
      Throw.IfTrue(nameof(shade), shade < 0.0 || shade > 1.0, "Invalid shade specified");

      Shade = shade;
    }

    /// <summary>
    /// The shade of this color (value between 0.0 - 1.0).
    /// </summary>
    public double Shade
    {
      get
      {
        return _Shade;
      }
      set
      {
        if (value < 0.0 || value > 1.0)
          return;

        _Shade = value;
      }
    }

    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static implicit operator ColorGray(MagickColor color)
    {
      return FromMagickColor(color);
    }

    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static ColorGray FromMagickColor(MagickColor color)
    {
      if (color == null)
        return null;

      return new ColorGray(color);
    }
  }
}