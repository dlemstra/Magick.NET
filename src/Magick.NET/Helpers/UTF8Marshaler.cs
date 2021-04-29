// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ImageMagick
{
    internal sealed class UTF8Marshaler : INativeInstance
    {
        private UTF8Marshaler(string? value)
        {
            Instance = ManagedToNative(value);
        }

        public IntPtr Instance { get; private set; }

        public void Dispose()
        {
            if (Instance == IntPtr.Zero)
                return;

            Marshal.FreeHGlobal(Instance);
            Instance = IntPtr.Zero;
        }

        internal static INativeInstance CreateInstance(string? value)
            => new UTF8Marshaler(value);

        internal static IntPtr ManagedToNative(string? value)
        {
            if (value == null)
                return IntPtr.Zero;

            // not null terminated
            byte[] strbuf = Encoding.UTF8.GetBytes(value);
            IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length + 1);
            Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

            // write the terminating null
#if NET20
            Marshal.WriteByte(new IntPtr(buffer.ToInt64() + strbuf.Length), 0);
#else
            Marshal.WriteByte(buffer + strbuf.Length, 0);
#endif
            return buffer;
        }

        internal static string? NativeToManaged(IntPtr nativeData)
        {
            var strbuf = ByteConverter.ToArray(nativeData);
            if (strbuf == null)
                return null;

            if (strbuf.Length == 0)
                return string.Empty;

            return Encoding.UTF8.GetString(strbuf, 0, strbuf.Length);
        }

        internal static string? NativeToManagedAndRelinquish(IntPtr nativeData)
        {
            var result = NativeToManaged(nativeData);

            MagickMemory.Relinquish(nativeData);

            return result;
        }
    }
}