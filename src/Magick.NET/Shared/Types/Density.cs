// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Represents the density of an image.
    /// </summary>
    public sealed class Density : IEquatable<Density>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class with the density set to inches.
        /// </summary>
        /// <param name="xy">The x and y.</param>
        public Density(double xy)
          : this(xy, xy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class.
        /// </summary>
        /// <param name="xy">The x and y.</param>
        /// <param name="units">The units.</param>
        public Density(double xy, DensityUnit units)
          : this(xy, xy, units)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class with the density set to inches.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Density(double x, double y)
          : this(x, y, DensityUnit.PixelsPerInch)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="units">The units.</param>
        public Density(double x, double y, DensityUnit units)
        {
            X = x;
            Y = y;
            Units = units;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class.
        /// </summary>
        /// <param name="value">Density specifications in the form: &lt;x&gt;x&lt;y&gt;[inch/cm] (where x, y are numbers).</param>
        public Density(string value)
        {
            Initialize(value);
        }

        /// <summary>
        /// Gets the units.
        /// </summary>
        public DensityUnit Units { get; private set; }

        /// <summary>
        /// Gets the x resolution.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y resolution.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="Density"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Density"/> to compare.</param>
        /// <param name="right"> The second <see cref="Density"/> to compare.</param>
        public static bool operator ==(Density left, Density right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="Density"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="Density"/> to compare.</param>
        /// <param name="right"> The second <see cref="Density"/> to compare.</param>
        public static bool operator !=(Density left, Density right) => !Equals(left, right);

        /// <summary>
        /// Changes the density of the instance to the specified units.
        /// </summary>
        /// <param name="units">The units to use.</param>
        /// <returns>A new <see cref="Density"/> with the specified units.</returns>
        public Density ChangeUnits(DensityUnit units)
        {
            if (Units == units || Units == DensityUnit.Undefined || units == DensityUnit.Undefined)
                return new Density(X, Y, units);
            else if (Units == DensityUnit.PixelsPerCentimeter && units == DensityUnit.PixelsPerInch)
                return new Density(X * 2.54, Y * 2.54, units);
            else
                return new Density(X / 2.54, Y / 2.54, units);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the <see cref="Density"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="Density"/> with.</param>
        /// <returns>True when the specified object is equal to the <see cref="Density"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Density))
                return false;

            return Equals((Density)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Density"/> is equal to the current <see cref="Density"/>.
        /// </summary>
        /// <param name="other">The <see cref="Density"/> to compare this <see cref="Density"/> with.</param>
        /// <returns>True when the specified <see cref="Density"/> is equal to the current <see cref="Density"/>.</returns>
        public bool Equals(Density other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
              X == other.X &&
              Y == other.Y &&
              Units == other.Units;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              X.GetHashCode() ^
              Y.GetHashCode() ^
              Units.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="MagickGeometry"/> based on the specified width and height.
        /// </summary>
        /// <param name="width">The width in cm or inches.</param>
        /// <param name="height">The height in cm or inches.</param>
        /// <returns>A <see cref="MagickGeometry"/> based on the specified width and height in cm or inches.</returns>
        public MagickGeometry ToGeometry(double width, double height)
        {
            int pixelWidth = (int)(width * X);
            int pixelHeight = (int)(height * Y);

            return new MagickGeometry(pixelWidth, pixelHeight);
        }

        /// <summary>
        /// Returns a string that represents the current <see cref="Density"/>.
        /// </summary>
        /// <returns>A string that represents the current <see cref="Density"/>.</returns>
        public override string ToString() => ToString(Units);

        /// <summary>
        /// Returns a string that represents the current <see cref="Density"/>.
        /// </summary>
        /// <param name="units">The units to use.</param>
        /// <returns>A string that represents the current <see cref="Density"/>.</returns>
        public string ToString(DensityUnit units)
        {
            if (Units == units || units == DensityUnit.Undefined)
                return ToString(X, Y, units);
            else if (Units == DensityUnit.PixelsPerCentimeter && units == DensityUnit.PixelsPerInch)
                return ToString(X * 2.54, Y * 2.54, units);
            else
                return ToString(X / 2.54, Y / 2.54, units);
        }

        internal static Density Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new Density(value);
        }

        internal static Density Clone(Density value)
        {
            if (value == null)
                return null;

            return new Density(value.X, value.Y, value.Units);
        }

        private static string ToString(double x, double y, DensityUnit units)
        {
            string result = string.Format(CultureInfo.InvariantCulture, "{0}x{1}", x, y);

            switch (units)
            {
                case DensityUnit.PixelsPerCentimeter:
                    return result + " cm";
                case DensityUnit.PixelsPerInch:
                    return result + " inch";
                default:
                    return result;
            }
        }

        private void Initialize(string value)
        {
            Throw.IfNullOrEmpty(nameof(value), value);

            string[] values = value.Split(' ');
            Throw.IfTrue(nameof(value), values.Length > 2, "Invalid density specified.");

            if (values.Length == 2)
            {
                if (values[1].Equals("cm", StringComparison.OrdinalIgnoreCase))
                    Units = DensityUnit.PixelsPerCentimeter;
                else if (values[1].Equals("inch", StringComparison.OrdinalIgnoreCase))
                    Units = DensityUnit.PixelsPerInch;
                else
                    throw new ArgumentException("Invalid density specified.", nameof(value));
            }

            string[] xyValues = values[0].Split('x');
            Throw.IfTrue(nameof(value), xyValues.Length > 2, "Invalid density specified.");

            Throw.IfFalse(nameof(value), double.TryParse(xyValues[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double x), "Invalid density specified.");

            double y;
            if (xyValues.Length == 1)
                y = x;
            else
                Throw.IfFalse(nameof(value), double.TryParse(xyValues[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y), "Invalid density specified.");

            X = x;
            Y = y;
        }
    }
}
