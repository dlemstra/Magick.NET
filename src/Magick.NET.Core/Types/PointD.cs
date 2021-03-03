// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Struct for a point with doubles.
    /// </summary>
    public struct PointD : IEquatable<PointD>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="xy">The x and y.</param>
        public PointD(double xy)
        {
            X = xy;
            Y = xy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointD"/> struct.
        /// </summary>
        /// <param name="value">PointD specifications in the form: &lt;x&gt;x&lt;y&gt; (where x, y are numbers).</param>
        public PointD(string value)
          : this()
        {
            Throw.IfNullOrEmpty(nameof(value), value);

            Initialize(value);
        }

        /// <summary>
        /// Gets the x-coordinate of this <see cref="PointD"/>.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of this <see cref="PointD"/>.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="PointD"/> to compare.</param>
        /// <param name="right"> The second <see cref="PointD"/> to compare.</param>
        public static bool operator ==(PointD left, PointD right)
            => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="PointD"/> to compare.</param>
        /// <param name="right"> The second <see cref="PointD"/> to compare.</param>
        public static bool operator !=(PointD left, PointD right)
            => !Equals(left, right);

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="PointD"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="PointD"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="PointD"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(PointD))
                return false;

            return Equals((PointD)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="PointD"/> is equal to the current <see cref="PointD"/>.
        /// </summary>
        /// <param name="other">The <see cref="PointD"/> to compare this <see cref="PointD"/> with.</param>
        /// <returns>True when the specified <see cref="PointD"/> is equal to the current <see cref="PointD"/>.</returns>
        public bool Equals(PointD other)
            => X == other.X && Y == other.Y;

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
            => X.GetHashCode() ^ Y.GetHashCode();

        /// <summary>
        /// Returns a string that represents the current PointD.
        /// </summary>
        /// <returns>A string that represents the current PointD.</returns>
        public override string ToString()
            => string.Format(CultureInfo.InvariantCulture, "{0}x{1}", X, Y);

        private void Initialize(string value)
        {
            var values = value.Split('x');
            Throw.IfTrue(nameof(value), values.Length > 2, "Invalid point specified.");

            Throw.IfFalse(nameof(value), double.TryParse(values[0], NumberStyles.Number, CultureInfo.InvariantCulture, out double x), "Invalid point specified.");

            double y;
            if (values.Length == 2)
                Throw.IfFalse(nameof(value), double.TryParse(values[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y), "Invalid point specified.");
            else
                y = x;

            X = x;
            Y = y;
        }
    }
}