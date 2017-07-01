// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;

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
    /// Class that represents a color.
    /// </summary>
    public sealed partial class MagickColor : IEquatable<MagickColor>, IComparable<MagickColor>
    {
        private bool _IsCmyk = false;

        private MagickColor(NativeMagickColor instance)
        {
            Initialize(instance);
        }

        private NativeMagickColor CreateNativeInstance()
        {
            NativeMagickColor instance = new NativeMagickColor()
            {
                Red = R,
                Green = G,
                Blue = B,
                Alpha = A,
                Black = K,
            };
            return instance;
        }

        private void Initialize(NativeMagickColor instance)
        {
            R = instance.Red;
            G = instance.Green;
            B = instance.Blue;
            A = instance.Alpha;
            K = instance.Black;

            _IsCmyk = instance.IsCMYK;
            Count = (int)instance.Count;
        }

#if !Q8
        private void Initialize(byte red, byte green, byte blue, byte alpha)
        {
            R = Quantum.Convert(red);
            G = Quantum.Convert(green);
            B = Quantum.Convert(blue);
            A = Quantum.Convert(alpha);
            K = 0;
        }
#endif

        private void Initialize(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
            K = 0;
        }

        private void ParseHexColor(string color)
        {
            List<QuantumType> colors = HexColor.Parse(color);

            if (colors.Count == 1)
                Initialize(colors[0], colors[0], colors[0], Quantum.Max);
            else if (colors.Count == 3)
                Initialize(colors[0], colors[1], colors[2], Quantum.Max);
            else
                Initialize(colors[0], colors[1], colors[2], colors[3]);
        }

        internal int Count
        {
            get;
            private set;
        }

        internal static MagickColor Clone(MagickColor value)
        {
            if (value == null)
                return value;

            MagickColor clone = new MagickColor()
            {
                R = value.R,
                G = value.G,
                B = value.B,
                A = value.A,
                K = value.K,
                _IsCmyk = value._IsCmyk,
            };
            return clone;
        }

        internal string ToShortString()
        {
            if (A != Quantum.Max)
                return ToString();

            if (_IsCmyk)
                return string.Format(CultureInfo.InvariantCulture, "cmyk({0},{1},{2},{3})", R, G, B, K);
#if Q8
            return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", R, G, B);
#elif Q16 || Q16HDRI
            return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}", (ushort)R, (ushort)G, (ushort)B);
#else
#error Not implemented!
#endif
        }

        internal static string ToString(MagickColor value)
        {
            return value == null ? null : value.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        public MagickColor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="color">The color to use.</param>
        public MagickColor(MagickColor color)
        {
            Throw.IfNull(nameof(color), color);

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
            K = color.K;
            _IsCmyk = color._IsCmyk;
        }

#if Q8
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="red">Red component value of this color (0-255).</param>
        /// <param name="green">Green component value of this color (0-255).</param>
        /// <param name="blue">Blue component value of this color (0-255).</param>
#elif Q16 || Q16HDRI
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="red">Red component value of this color (0-65535).</param>
        /// <param name="green">Green component value of this color (0-65535).</param>
        /// <param name="blue">Blue component value of this color (0-65535).</param>
#endif
        public MagickColor(QuantumType red, QuantumType green, QuantumType blue)
        {
            Initialize(red, green, blue, Quantum.Max);
        }

#if Q8
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="red">Red component value of this color (0-255).</param>
        /// <param name="green">Green component value of this color (0-255).</param>
        /// <param name="blue">Blue component value of this color (0-255).</param>
        /// <param name="alpha">Alpha component value of this color (0-255).</param>
#elif Q16 || Q16HDRI
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="red">Red component value of this color (0-65535).</param>
        /// <param name="green">Green component value of this color (0-65535).</param>
        /// <param name="blue">Blue component value of this color (0-65535).</param>
        /// <param name="alpha">Alpha component value of this color (0-65535).</param>
#endif
        public MagickColor(QuantumType red, QuantumType green, QuantumType blue, QuantumType alpha)
        {
            Initialize(red, green, blue, alpha);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="cyan">Cyan component value of this color.</param>
        /// <param name="magenta">Magenta component value of this color.</param>
        /// <param name="yellow">Yellow component value of this color.</param>
        /// <param name="black">Black component value of this color.</param>
        /// <param name="alpha">Alpha component value of this color.</param>
        public MagickColor(QuantumType cyan, QuantumType magenta, QuantumType yellow, QuantumType black, QuantumType alpha)
        {
            Initialize(cyan, magenta, yellow, alpha);
            K = black;
            _IsCmyk = true;
        }

#if Q8
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="color">The RGBA/CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
        /// For example: #F000, #FF000000</param>
#elif Q16 || Q16HDRI
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="color">The RGBA/CMYK hex string or name of the color (http://www.imagemagick.org/script/color.php).
        /// For example: #F000, #FF000000, #FFFF000000000000</param>
#else
#error Not implemented!
#endif
        public MagickColor(string color)
        {
            Throw.IfNullOrEmpty(nameof(color), color);

            if (color.Equals("transparent", StringComparison.OrdinalIgnoreCase))
            {
                Initialize(Quantum.Max, Quantum.Max, Quantum.Max, 0);
                return;
            }

            if (color[0] == '#')
            {
                ParseHexColor(color);
                return;
            }

            using (NativeMagickColor instance = new NativeMagickColor())
            {
                Throw.IfFalse(nameof(color), instance.Initialize(color), "Invalid color specified");
                Initialize(instance);
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="MagickColor"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator ==(MagickColor left, MagickColor right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="MagickColor"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator !=(MagickColor left, MagickColor right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickColor"/> is more than the second <see cref="MagickColor"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator >(MagickColor left, MagickColor right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.CompareTo(right) == 1;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickColor"/> is less than the second <see cref="MagickColor"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator <(MagickColor left, MagickColor right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return left.CompareTo(right) == -1;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickColor"/> is more than or equal to the second <see cref="MagickColor"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator >=(MagickColor left, MagickColor right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="MagickColor"/> is less than or equal to the second <see cref="MagickColor"/>.
        /// </summary>
        /// <param name="left">The first <see cref="MagickColor"/> to compare.</param>
        /// <param name="right"> The second <see cref="MagickColor"/> to compare.</param>
        public static bool operator <=(MagickColor left, MagickColor right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Gets or sets the alpha component value of this color.
        /// </summary>
        public QuantumType A
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the blue component value of this color.
        /// </summary>
        public QuantumType B
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the green component value of this color.
        /// </summary>
        public QuantumType G
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the key (black) component value of this color.
        /// </summary>
        public QuantumType K
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the red component value of this color.
        /// </summary>
        public QuantumType R
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a clone of the current color.
        /// </summary>
        /// <returns>A clone of the current color.</returns>
        public MagickColor Clone()
        {
            return new MagickColor(this);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="other">The color to compare this color with.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(MagickColor other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            if (R < other.R)
                return -1;

            if (R > other.R)
                return 1;

            if (G < other.G)
                return -1;

            if (G > other.G)
                return 1;

            if (B < other.B)
                return -1;

            if (B > other.B)
                return 1;

            if (K < other.K)
                return -1;

            if (K > other.K)
                return 1;

            if (A < other.A)
                return -1;

            if (A > other.A)
                return 1;

            return 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current color.
        /// </summary>
        /// <param name="obj">The object to compare this color with.</param>
        /// <returns>True when the specified object is equal to the current color.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as MagickColor);
        }

        /// <summary>
        /// Determines whether the specified color is equal to the current color.
        /// </summary>
        /// <param name="other">The color to compare this color with.</param>
        /// <returns>True when the specified color is equal to the current color.</returns>
        public bool Equals(MagickColor other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              _IsCmyk == other._IsCmyk &&
              A == other.A &&
              B == other.B &&
              G == other.G &&
              R == other.R &&
              K == other.K;
        }

        /// <summary>
        /// Creates a new <see cref="MagickColor"/> instance from the specified 8-bit color values (red, green,
        /// and blue). The alpha value is implicitly 255 (fully opaque).
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        /// <returns>A <see cref="MagickColor"/> instance.</returns>
        public static MagickColor FromRgb(byte red, byte green, byte blue)
        {
            MagickColor color = new MagickColor();
            color.Initialize(red, green, blue, 255);
            return color;
        }

        /// <summary>
        /// Creates a new <see cref="MagickColor"/> instance from the specified 8-bit color values (red, green,
        /// blue and alpha).
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        /// <param name="alpha">Alpha component value of this color.</param>
        /// <returns>A <see cref="MagickColor"/> instance.</returns>
        public static MagickColor FromRgba(byte red, byte green, byte blue, byte alpha)
        {
            MagickColor color = new MagickColor();
            color.Initialize(red, green, blue, alpha);
            return color;
        }

        /// <summary>
        /// Determines whether the specified color is fuzzy equal to the current color.
        /// </summary>
        /// <param name="other">The color to compare this color with.</param>
        /// <param name="fuzz">The fuzz factor.</param>
        /// <returns>True when the specified color is fuzzy equal to the current instance.</returns>
        public bool FuzzyEquals(MagickColor other, Percentage fuzz)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            using (NativeMagickColor instance = CreateNativeInstance())
            {
                return instance.FuzzyEquals(other, fuzz.ToQuantumType());
            }
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              _IsCmyk.GetHashCode() ^
              A.GetHashCode() ^
              B.GetHashCode() ^
              G.GetHashCode() ^
              K.GetHashCode() ^
              R.GetHashCode();
        }

        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (_IsCmyk)
                return string.Format(CultureInfo.InvariantCulture, "cmyka({0},{1},{2},{3},{4:0.0###})", R, G, B, K, (double)A / Quantum.Max);
#if Q8
            return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", R, G, B, A);
#elif Q16 || Q16HDRI
            return string.Format(CultureInfo.InvariantCulture, "#{0:X4}{1:X4}{2:X4}{3:X4}", (ushort)R, (ushort)G, (ushort)B, (ushort)A);
#else
#error Not implemented!
#endif
        }
    }
}
