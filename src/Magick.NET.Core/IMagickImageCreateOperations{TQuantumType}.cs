// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface that represents ImageMagick operations that create a new image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickImageCreateOperations<TQuantumType> : IMagickImageCreateOperations
    where TQuantumType : struct, IConvertible
{
}
