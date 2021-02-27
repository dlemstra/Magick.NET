// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;

namespace ImageMagick
{
    internal sealed class Bytes
    {
        private const int BufferSize = 8192;
        private readonly byte[] _data;
        private readonly int _length;

        public Bytes(Stream stream)
        {
            Throw.IfNull(nameof(stream), stream);
            Throw.IfFalse(nameof(stream), stream.Position == 0, "The position of the stream should be at zero.");

            _data = GetData(stream, out _length);
        }

        private Bytes(byte[] data, int length)
        {
            _data = data;
            _length = length;
        }

        public int Length
            => _length;

        public static Bytes? FromStreamBuffer(Stream stream)
        {
            var memStream = stream as MemoryStream;

            if (memStream == null || memStream.Position != 0)
                return null;

            var data = GetDataFromMemoryStreamBuffer(memStream, out var length);
            if (data == null)
                return null;

            return new Bytes(data, length);
        }

        public byte[] GetData()
            => _data;

        private static byte[] GetData(Stream stream, out int length)
        {
            if (stream is MemoryStream memStream)
                return GetDataFromMemoryStream(memStream, out length);

            Throw.IfFalse(nameof(stream), stream.CanRead, "The stream is not readable.");

            if (stream.CanSeek)
                return GetDataWithSeekableStream(stream, out length);

            var buffer = new byte[BufferSize];
            using (var tempStream = new MemoryStream())
            {
                int count;
                while ((count = stream.Read(buffer, 0, BufferSize)) != 0)
                {
                    CheckLength(tempStream.Length + count);

                    tempStream.Write(buffer, 0, count);
                }

                return GetDataFromMemoryStream(tempStream, out length);
            }
        }

        private static byte[] GetDataWithSeekableStream(Stream stream, out int length)
        {
            CheckLength(stream.Length);

            length = (int)stream.Length;
            var data = new byte[length];

            int read = 0;
            int bytesRead;
            while ((bytesRead = stream.Read(data, read, length - read)) != 0)
            {
                read += bytesRead;
            }

            return data;
        }

        private static byte[] GetDataFromMemoryStream(MemoryStream memStream, out int length)
        {
            var data = GetDataFromMemoryStreamBuffer(memStream, out length);
            if (data != null)
                return data;

            data = memStream.ToArray();
            length = data.Length;

            return data;
        }

        private static byte[]? GetDataFromMemoryStreamBuffer(MemoryStream memStream, out int length)
        {
            length = 0;

            if (!IsSupportedLength(memStream.Length))
                return null;

#if NETSTANDARD
            if (!memStream.TryGetBuffer(out var buffer))
                return null;

            if (buffer.Offset == 0)
            {
                length = (int)memStream.Length;
                return buffer.Array;
            }
#else
            try
            {
                length = (int)memStream.Length;
                return memStream.GetBuffer();
            }
            catch (UnauthorizedAccessException)
            {
            }
#endif

            return null;
        }

        private static void CheckLength(long length)
        {
            Throw.IfFalse(nameof(length), IsSupportedLength(length), $"Streams with a length larger than {int.MaxValue} are not supported, read from file instead.");
        }

        private static bool IsSupportedLength(long length)
            => length <= int.MaxValue;
    }
}
