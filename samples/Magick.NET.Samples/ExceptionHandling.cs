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
    public static class ExceptionHandlingSamples
    {
        private static void MagickImage_Warning(object sender, WarningEventArgs arguments)
        {
            Console.WriteLine(arguments.Message);
        }

        public static void ExceptionHandling()
        {
            try
            {
                // Read invalid jpg file
                using (MagickImage image = new MagickImage(SampleFiles.InvalidFileJpg))
                {
                }
            }
            // Catch any MagickException
            catch (MagickException exception)
            {
                // Write excepion raised when reading the invalid jpg to the console
                Console.WriteLine(exception.Message);
            }

            try
            {
                // Read corrupt jpg file
                using (MagickImage image = new MagickImage(SampleFiles.CorruptImageJpg))
                {
                }
            }
            // Catch only MagickCorruptImageErrorException
            catch (MagickCorruptImageErrorException exception)
            {
                // Write excepion raised when reading the corrupt jpg to the console
                Console.WriteLine(exception.Message);
            }
        }

        public static void ObtainWarningThatOccurredDuringRead()
        {
            using (MagickImage image = new MagickImage())
            {
                // Attach event handler to warning event
                image.Warning += MagickImage_Warning;
                // Read file that will raise a warning.
                image.Read(SampleFiles.FileWithWarningJpg);
            }
        }
    }
}
