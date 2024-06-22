// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick connected component object.
/// </summary>
public sealed partial class ConnectedComponent : IConnectedComponent<QuantumType>
{
    private ConnectedComponent(IntPtr instance)
    {
        Area = (int)NativeConnectedComponent.GetArea(instance);
        Centroid = NativeConnectedComponent.GetCentroid(instance).ToPointD();
        Color = NativeConnectedComponent.GetColor(instance);
        Height = (int)NativeConnectedComponent.GetHeight(instance);
        Id = (int)NativeConnectedComponent.GetId(instance);
        Width = (int)NativeConnectedComponent.GetWidth(instance);
        X = (int)NativeConnectedComponent.GetX(instance);
        Y = (int)NativeConnectedComponent.GetY(instance);
    }

    /// <summary>
    /// Gets the pixel count of the area.
    /// </summary>
    public int Area { get; }

    /// <summary>
    /// Gets the centroid of the area.
    /// </summary>
    public PointD Centroid { get; }

    /// <summary>
    /// Gets the color of the area.
    /// </summary>
    public IMagickColor<QuantumType>? Color { get; }

    /// <summary>
    /// Gets the height of the area.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the id of the area.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the width of the area.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the X offset from origin.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y offset from origin.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    /// <returns>The geometry of the area of this connected component.</returns>
    public IMagickGeometry ToGeometry()
        => new MagickGeometry(X, Y, Width, Height);

    /// <summary>
    /// Returns the geometry of the area of this connected component.
    /// </summary>
    /// <param name="extent">The number of pixels to extent the image with.</param>
    /// <returns>The geometry of the area of this connected component.</returns>
    public IMagickGeometry ToGeometry(int extent)
    {
        int extra;
        checked
        {
            extra = extent * 2;
        }

        return new MagickGeometry(X - extent, Y - extent, Width + extra, Height + extra);
    }

    internal static IReadOnlyCollection<IConnectedComponent<QuantumType>> Create(IntPtr list, int length)
    {
        var result = new Collection<IConnectedComponent<QuantumType>>();

        if (list == IntPtr.Zero)
            return result;

        for (var i = 0U; i < length; i++)
        {
            var instance = NativeConnectedComponent.GetInstance(list, i);
            if (instance == IntPtr.Zero)
                continue;

            if (NativeConnectedComponent.GetArea(instance) < double.Epsilon)
                continue;

            result.Add(new ConnectedComponent(instance));
        }

        return result;
    }

    internal static void DisposeList(IntPtr list)
    {
        if (list != IntPtr.Zero)
            NativeConnectedComponent.DisposeList(list);
    }
}
