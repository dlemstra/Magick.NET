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
  /// Class that represents a YUV color.
  /// </summary>
  public sealed class ColorYUV : ColorBase
  {
    private double _U;
    private double _V;
    private double _Y;

    private ColorYUV(MagickColor color)
      : base(color)
    {
      _Y = (0.29900 * color.R) + (0.58700 * color.G) + (0.11400 * color.B);
      _U = (-0.14740 * color.R) - (0.28950 * color.G) + (0.43690 * color.B);
      _V = (0.61500 * color.R) - (0.51500 * color.G) - (0.10000 * color.B);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateValue()
    {
      Value.R = Quantum.ScaleToQuantum(_Y + (1.13980 * _V));
      Value.G = Quantum.ScaleToQuantum(_Y - (0.39380 * _U) - (0.58050 * _V));
      Value.B = Quantum.ScaleToQuantum(_Y + (2.02790 * _U));
    }

    /// <summary>
    /// Initializes a new instance of the ColorYUV class.
    /// </summary>
    /// <param name="y">Y component value of this color.</param>
    /// <param name="u">U component value of this color.</param>
    /// <param name="v">V component value of this color.</param>
    public ColorYUV(double y, double u, double v)
      : base(new MagickColor(0, 0, 0))
    {
      Throw.IfTrue(nameof(y), y < 0.0 || y > 1.0, "Invalid Y component.");
      Throw.IfTrue(nameof(u), u < -0.5 || u > 0.5, "Invalid Y component.");
      Throw.IfTrue(nameof(v), v < -0.5 || v > 0.5, "Invalid Y component.");

      _Y = y;
      _U = u;
      _V = v;
    }

    /// <summary>
    /// U component value of this color. (value beteeen -0.5 and 0.5)
    /// </summary>
    public double U
    {
      get
      {
        return _U;
      }
      set
      {
        if (value < -0.5 || value > 0.5)
          return;

        _U = value;
      }
    }

    /// <summary>
    /// V component value of this color. (value beteeen -0.5 and 0.5)
    /// </summary>
    public double V
    {
      get
      {
        return _V;
      }
      set
      {
        if (value < -0.5 || value > 0.5)
          return;

        _V = value;
      }
    }

    /// <summary>
    /// Y component value of this color. (value beteeen 0.0 and 1.0)
    /// </summary>
    public double Y
    {
      get
      {
        return _Y;
      }
      set
      {
        if (value < 0.0 || value > 1.0)
          return;

        _Y = value;
      }
    }


    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static implicit operator ColorYUV(MagickColor color)
    {
      return FromMagickColor(color);
    }

    /// <summary>
    /// Converts the specified MagickColor to an instance of this type.
    /// </summary>
    public static ColorYUV FromMagickColor(MagickColor color)
    {
      if (color == null)
        return null;

      return new ColorYUV(color);
    }
  }
}
