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

using System.IO;
using ImageMagick;

namespace Magick.NET.Samples
{
    public static class MagickScriptSamples
    {
        private static void OnScriptRead(object sender, ScriptReadEventArgs arguments)
        {
            arguments.Image = new MagickImage(SampleFiles.SnakewareJpg);
        }

        private static void OnScriptWrite(object sender, ScriptWriteEventArgs arguments)
        {
            arguments.Image.Write(SampleFiles.SnakewarePng);
        }

        public static void Resize()
        {
            // Load resize script and execute it
            MagickScript script = new MagickScript(SampleFiles.ResizeMsl);
            script.Execute();
        }

        public static void ReuseSameScript()
        {
            // Load wave script
            MagickScript script = new MagickScript(SampleFiles.WaveMsl);

            // Execute script multiple times
            string[] files = new string[] { SampleFiles.FujiFilmFinePixS1ProJpg, SampleFiles.SnakewareJpg };
            foreach (string fileName in files)
            {
                // Read image from file
                using (MagickImage image = new MagickImage(fileName))
                {
                    // Execute script with the image and write it to a jpg file
                    script.Execute(image);
                    image.Write(SampleFiles.OutputDirectory + fileName + ".wave.jpg");
                }
            }
        }

        public static void ReadWriteEvents()
        {
            // Load crop script
            MagickScript script = new MagickScript(SampleFiles.CropMsl);
            // Event that will be raised when the script wants to read a file
            script.Read += OnScriptRead;
            // Event that will be raised when the script wants to write a file
            script.Write += OnScriptWrite;
            // Execute the script
            script.Execute();
        }

        public static void WriteMultipleOutputFiles()
        {
            // Load clone script and execute it
            MagickScript script = new MagickScript(SampleFiles.CloneMsl);
            script.Execute();
        }
    }
}
