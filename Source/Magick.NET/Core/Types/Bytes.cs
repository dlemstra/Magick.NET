//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.IO;

namespace ImageMagick
{
  internal sealed class Bytes : IDisposable
  {
    private MemoryStream _stream;

    private static void CheckLength(long length)
    {
      Throw.IfTrue(nameof(length), length > int.MaxValue, "Streams with a length larger than 2147483591 are not supported, read from file instead.");
    }

    private bool Initialize(MemoryStream memStream, bool memStreamOptimization)
    {
      if (memStream == null)
        return false;

      CheckLength(memStream.Length);

      if (memStreamOptimization)
      {
        try
        {
          Data = memStream.GetBuffer();
          Length = (int)memStream.Length;

          return true;
        }
        catch (UnauthorizedAccessException)
        {
        }
      }

      Data = memStream.ToArray();
      Length = Data.Length;

      return true;
    }

    private void Initialize(Stream stream, bool memStreamOptimization)
    {
      MemoryStream memStream = stream as MemoryStream;
      if (Initialize(memStream, memStreamOptimization))
        return;

      Throw.IfFalse(nameof(stream), stream.CanRead, "The stream is not readable.");

      if (stream.CanSeek)
      {
        InitializeWithSeekableStream(stream);
        return;
      }

      int bufferSize = 8192;
      _stream = new MemoryStream();

      byte[] buffer = new byte[bufferSize];
      int length;
      while ((length = stream.Read(buffer, 0, bufferSize)) != 0)
      {
        CheckLength(_stream.Length + length);

        _stream.Write(buffer, 0, length);
      }

      Initialize(_stream, true);
    }

    private void InitializeWithSeekableStream(Stream stream)
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

    public Bytes(Stream stream)
      : this(stream, true)
    {
    }

    public Bytes(Stream stream, bool memStreamOptimization)
    {
      Throw.IfNull(nameof(stream), stream);

      Initialize(stream, memStreamOptimization);
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

    public void Dispose()
    {
      if (_stream == null)
        return;

      _stream.Dispose();
      _stream = null;
    }
  }
}
