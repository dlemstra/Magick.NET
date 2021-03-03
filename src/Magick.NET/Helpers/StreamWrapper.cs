// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageMagick
{
    internal sealed unsafe class StreamWrapper : IDisposable
    {
        private const int BufferSize = 8192;

        private readonly byte[] _buffer;
        private readonly byte* _bufferStart;
        private readonly long _streamStart;
        private readonly GCHandle _handle;
        private Stream _stream;

        private StreamWrapper(Stream stream)
        {
            _stream = stream;
            _buffer = new byte[BufferSize];
            _handle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
            _bufferStart = (byte*)_handle.AddrOfPinnedObject().ToPointer();

            try
            {
                _streamStart = _stream.Position;
            }
            catch
            {
                _streamStart = 0;
            }
        }

        public static StreamWrapper CreateForReading(Stream stream)
        {
            Throw.IfFalse(nameof(stream), stream.CanRead, "The stream should be readable.");

            return new StreamWrapper(stream);
        }

        public static StreamWrapper CreateForWriting(Stream stream)
        {
            Throw.IfFalse(nameof(stream), stream.CanWrite, "The stream should be writable.");

            return new StreamWrapper(stream);
        }

        public void Dispose()
            => _handle.Free();

        public int Read(IntPtr data, UIntPtr count, IntPtr user_data)
        {
            int total = (int)count;
            if (total == 0)
                return 0;

            if (data == IntPtr.Zero)
                return 0;

            byte* p = (byte*)data.ToPointer();
            int bytesRead = 0;

            while (total > 0)
            {
                int length = Math.Min(total, BufferSize);

                try
                {
                    length = _stream.Read(_buffer, 0, length);
                }
                catch
                {
                    return -1;
                }

                if (length == 0)
                    break;

                bytesRead += length;

                p = ReadBuffer(p, length);

                total -= length;
            }

            return bytesRead;
        }

        public long Seek(long offset, IntPtr whence, IntPtr user_data)
        {
            try
            {
                switch ((SeekOrigin)whence)
                {
                    case SeekOrigin.Begin:
                        return _stream.Seek(_streamStart + offset, SeekOrigin.Begin) - _streamStart;
                    case SeekOrigin.Current:
                        return _stream.Seek(offset, SeekOrigin.Current) - _streamStart;
                    case SeekOrigin.End:
                        return _stream.Seek(offset, SeekOrigin.End) - _streamStart;
                    default:
                        return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public long Tell(IntPtr user_data)
        {
            try
            {
                return _stream.Position - _streamStart;
            }
            catch
            {
                return -1;
            }
        }

        public int Write(IntPtr data, UIntPtr count, IntPtr user_data)
        {
            int total = (int)count;
            if (total == 0)
                return 0;

            if (data == IntPtr.Zero)
                return 0;

            byte* p = (byte*)data.ToPointer();

            while (total > 0)
            {
                int length = Math.Min(total, BufferSize);

                p = FillBuffer(p, length);

                try
                {
                    _stream.Write(_buffer, 0, length);
                }
                catch
                {
                    return -1;
                }

                total -= length;
            }

            return (int)count;
        }

        private byte* FillBuffer(byte* p, int length)
        {
            byte* q = _bufferStart;
            while (length >= 4)
            {
                *(q++) = *(p++);
                *(q++) = *(p++);
                *(q++) = *(p++);
                *(q++) = *(p++);
                length -= 4;
            }

            while (length-- > 0)
            {
                *(q++) = *(p++);
            }

            return p;
        }

        private byte* ReadBuffer(byte* p, int length)
        {
            byte* q = _bufferStart;
            while (length >= 4)
            {
                *(p++) = *(q++);
                *(p++) = *(q++);
                *(p++) = *(q++);
                *(p++) = *(q++);
                length -= 4;
            }

            while (length-- > 0)
            {
                *(p++) = *(q++);
            }

            return p;
        }
    }
}
