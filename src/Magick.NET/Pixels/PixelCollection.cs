// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

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

internal abstract partial class PixelCollection : IPixelCollection<QuantumType>
{
    private NativePixelCollection? _nativeInstance;

    protected PixelCollection(MagickImage image)
        => Image = image;

    public int Channels
        => Image.ChannelCount;

    protected MagickImage Image { get; }

    private NativePixelCollection NativeInstance
    {
        get
        {
            _nativeInstance ??= new NativePixelCollection(Image);

            return _nativeInstance;
        }
    }

    public IPixel<QuantumType>? this[int x, int y]
        => GetPixel(x, y);

    public void Dispose()
    {
        _nativeInstance?.Dispose();
    }

    public virtual QuantumType[]? GetArea(int x, int y, int width, int height)
        => GetAreaUnchecked(x, y, width, height);

    public virtual QuantumType[]? GetArea(IMagickGeometry geometry)
        => GetArea(geometry.X, geometry.Y, geometry.Width, geometry.Height);

    public virtual IntPtr GetAreaPointer(int x, int y, int width, int height)
        => NativeInstance.GetArea(x, y, width, height);

    public virtual IntPtr GetAreaPointer(IMagickGeometry geometry)
        => GetAreaPointer(geometry.X, geometry.Y, geometry.Width, geometry.Height);

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public IEnumerator<IPixel<QuantumType>> GetEnumerator()
        => new PixelCollectionEnumerator(this, Image.Width, Image.Height);

    public int GetIndex(PixelChannel channel)
        => Image.ChannelOffset(channel);

    public virtual IPixel<QuantumType> GetPixel(int x, int y)
        => Pixel.Create(this, x, y, GetAreaUnchecked(x, y, 1, 1)!);

    public virtual QuantumType[]? GetValue(int x, int y)
        => GetAreaUnchecked(x, y, 1, 1);

    public QuantumType[]? GetValues()
        => GetAreaUnchecked(0, 0, Image.Width, Image.Height);

    public virtual void SetArea(int x, int y, int width, int height, QuantumType[] values)
        => SetAreaUnchecked(x, y, width, height, values);

    public virtual void SetArea(IMagickGeometry geometry, QuantumType[] values)
        => SetArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetByteArea(int x, int y, int width, int height, byte[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(x, y, width, height, castedValues);
    }

    public virtual void SetByteArea(IMagickGeometry geometry, byte[] values)
        => SetByteArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetBytePixels(byte[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
    }

    public virtual void SetDoubleArea(int x, int y, int width, int height, double[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(x, y, width, height, castedValues);
    }

    public virtual void SetDoubleArea(IMagickGeometry geometry, double[] values)
        => SetDoubleArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetDoublePixels(double[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
    }

    public virtual void SetIntArea(int x, int y, int width, int height, int[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(x, y, width, height, castedValues);
    }

    public virtual void SetIntArea(IMagickGeometry geometry, int[] values)
        => SetIntArea(geometry.X, geometry.Y, geometry.Width, geometry.Height, values);

    public virtual void SetIntPixels(int[] values)
    {
        var castedValues = CastArray(values, Quantum.Convert);
        SetAreaUnchecked(0, 0, Image.Width, Image.Height, castedValues);
    }

    public virtual void SetPixel(int x, int y, QuantumType[] value)
        => SetPixelUnchecked(x, y, value);

    public virtual void SetPixel(IPixel<QuantumType> pixel)
    {
        if (pixel is not null)
            SetPixelUnchecked(pixel.X, pixel.Y, pixel.ToArray());
    }

    public virtual void SetPixel(IEnumerable<IPixel<QuantumType>> pixels)
    {
        var enumerator = pixels.GetEnumerator();

        while (enumerator.MoveNext())
        {
            SetPixel(enumerator.Current);
        }
    }

    public virtual void SetPixels(QuantumType[] values)
        => SetAreaUnchecked(0, 0, Image.Width, Image.Height, values);

    public QuantumType[]? ToArray()
        => GetValues();

    public virtual byte[]? ToByteArray(int x, int y, int width, int height, string mapping)
    {
        var nativeResult = IntPtr.Zero;
        byte[]? result = null;

        try
        {
            nativeResult = NativeInstance.ToByteArray(x, y, width, height, mapping);
            result = ByteConverter.ToArray(nativeResult, width * height * mapping.Length);
        }
        finally
        {
            MagickMemory.Relinquish(nativeResult);
        }

        return result;
    }

    public virtual byte[]? ToByteArray(int x, int y, int width, int height, PixelMapping mapping)
        => ToByteArray(x, y, width, height, mapping.ToString());

    public virtual byte[]? ToByteArray(IMagickGeometry geometry, string mapping)
        => ToByteArray(geometry.X, geometry.Y, geometry.Width, geometry.Height, mapping);

    public virtual byte[]? ToByteArray(IMagickGeometry geometry, PixelMapping mapping)
        => ToByteArray(geometry.X, geometry.Y, geometry.Width, geometry.Height, mapping.ToString());

    public byte[]? ToByteArray(string mapping)
        => ToByteArray(0, 0, Image.Width, Image.Height, mapping);

    public byte[]? ToByteArray(PixelMapping mapping)
        => ToByteArray(0, 0, Image.Width, Image.Height, mapping.ToString());

    public virtual ushort[]? ToShortArray(int x, int y, int width, int height, string mapping)
    {
        var nativeResult = IntPtr.Zero;

        try
        {
            nativeResult = NativeInstance.ToShortArray(x, y, width, height, mapping);
            return ShortConverter.ToArray(nativeResult, width * height * mapping.Length);
        }
        finally
        {
            MagickMemory.Relinquish(nativeResult);
        }
    }

    public virtual ushort[]? ToShortArray(int x, int y, int width, int height, PixelMapping mapping)
        => ToShortArray(x, y, width, height, mapping.ToString());

    public virtual ushort[]? ToShortArray(IMagickGeometry geometry, string mapping)
        => ToShortArray(geometry.X, geometry.Y, geometry.Width, geometry.Height, mapping);

    public virtual ushort[]? ToShortArray(IMagickGeometry geometry, PixelMapping mapping)
        => ToShortArray(geometry, mapping.ToString());

    public ushort[]? ToShortArray(string mapping)
        => ToShortArray(0, 0, Image.Width, Image.Height, mapping);

    public ushort[]? ToShortArray(PixelMapping mapping)
        => ToShortArray(0, 0, Image.Width, Image.Height, mapping.ToString());

    internal QuantumType[]? GetAreaUnchecked(int x, int y, int width, int height)
    {
        var pixels = NativeInstance.GetArea(x, y, width, height);
        if (pixels == IntPtr.Zero)
            throw new InvalidOperationException("Image contains no pixel data.");

        var length = width * height * Image.ChannelCount;
        return QuantumConverter.ToArray(pixels, length);
    }

    internal void SetPixelUnchecked(int x, int y, QuantumType[] value)
        => SetAreaUnchecked(x, y, 1, 1, value);

    private static QuantumType[] CastArray<T>(T[] values, Func<T, QuantumType> convertMethod)
    {
        var result = new QuantumType[values.Length];
        for (var i = 0; i < values.Length; i++)
            result[i] = convertMethod(values[i]);

        return result;
    }

    private void SetAreaUnchecked(int x, int y, int width, int height, QuantumType[] values)
        => NativeInstance.SetArea(x, y, width, height, values, values.Length);
}
