//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
  /// Base class for colors
  /// </summary>
  public abstract class ColorBase : IEquatable<ColorBase>, IComparable<ColorBase>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBase"/> class.
    /// </summary>
    /// <param name="color">The color to use.</param>
    protected ColorBase(MagickColor color)
    {
      Color = color;
    }

    /// <summary>
    /// Gets the actual color of this instance.
    /// </summary>
    protected MagickColor Color
    {
      get;
      private set;
    }

    /// <summary>
    /// Updates the color value from an inherited class.
    /// </summary>
    protected virtual void UpdateColor()
    {
    }

    /// <summary>
    /// Determines whether the specified <see cref="ColorBase"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator ==(ColorBase left, ColorBase right)
    {
      return Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified <see cref="ColorBase"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator !=(ColorBase left, ColorBase right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is more than the second <see cref="ColorBase"/>
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator >(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is less than the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator <(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is more than or equal to the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator >=(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return ReferenceEquals(right, null);

      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first <see cref="ColorBase"/> is less than or equal to the second <see cref="ColorBase"/>.
    /// </summary>
    /// <param name="left">The first <see cref="ColorBase"/> to compare.</param>
    /// <param name="right"> The second <see cref="ColorBase"/> to compare.</param>
    public static bool operator <=(ColorBase left, ColorBase right)
    {
      if (ReferenceEquals(left, null))
        return !ReferenceEquals(right, null);

      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Converts the specified color to a <see cref="MagickColor"/> instance.
    /// </summary>
    /// <param name="color">The color to use.</param>
    public static implicit operator MagickColor(ColorBase color)
    {
      if (color == null)
        return null;

      return color.ToMagickColor();
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this color with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(ColorBase other)
    {
      if (ReferenceEquals(other, null))
        return 1;

      UpdateColor();
      other.UpdateColor();

      return Color.CompareTo(other.Color);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare this color with.</param>
    /// <returns>True when the specified object is equal to the current instance.</returns>
    public override bool Equals(object obj)
    {
      return Equals(obj as ColorBase);
    }

    /// <summary>
    /// Determines whether the specified color is equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <returns>True when the specified color is equal to the current instance.</returns>
    public bool Equals(ColorBase other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      UpdateColor();
      other.UpdateColor();

      return Color.Equals(other.Color);
    }

    /// <summary>
    /// Determines whether the specified color is fuzzy equal to the current color.
    /// </summary>
    /// <param name="other">The color to compare this color with.</param>
    /// <param name="fuzz">The fuzz factor.</param>
    /// <returns>True when the specified color is fuzzy equal to the current instance.</returns>
    public bool FuzzyEquals(ColorBase other, Percentage fuzz)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      UpdateColor();
      other.UpdateColor();

      return Color.FuzzyEquals(other.Color, fuzz);
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
      UpdateColor();

      return Color.GetHashCode();
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent <see cref="MagickColor"/>.
    /// </summary>
    /// <returns>A <see cref="MagickColor"/> instance.</returns>
    public MagickColor ToMagickColor()
    {
      UpdateColor();

      return new MagickColor(Color);
    }

    /// <summary>
    /// Converts the value of this instance to a hexadecimal string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
    {
      return ToMagickColor().ToString();
    }
  }
}