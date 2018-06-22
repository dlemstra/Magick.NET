﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Collections.Generic;

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
    /// Interface that can be used to access the individual pixels of an image.
    /// </summary>
    public interface IPixelCollection : IEnumerable<Pixel>, IDisposable
    {
        /// <summary>
        /// Gets the number of channels that the image contains.
        /// </summary>
        int Channels { get; }

        /// <summary>
        /// Gets the pixel at the specified coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        Pixel this[int x, int y] { get; }

        /// <summary>
        /// Returns the pixel at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        QuantumType[] GetArea(int x, int y, int width, int height);

        /// <summary>
        /// Returns the pixel of the specified area
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        QuantumType[] GetArea(MagickGeometry geometry);

        /// <summary>
        /// Returns the index of the specified channel. Returns -1 if not found.
        /// </summary>
        /// <param name="channel">The channel to get the index of.</param>
        /// <returns>The index of the specified channel. Returns -1 if not found.</returns>
        int GetIndex(PixelChannel channel);

        /// <summary>
        /// Returns the <see cref="Pixel"/> at the specified coordinate.
        /// </summary>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <returns>The <see cref="Pixel"/> at the specified coordinate.</returns>
        Pixel GetPixel(int x, int y);

        /// <summary>
        /// Returns the value of the specified coordinate.
        /// </summary>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        QuantumType[] GetValue(int x, int y);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        QuantumType[] GetValues();

        /// <summary>
        /// Changes the value of the specified pixel.
        /// </summary>
        /// <param name="pixel">The pixel to set.</param>
        void SetPixel(Pixel pixel);

        /// <summary>
        /// Changes the value of the specified pixels.
        /// </summary>
        /// <param name="pixels">The pixels to set.</param>
        void SetPixel(IEnumerable<Pixel> pixels);

        /// <summary>
        /// Changes the value of the specified pixel.
        /// </summary>
        /// <param name="x">The X coordinate of the pixel.</param>
        /// <param name="y">The Y coordinate of the pixel.</param>
        /// <param name="value">The value of the pixel.</param>
        void SetPixel(int x, int y, QuantumType[] value);

#if !Q8
        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="values">The values of the pixels.</param>
        void SetPixels(byte[] values);
#endif

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="values">The values of the pixels.</param>
        void SetPixels(double[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="values">The values of the pixels.</param>
        void SetPixels(int[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="values">The values of the pixels.</param>
        void SetPixels(QuantumType[] values);

#if !Q8
        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(int x, int y, int width, int height, byte[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(MagickGeometry geometry, byte[] values);
#endif

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(int x, int y, int width, int height, double[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(MagickGeometry geometry, double[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(int x, int y, int width, int height, int[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(MagickGeometry geometry, int[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(int x, int y, int width, int height, QuantumType[] values);

        /// <summary>
        /// Changes the values of the specified pixels.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="values">The values of the pixels.</param>
        void SetArea(MagickGeometry geometry, QuantumType[] values);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <returns>A <see cref="QuantumType"/> array.</returns>
        QuantumType[] ToArray();

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(int x, int y, int width, int height, string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(int x, int y, int width, int height, PixelMapping mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(MagickGeometry geometry, string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(MagickGeometry geometry, PixelMapping mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray(PixelMapping mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(int x, int y, int width, int height, string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="x">The X coordinate of the area.</param>
        /// <param name="y">The Y coordinate of the area.</param>
        /// <param name="width">The width of the area.</param>
        /// <param name="height">The height of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(int x, int y, int width, int height, PixelMapping mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(MagickGeometry geometry, string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="geometry">The geometry of the area.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(MagickGeometry geometry, PixelMapping mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(string mapping);

        /// <summary>
        /// Returns the values of the pixels as an array.
        /// </summary>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        /// <returns>An <see cref="ushort"/> array.</returns>
        ushort[] ToShortArray(PixelMapping mapping);
    }
}
