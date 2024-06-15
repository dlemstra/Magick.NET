// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
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

namespace ImageMagick;

internal sealed class PixelCollectionEnumerator : IEnumerator<IPixel<QuantumType>>
{
    private readonly PixelCollection _collection;
    private readonly uint _height;
    private readonly uint _width;

    private QuantumType[]? _row;
    private int _x;
    private int _y;

    public PixelCollectionEnumerator(PixelCollection collection, uint width, uint height)
    {
        _collection = collection;
        _width = width;
        _height = height;
        Reset();
    }

    object? IEnumerator.Current
        => Current;

    public IPixel<QuantumType> Current
    {
        get
        {
            if (_x == -1 || _row is null)
                throw new InvalidOperationException();

            var pixel = new QuantumType[_collection.Channels];
            Array.Copy(_row, _x * _collection.Channels, pixel, 0, _collection.Channels);

            return Pixel.Create(_collection, _x, _y, pixel);
        }
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        if (++_x == _width)
        {
            _x = 0;
            _y++;
            SetRow();
        }

        if (_y < _height)
            return true;

        _x = (int)(_width - 1U);
        _y = (int)(_height - 1U);
        return false;
    }

    public void Reset()
    {
        _x = -1;
        _y = 0;
        SetRow();
    }

    private void SetRow()
    {
        if (_y < _height)
            _row = _collection.GetAreaUnchecked(0, _y, _width, 1);
    }
}
