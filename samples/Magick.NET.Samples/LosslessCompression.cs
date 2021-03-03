// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;

namespace Magick.NET.Samples
{
    public static class LosslessCompressionSamples
    {
        public static void MakeGooglePageSpeedInsightsHappy()
        {
            var snakewareLogo = new FileInfo(SampleFiles.OutputDirectory + "OptimizeTest.jpg");
            File.Copy(SampleFiles.SnakewareJpg, snakewareLogo.FullName, true);

            Console.WriteLine("Bytes before: " + snakewareLogo.Length);

            var optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(snakewareLogo);

            snakewareLogo.Refresh();
            Console.WriteLine("Bytes after:  " + snakewareLogo.Length);
        }
    }
}
