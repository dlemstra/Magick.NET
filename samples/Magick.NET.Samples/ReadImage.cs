// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using ImageMagick;

namespace Magick.NET.Samples
{
    public static class ReadImageSamples
    {
        private static MemoryStream LoadMemoryStreamImage()
        {
            return new MemoryStream(LoadImageBytes());
        }

        private static byte[] LoadImageBytes()
        {
            return File.ReadAllBytes(SampleFiles.SnakewarePng);
        }

        public static void ReadImage()
        {
            // Read from file.
            using (var image = new MagickImage(SampleFiles.SnakewareJpg))
            {
            }

            // Read from stream.
            using (var memStream = LoadMemoryStreamImage())
            {
                using (var image = new MagickImage(memStream))
                {
                }
            }

            // Read from byte array.
            var data = LoadImageBytes();
            using (var image = new MagickImage(data))
            {
            }

            // Read image that has no predefined dimensions.
            var settings = new MagickReadSettings();
            settings.Width = 800;
            settings.Height = 600;
            using (var image = new MagickImage("xc:yellow", settings))
            {
            }

            using (var image = new MagickImage())
            {
                image.Read(SampleFiles.SnakewareJpg);
                image.Read(data);
                image.Read("xc:yellow", settings);

                using (var memStream = LoadMemoryStreamImage())
                {
                    image.Read(memStream);
                }
            }
        }

        public static void ReadBasicImageInformation()
        {
            // Read from file
            var info = new MagickImageInfo(SampleFiles.SnakewarePng);

            // Read from stream
            using (var memStream = LoadMemoryStreamImage())
            {
                info = new MagickImageInfo(memStream);
            }

            // Read from byte array
            var data = LoadImageBytes();
            info = new MagickImageInfo(data);

            info = new MagickImageInfo();
            info.Read(SampleFiles.SnakewarePng);
            using (var memStream = LoadMemoryStreamImage())
            {
                info.Read(memStream);
            }
            info.Read(data);

            Console.WriteLine(info.Width);
            Console.WriteLine(info.Height);
            Console.WriteLine(info.ColorSpace);
            Console.WriteLine(info.Format);
            Console.WriteLine(info.Density.X);
            Console.WriteLine(info.Density.Y);
            Console.WriteLine(info.Density.Units);
        }

        public static void ReadImageWithMultipleFrames()
        {
            // Read from file
            using (var collection = new MagickImageCollection(SampleFiles.SnakewareJpg))
            {
            }

            // Read from stream
            using (var memStream = LoadMemoryStreamImage())
            {
                using (var collection = new MagickImageCollection(memStream))
                {
                }
            }

            // Read from byte array
            var data = LoadImageBytes();
            using (var collection = new MagickImageCollection(data))
            {
            }

            // Read pdf with custom density.
            var settings = new MagickReadSettings();
            settings.Density = new Density(144);

            using (var collection = new MagickImageCollection(SampleFiles.SnakewarePdf, settings))
            {
            }

            using (var collection = new MagickImageCollection())
            {
                collection.Read(SampleFiles.SnakewareJpg);
                using (var memStream = LoadMemoryStreamImage())
                {
                    collection.Read(memStream);
                }
                collection.Read(data);
                collection.Read(SampleFiles.SnakewarePdf, settings);
            }
        }
    }
}
