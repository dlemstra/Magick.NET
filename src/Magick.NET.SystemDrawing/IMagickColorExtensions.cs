// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickColor{QuantumType}"/> interface.
    /// </summary>
    public static class IMagickColorExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Color"/> to a <see cref="IMagickColor{QuantumType}"/> instance.
        /// </summary>
        /// <param name="self">The color.</param>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        public static void SetFromColor<TQuantumType>(this IMagickColor<TQuantumType> self, Color color)
            where TQuantumType : struct
            => self?.SetFromBytes(color.R, color.G, color.B, color.A);

        /// <summary>
        /// Converts the value of this instance to an equivalent Color.
        /// </summary>
        /// <param name="self">The color.</param>
        /// <returns>A <see cref="Color"/> instance.</returns>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        public static Color ToColor<TQuantumType>(this IMagickColor<TQuantumType> self)
            where TQuantumType : struct
        {
            if (self == null)
                return default;

            var bytes = self.ToByteArray();

            if (!self.IsCmyk)
                return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);

            byte r = CmykToRgb(bytes, 0);
            byte g = CmykToRgb(bytes, 1);
            byte b = CmykToRgb(bytes, 2);

            return Color.FromArgb(bytes[4], r, g, b);
        }

        private static byte CmykToRgb(byte[] bytes, int index)
            => (byte)(255 - (((1.0 / 255) * bytes[index] * (255 - bytes[3])) + bytes[3]));
    }
}