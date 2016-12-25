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
  /// Class that represents a HSL color.
  /// </summary>
  public sealed class ColorHSL : ColorBase
  {
    private void Initialize(double red, double green, double blue)
    {
      double quantumScale = 1.0 / Quantum.Max;
      double max = Math.Max(red, Math.Max(green, blue)) * quantumScale;
      double min = Math.Min(red, Math.Max(green, blue)) * quantumScale;
      double c = max - min;

      Lightness = (max + min) / 2.0;
      if (c <= 0.0)
      {
        Hue = 0.0;
        Saturation = 0.0;
      }
      else
      {
        if (Math.Abs(max - (quantumScale * red)) < double.Epsilon)
        {
          Hue = ((quantumScale * green) - (quantumScale * blue)) / c;
          if ((quantumScale * green) < (quantumScale * blue))
            Hue += 6.0;
        }
        else if (Math.Abs(max - (quantumScale * green)) < double.Epsilon)
          Hue = 2.0 + (((quantumScale * blue) - (quantumScale * red)) / c);
        else
          Hue = 4.0 + (((quantumScale * red) - (quantumScale * green)) / c);
        Hue *= 60.0 / 360.0;
        if (Lightness <= 0.5)
          Saturation = c / (2.0 * Lightness);
        else
          Saturation = c / (2.0 - (2.0 * Lightness));
      }
    }

    private ColorHSL(MagickColor color)
      : base(color)
    {
      Initialize(color.R, color.G, color.B);
    }

    /// <summary>
    /// Updates the color value in an inherited class.
    /// </summary>
    protected override void UpdateColor()
    {
      double c;
      double h = Hue * 360.0;
      if (Lightness <= 0.5)
        c = 2.0 * Lightness * Saturation;
      else
        c = (2.0 - (2.0 * Lightness)) * Saturation;
      double min = Lightness - (0.5 * c);
      h -= 360.0 * Math.Floor(h / 360.0);
      h /= 60.0;
      double x = c * (1.0 - Math.Abs(h - (2.0 * Math.Floor(h / 2.0)) - 1.0));
      switch ((int)Math.Floor(h))
      {
        case 0:
          Color.R = (QuantumType)(Quantum.Max * (min + c));
          Color.G = (QuantumType)(Quantum.Max * (min + x));
          Color.B = (QuantumType)(Quantum.Max * min);
          break;
        case 1:
          Color.R = (QuantumType)(Quantum.Max * (min + x));
          Color.G = (QuantumType)(Quantum.Max * (min + c));
          Color.B = (QuantumType)(Quantum.Max * min);
          break;
        case 2:
          Color.R = (QuantumType)(Quantum.Max * min);
          Color.G = (QuantumType)(Quantum.Max * (min + c));
          Color.B = (QuantumType)(Quantum.Max * (min + x));
          break;
        case 3:
          Color.R = (QuantumType)(Quantum.Max * min);
          Color.G = (QuantumType)(Quantum.Max * (min + x));
          Color.B = (QuantumType)(Quantum.Max * (min + c));
          break;
        case 4:
          Color.R = (QuantumType)(Quantum.Max * (min + x));
          Color.G = (QuantumType)(Quantum.Max * min);
          Color.B = (QuantumType)(Quantum.Max * (min + c));
          break;
        case 5:
          Color.R = (QuantumType)(Quantum.Max * (min + c));
          Color.G = (QuantumType)(Quantum.Max * min);
          Color.B = (QuantumType)(Quantum.Max * (min + x));
          break;
        default:
          Color.R = 0;
          Color.G = 0;
          Color.B = 0;
          break;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorHSL"/> class.
    /// </summary>
    /// <param name="hue">Hue component value of this color.</param>
    /// <param name="saturation">Saturation component value of this color.</param>
    /// <param name="lightness">Lightness component value of this color.</param>
    public ColorHSL(double hue, double saturation, double lightness)
          : base(new MagickColor(0, 0, 0))
    {
      Hue = hue;
      Saturation = saturation;
      Lightness = lightness;
    }

    /// <summary>
    /// Gets or sets the hue component value of this color.
    /// </summary>
    public double Hue
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the lightness component value of this color.
    /// </summary>
    public double Lightness
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the saturation component value of this color.
    /// </summary>
    public double Saturation
    {
      get;
      set;
    }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSL"/> instance.</returns>
    public static implicit operator ColorHSL(MagickColor color)
    {
      return FromMagickColor(color);
    }

    /// <summary>
    /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>A <see cref="ColorHSL"/> instance.</returns>
    public static ColorHSL FromMagickColor(MagickColor color)
    {
      if (color == null)
        return null;

      return new ColorHSL(color);
    }
  }
}
