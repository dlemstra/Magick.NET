// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

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
/// Class that can be used to acquire information about the Quantum.
/// </summary>
public partial class Quantum : IQuantum<QuantumType>
{
    static Quantum()
    {
        Depth = (uint)NativeQuantum.Depth_Get();
        Max = NativeQuantum.Max_Get();
    }

    /// <summary>
    /// Gets the quantum depth.
    /// </summary>
    public static uint Depth { get; }

    /// <summary>
    /// Gets the maximum value of the quantum.
    /// </summary>
    public static QuantumType Max { get; }

    /// <summary>
    /// Gets the quantum depth.
    /// </summary>
    uint IQuantum.Depth
        => Depth;

    /// <summary>
    /// Gets the maximum value of the quantum.
    /// </summary>
    QuantumType IQuantum<QuantumType>.Max
        => Max;

    internal static QuantumType ConvertFromDouble(double value)
    {
        if (value < 0)
            return 0;
        if (value > Max)
            return Max;

        return (QuantumType)value;
    }

    internal static QuantumType ConvertFromInteger(int value)
    {
        if (value < 0)
            return 0;
        if (value > Max)
            return Max;

        return (QuantumType)value;
    }
}
