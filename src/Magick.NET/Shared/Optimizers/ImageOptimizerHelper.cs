// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
    internal static class ImageOptimizerHelper
    {
        public static void CheckFormat(IMagickImage image, MagickFormat expectedFormat)
        {
            var format = image.FormatInfo.Module;
            if (format != expectedFormat)
                throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString());
        }

        public static void CheckStream([ValidatedNotNull] Stream stream)
        {
            Throw.IfNullOrEmpty(nameof(stream), stream);
            Throw.IfFalse(nameof(stream), stream.CanRead, "The stream should be readable.");
            Throw.IfFalse(nameof(stream), stream.CanWrite, "The stream should be writeable.");
            Throw.IfFalse(nameof(stream), stream.CanSeek, "The stream should be seekable.");
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not sure which exception will be thrown.")]
        public static MagickFormatInfo GetFormatInformation(FileInfo file)
        {
            var info = MagickNET.GetFormatInformation(file);
            if (info != null)
                return info;

            try
            {
                var imageInfo = new MagickImageInfo(file);
                return MagickNET.GetFormatInformation(imageInfo.Format);
            }
            catch
            {
                try
                {
                    using (var stream = file.OpenRead())
                    {
                        return GetFormatInformationFromHeader(stream);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not sure which exception will be thrown.")]
        public static MagickFormatInfo GetFormatInformation(string fileName)
        {
            var info = MagickNET.GetFormatInformation(fileName);
            if (info != null)
                return info;

            try
            {
                var imageInfo = new MagickImageInfo(fileName);
                return MagickNET.GetFormatInformation(imageInfo.Format);
            }
            catch
            {
                try
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        return GetFormatInformationFromHeader(stream);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not sure which exception will be thrown.")]
        public static MagickFormatInfo GetFormatInformation(Stream stream)
        {
            var startPosition = stream.Position;

            try
            {
                var info = new MagickImageInfo(stream);
                return MagickNET.GetFormatInformation(info.Format);
            }
            catch
            {
                stream.Position = startPosition;

                return GetFormatInformationFromHeader(stream);
            }
            finally
            {
                stream.Position = startPosition;
            }
        }

        private static MagickFormatInfo GetFormatInformationFromHeader(Stream stream)
        {
            var buffer = new byte[4];
            stream.Read(buffer, 0, buffer.Length);

            if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 1 && buffer[3] == 0)
                return MagickNET.GetFormatInformation(MagickFormat.Ico);

            return null;
        }
    }
}
