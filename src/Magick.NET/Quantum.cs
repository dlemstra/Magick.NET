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
        Depth = NativeQuantum.Depth_Get();
        Max = NativeQuantum.Max_Get();
    }

    /// <summary>
    /// Gets the quantum depth.
    /// </summary>
    public static int Depth { get; }

    /// <summary>
    /// Gets the maximum value of the quantum.
    /// </summary>
    public static QuantumType Max { get; }

    /// <summary>
    /// Gets the quantum depth.
    /// </summary>
    int IQuantum.Depth
        => Depth;

    /// <summary>
    /// Gets the maximum value of the quantum.
    /// </summary>
    QuantumType IQuantum<QuantumType>.Max
        => Max;

    internal static QuantumType Convert(byte value)
    {
#if Q16 || Q16HDRI
        return (QuantumType)(257UL * value);
#else
        return value;
#endif
    }

    internal static QuantumType Convert(double value)
    {
        if (value < 0)
            return 0;
        if (value > Max)
            return Max;

        return (QuantumType)value;
    }

    internal static QuantumType Convert(int value)
    {
        if (value < 0)
            return 0;
        if (value > Max)
            return Max;

        return (QuantumType)value;
    }

#if !Q16
    internal static QuantumType Convert(ushort value)
    {
#if Q8
        return (QuantumType)((value + 128U) / 257U);
#elif Q16HDRI
        return (QuantumType)value;
#endif
    }
#endif

    internal static QuantumType ScaleToQuantum(double value)
        => (QuantumType)Math.Min(Math.Max(0, value * Max), Max);

    internal static byte ScaleToByte(QuantumType value)
        => NativeQuantum.ScaleToByte(value);

    internal static double ScaleToDouble(QuantumType value)
        => (1.0 / Max) * value;
}
