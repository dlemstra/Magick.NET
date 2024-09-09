// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;

namespace Magick.NET;

public sealed class TestImageProfile : IImageProfile
{
    private readonly byte[] _bytes;

    public TestImageProfile(string name, byte[] bytes)
    {
        Name = name;
        _bytes = bytes;
    }

    public string Name { get; }

    public bool Equals(IImageProfile? other)
        => false;

    public byte[] GetData()
        => _bytes;

    public byte[] ToByteArray()
        => _bytes;

#if NETCOREAPP
    public ReadOnlySpan<byte> ToReadOnlySpan()
        => _bytes.AsSpan();
#endif
}
