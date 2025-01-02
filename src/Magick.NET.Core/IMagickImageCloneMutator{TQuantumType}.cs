// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface that can be used to efficiently clone and mutate an image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickImageCloneMutator<TQuantumType> : IMagickImageCreateOperations<TQuantumType>
    where TQuantumType : struct, IConvertible
{
}
