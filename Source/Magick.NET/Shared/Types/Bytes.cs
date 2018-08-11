// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
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

        public Bytes(Stream stream)
        {
            Throw.IfNull(nameof(stream), stream);
            Throw.IfFalse(nameof(stream), stream.Position == 0, "The position of the stream should be at zero.");

            SetData(stream);
        }

        private Bytes()
        {
        }

        public byte[] Data
        {
            get;
            private set;
        }

        public int Length
        {
            get;
            private set;
        }

        public static Bytes FromStreamBuffer(Stream stream)
        {
            MemoryStream memStream = stream as MemoryStream;

            if (memStream == null || memStream.Position != 0)
                return null;

            Bytes bytes = new Bytes();
            if (bytes.SetDataWithMemoryStreamBuffer(memStream))
                return bytes;

            return null;
        }

        private static void CheckLength(long length)
        {
            Throw.IfFalse(nameof(length), IsSupportedLength(length), "Streams with a length larger than 2147483591 are not supported, read from file instead.");
        }

        private static bool IsSupportedLength(long length)
        {
            return length <= int.MaxValue;
        }

        private void SetData(Stream stream)
        {
            if (stream is MemoryStream memStream)
            {
                SetDataWithMemoryStream(memStream);
                return;
            }

            Throw.IfFalse(nameof(stream), stream.CanRead, "The stream is not readable.");

            if (stream.CanSeek)
            {
                SetDataWithSeekableStream(stream);
                return;
            }

            byte[] buffer = new byte[BufferSize];
            using (MemoryStream tempStream = new MemoryStream())
            {
                int length;
                while ((length = stream.Read(buffer, 0, BufferSize)) != 0)
                {
                    CheckLength(tempStream.Length + length);

                    tempStream.Write(buffer, 0, length);
                }

                SetDataWithMemoryStream(tempStream);
            }
        }

        private void SetDataWithMemoryStream(MemoryStream memStream)
        {
            if (SetDataWithMemoryStreamBuffer(memStream))
                return;

            Data = memStream.ToArray();
            Length = Data.Length;
        }

        private bool SetDataWithMemoryStreamBuffer(MemoryStream memStream)
        {
            if (!IsSupportedLength(memStream.Length))
                return false;

            try
            {
                Data = memStream.GetBuffer();
                Length = (int)memStream.Length;
                return true;
            }
            catch (UnauthorizedAccessException)
            {
            }

            return false;
        }

        private void SetDataWithSeekableStream(Stream stream)
        {
            CheckLength(stream.Length);

            Length = (int)stream.Length;
            Data = new byte[Length];

            int read = 0;
            int bytesRead = 0;
            while ((bytesRead = stream.Read(Data, read, Length - read)) != 0)
            {
                read += bytesRead;
            }
        }
    }
}
