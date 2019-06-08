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
    public static class DetailedDebugInformationSamples
    {
        private static void WriteLogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void MagickNET_Log(object sender, LogEventArgs arguments)
        {
            // Write log message
            WriteLogMessage(arguments.Message);
        }

        public static void ReadImage()
        {
            // Log all events
            ImageMagick.MagickNET.SetLogEvents(LogEvents.All);
            // Set the log handler (all threads use the same handler)
            ImageMagick.MagickNET.Log += MagickNET_Log;

            using (MagickImage image = new MagickImage())
            {
                // Reading the image will send all log events to the log handler
                image.Read(SampleFiles.SnakewarePng);
            }
        }
    }
}
