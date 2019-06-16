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

using System;
using ImageMagick;

namespace Magick.NET.Samples
{
    public static class ExifDataSamples
    {
        public static void ReadExifData()
        {
            // Read image from file
            using (MagickImage image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg))
            {
                // Retrieve the exif information
                ExifProfile profile = image.GetExifProfile();

                // Check if image contains an exif profile
                if (profile == null)
                    Console.WriteLine("Image does not contain exif information.");
                else
                {
                    // Write all values to the console
                    foreach (IExifValue value in profile.Values)
                    {
                        Console.WriteLine("{0}({1}): {2}", value.Tag, value.DataType, value.ToString());
                    }
                }
            }
        }

        public static void CreateThumbnailFromExifData()
        {
            // Read image from file
            using (MagickImage image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg))
            {
                // Retrieve the exif information
                ExifProfile profile = image.GetExifProfile();

                // Create thumbnail from exif information
                using (MagickImage thumbnail = profile.CreateThumbnail())
                {
                    // Check if exif profile contains thumbnail and save it
                    if (thumbnail != null)
                        thumbnail.Write(SampleFiles.OutputDirectory + "FujiFilmFinePixS1Pro.thumb.jpg");
                }
            }
        }
    }
}
