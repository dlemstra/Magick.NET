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

using System.Collections.Generic;
using System.IO;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.ImageOptimizers
{
    internal sealed class PngHelper
    {
        private readonly bool _optimalCompression;

        public PngHelper(IImageOptimizer optimizer)
        {
            _optimalCompression = optimizer.OptimalCompression;
        }

        public TemporaryFile? FindBestFileQuality(IMagickImage<QuantumType> image, out int bestQuality)
        {
            bestQuality = 0;

            CheckTransparency(image);

            TemporaryFile? bestFile = null;

            foreach (var quality in GetQualityList())
            {
                TemporaryFile? tempFile = null;

                try
                {
                    tempFile = new TemporaryFile();

                    image.Quality = quality;
                    image.Write(tempFile);

                    if (bestFile == null || bestFile.Length > tempFile.Length)
                    {
                        if (bestFile != null)
                            bestFile.Dispose();

                        bestFile = tempFile;
                        bestQuality = quality;
                        tempFile = null;
                    }
                }
                finally
                {
                    if (tempFile != null)
                        tempFile.Dispose();
                }
            }

            return bestFile;
        }

        public MemoryStream? FindBestStreamQuality(IMagickImage<QuantumType> image, out int bestQuality)
        {
            bestQuality = 0;

            CheckTransparency(image);

            MemoryStream? bestStream = null;

            foreach (var quality in GetQualityList())
            {
                var memStream = new MemoryStream();

                try
                {
                    image.Quality = quality;
                    image.Write(memStream);

                    if (bestStream == null || memStream.Length < bestStream.Length)
                    {
                        if (bestStream != null)
                            bestStream.Dispose();

                        bestStream = memStream;
                        bestQuality = quality;
                        memStream = null;
                    }
                }
                finally
                {
                    if (memStream != null)
                        memStream.Dispose();
                }
            }

            return bestStream;
        }

        private static void CheckTransparency(IMagickImage<QuantumType> image)
        {
            if (!image.HasAlpha)
                return;

            if (image.IsOpaque)
                image.HasAlpha = false;
        }

        private IEnumerable<int> GetQualityList()
        {
            if (_optimalCompression)
                return new int[] { 91, 94, 95, 97 };
            else
                return new int[] { 90 };
        }
    }
}
