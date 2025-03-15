// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.CompilerServices;

namespace ImageMagick;

/// <summary>
/// Factory for creating quantum scalers.
/// </summary>
public static class QuantumScalerFactory
{
    /// <summary>
    /// Creates a quantum scaler for the specified quantum type.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    /// <returns>A new <see cref="IQuantumScaler{TQuantumType}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IQuantumScaler<TQuantumType> Create<TQuantumType>()
        where TQuantumType : struct, IConvertible
    {
        if (typeof(TQuantumType) == typeof(byte))
            return (IQuantumScaler<TQuantumType>)(object)new ByteQuantumScaler();
        else if (typeof(TQuantumType) == typeof(ushort))
            return (IQuantumScaler<TQuantumType>)(object)new UnsignedShortQuantumScaler();
        else if (typeof(TQuantumType) == typeof(float))
            return (IQuantumScaler<TQuantumType>)(object)new FloatQuantumScaler();

        throw new NotSupportedException($"Type {typeof(TQuantumType).Name} is not supported for quantum scaling");
    }
}
