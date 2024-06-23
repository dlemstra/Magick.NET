// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

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

/// <content />
public partial class MagickColor
{
    private MagickColor(NativeMagickColor nativeInstance)
        => Initialize(nativeInstance);

    internal static MagickColor? CreateInstance(IntPtr instance)
    {
        if (instance == IntPtr.Zero)
            return null;

        using var nativeInstance = new NativeMagickColor(instance);
        return new MagickColor(nativeInstance);
    }

    internal static MagickColor? CreateInstance(IntPtr instance, out int count)
    {
        count = 0;

        if (instance == IntPtr.Zero)
            return null;

        using var nativeInstance = new NativeMagickColor(instance);
        count = (int)nativeInstance.Count_Get();
        return new MagickColor(nativeInstance);
    }

    private static NativeMagickColor CreateNativeInstance(IMagickColor<QuantumType> instance)
    {
        var color = NativeMagickColor.Create();
        color.Red_Set(instance.R);
        color.Green_Set(instance.G);
        color.Blue_Set(instance.B);
        color.Alpha_Set(instance.A);
        color.Black_Set(instance.K);
        color.IsCMYK_Set(instance.IsCmyk);

        return color;
    }

    private void Initialize(NativeMagickColor instance)
    {
        R = instance.Red_Get();
        G = instance.Green_Get();
        B = instance.Blue_Get();
        A = instance.Alpha_Get();
        K = instance.Black_Get();

        IsCmyk = instance.IsCMYK_Get();
    }

    [NativeInterop(QuantumType = true, ManagedToNative = true)]
    private sealed partial class NativeMagickColor : NativeInstance
    {
        public static partial NativeMagickColor Create();

        public partial ulong Count_Get();

        public partial QuantumType Red_Get();

        public partial void Red_Set(QuantumType value);

        public partial QuantumType Green_Get();

        public partial void Green_Set(QuantumType value);

        public partial QuantumType Blue_Get();

        public partial void Blue_Set(QuantumType value);

        public partial QuantumType Alpha_Get();

        public partial void Alpha_Set(QuantumType value);

        public partial QuantumType Black_Get();

        public partial void Black_Set(QuantumType value);

        public partial bool IsCMYK_Get();

        public partial void IsCMYK_Set(bool value);

        public partial bool FuzzyEquals(IMagickColor<QuantumType>? other, QuantumType fuzz);

        public partial bool Initialize(string? value);
    }
}
