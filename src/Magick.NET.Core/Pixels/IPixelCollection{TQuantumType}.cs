// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that can be used to access the individual pixels of an image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IPixelCollection<TQuantumType> : IEnumerable<IPixel<TQuantumType>>, IDisposable
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets the number of channels that the image contains.
    /// </summary>
    uint Channels { get; }

    /// <summary>
    /// Gets the pixel at the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    IPixel<TQuantumType>? this[int x, int y] { get; }

    /// <summary>
    /// Returns the pixels at the specified area.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    TQuantumType[]? GetArea(int x, int y, uint width, uint height);

    /// <summary>
    /// Returns the pixels of the specified area.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    TQuantumType[]? GetArea(IMagickGeometry geometry);

    /// <summary>
    /// Returns the index of the specified channel. Returns null if not found.
    /// </summary>
    /// <param name="channel">The channel to get the index of.</param>
    /// <returns>The index of the specified channel. Returns null if not found.</returns>
    uint? GetChannelIndex(PixelChannel channel);

    /// <summary>
    /// Returns the <see cref="IPixel{TQuantumType}"/> at the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <returns>The <see cref="IPixel{TQuantumType}"/> at the specified coordinate.</returns>
    IPixel<TQuantumType> GetPixel(int x, int y);

    /// <summary>
    /// Returns the value of the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    TQuantumType[]? GetValue(int x, int y);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    TQuantumType[]? GetValues();

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetArea(int x, int y, uint width, uint height, TQuantumType[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetArea(IMagickGeometry geometry, TQuantumType[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetByteArea(int x, int y, uint width, uint height, byte[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetByteArea(IMagickGeometry geometry, byte[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="values">The values of the pixels.</param>
    void SetBytePixels(byte[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetDoubleArea(int x, int y, uint width, uint height, double[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetDoubleArea(IMagickGeometry geometry, double[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="values">The values of the pixels.</param>
    void SetDoublePixels(double[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetIntArea(int x, int y, uint width, uint height, int[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="values">The values of the pixels.</param>
    void SetIntArea(IMagickGeometry geometry, int[] values);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="values">The values of the pixels.</param>
    void SetIntPixels(int[] values);

    /// <summary>
    /// Changes the value of the specified pixel.
    /// </summary>
    /// <param name="pixel">The pixel to set.</param>
    void SetPixel(IPixel<TQuantumType> pixel);

    /// <summary>
    /// Changes the value of the specified pixels.
    /// </summary>
    /// <param name="pixels">The pixels to set.</param>
    void SetPixel(IEnumerable<IPixel<TQuantumType>> pixels);

    /// <summary>
    /// Changes the value of the specified pixel.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <param name="value">The value of the pixel.</param>
    void SetPixel(int x, int y, TQuantumType[] value);

    /// <summary>
    /// Changes the values of the specified pixels.
    /// </summary>
    /// <param name="values">The values of the pixels.</param>
    void SetPixels(TQuantumType[] values);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <returns>A <typeparamref name="TQuantumType"/> array.</returns>
    TQuantumType[]? ToArray();

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(int x, int y, uint width, uint height, string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(int x, int y, uint width, uint height, PixelMapping mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(IMagickGeometry geometry, string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(IMagickGeometry geometry, PixelMapping mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[]? ToByteArray(PixelMapping mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(int x, int y, uint width, uint height, string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="x">The X coordinate of the area.</param>
    /// <param name="y">The Y coordinate of the area.</param>
    /// <param name="width">The width of the area.</param>
    /// <param name="height">The height of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(int x, int y, uint width, uint height, PixelMapping mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(IMagickGeometry geometry, string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="geometry">The geometry of the area.</param>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(IMagickGeometry geometry, PixelMapping mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(string mapping);

    /// <summary>
    /// Returns the values of the pixels as an array.
    /// </summary>
    /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
    /// <returns>An <see cref="ushort"/> array.</returns>
    ushort[]? ToShortArray(PixelMapping mapping);
}
