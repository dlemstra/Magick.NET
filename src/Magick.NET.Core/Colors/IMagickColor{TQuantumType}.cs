// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Interface that represents a color.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickColor<TQuantumType> : IEquatable<IMagickColor<TQuantumType>>, IComparable<IMagickColor<TQuantumType>>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the alpha component value of this color.
        /// </summary>
        TQuantumType A { get; set; }

        /// <summary>
        /// Gets or sets the blue component value of this color.
        /// </summary>
        TQuantumType B { get; set; }

        /// <summary>
        /// Gets or sets the green component value of this color.
        /// </summary>
        TQuantumType G { get; set; }

        /// <summary>
        /// Gets a value indicating whether the color is a CMYK color.
        /// </summary>
        bool IsCmyk { get; }

        /// <summary>
        /// Gets or sets the key (black) component value of this color.
        /// </summary>
        TQuantumType K { get; set; }

        /// <summary>
        /// Gets or sets the red component value of this color.
        /// </summary>
        TQuantumType R { get; set; }

        /// <summary>
        /// Determines whether the specified color is fuzzy equal to the current color.
        /// </summary>
        /// <param name="other">The color to compare this color with.</param>
        /// <param name="fuzz">The fuzz factor.</param>
        /// <returns>True when the specified color is fuzzy equal to the current instance.</returns>
        bool FuzzyEquals(IMagickColor<TQuantumType> other, Percentage fuzz);

        /// <summary>
        /// Initializes the color with the specified bytes.
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        /// <param name="alpha">Alpha component value of this color.</param>
        void SetFromBytes(byte red, byte green, byte blue, byte alpha);

        /// <summary>
        /// Converts the value of this instance to a <see cref="byte"/> array (RGBA or CMYKA).
        /// </summary>
        /// <returns>The <see cref="byte"/> array.</returns>
        byte[] ToByteArray();

        /// <summary>
        /// Converts the value of this instance to a hexadecimal string that will not include the alpha channel if it is opaque.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string ToHexString();

        /// <summary>
        /// Converts the value of this instance to a string representation that will not include the alpha channel if it is opaque.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string ToShortString();

        /// <summary>
        /// Converts the value of this instance to a string representation.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string ToString();
    }
}