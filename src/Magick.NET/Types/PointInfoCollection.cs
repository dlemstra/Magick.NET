// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

internal sealed partial class PointInfoCollection : INativeInstance
{
    public PointInfoCollection(IReadOnlyList<PointD> coordinates)
      : this(coordinates.Count)
    {
        for (var i = 0; i < coordinates.Count; i++)
        {
            var point = coordinates[i];
            _nativeInstance.Set(i, point.X, point.Y);
        }
    }

    public PointInfoCollection(IntPtr instance, int count)
    {
        _nativeInstance = new NativePointInfoCollection(instance);
        Count = count;
    }

    private PointInfoCollection(int count)
    {
        _nativeInstance = new NativePointInfoCollection(count);
        Count = count;
    }

    public int Count { get; private set; }

    IntPtr INativeInstance.Instance
        => _nativeInstance.Instance;

    public static void DisposeList(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
        {
            return;
        }

        var nativeInstance = new NativePointInfoCollection(instance);
        nativeInstance.Dispose();
    }

    public void Dispose()
        => _nativeInstance.Dispose();

    public double GetX(int index)
        => _nativeInstance.GetX(index);

    public double GetY(int index)
        => _nativeInstance.GetY(index);

    internal static IntPtr GetInstance(PointInfoCollection pointInfoCollection)
        => pointInfoCollection._nativeInstance.Instance;
}
