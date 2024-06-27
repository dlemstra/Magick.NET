// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
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
public partial class MagickImageCollection
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long ReadWriteStreamDelegate(IntPtr data, UIntPtr length, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long SeekStreamDelegate(long offset, IntPtr whence, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long TellStreamDelegate(IntPtr user_data);

    [NativeInterop(RaiseWarnings = true)]
    private unsafe partial class NativeMagickImageCollection : NativeHelper
    {
        public static partial void Dispose(IntPtr value);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Append(IMagickImage image, bool stack);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Coalesce(IMagickImage image);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Combine(IMagickImage image, ColorSpace colorSpace);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Complex(IMagickImage image, ComplexOperator complexOperator);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Deconstruct(IMagickImage image);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Evaluate(IMagickImage image, EvaluateOperator evaluateOperator);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Map(IMagickImage image, IQuantizeSettings settings, IMagickImage remapImage);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Merge(IMagickImage image, LayerMethod method);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Montage(IMagickImage image, IMontageSettings<QuantumType>? settings);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Morph(IMagickImage image, nuint frames);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Optimize(IMagickImage image);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr OptimizePlus(IMagickImage image);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void OptimizeTransparency(IMagickImage image);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Polynomial(IMagickImage image, double[] terms, nuint length);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void Quantize(IMagickImage image, IQuantizeSettings settings);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr ReadBlob(IMagickSettings<QuantumType>? settings, byte[] data, nuint offset, nuint length);

#if NETSTANDARD2_1
        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr ReadBlob(IMagickSettings<QuantumType>? settings, ReadOnlySpan<byte> data, nuint offset, nuint length);
#endif

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr ReadFile(IMagickSettings<QuantumType>? settings);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr ReadStream(IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate reader, SeekStreamDelegate? seeker, TellStreamDelegate? teller, void* data);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Smush(IMagickImage image, nuint offset, bool stack);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void WriteFile(IMagickImage image, IMagickSettings<QuantumType>? settings);

        [Throws]
        [Instance(SetsInstance = false)]
        public partial void WriteStream(IMagickImage image, IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate writer, SeekStreamDelegate? seeker, TellStreamDelegate? teller, ReadWriteStreamDelegate? reader, void* data);
    }
}
