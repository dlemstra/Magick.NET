// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface for classes that implement a quantum scaler.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IQuantumScaler<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Scales the specified value to a quantum type.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The value scaled to a quantum type.</returns>
    TQuantumType ScaleFromByte(byte value);

    /// <summary>
    /// Scales the specified value to a quantum type.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The value scaled to a quantum type.</returns>
    TQuantumType ScaleFromUnsignedShort(ushort value);

    /// <summary>
    /// Scales the specified value to a byte.
    /// </summary>
    /// <param name="value">The value to scale.</param>
    /// <returns>The value scaled to a byte.</returns>
    byte ScaleToByte(TQuantumType value);
}
