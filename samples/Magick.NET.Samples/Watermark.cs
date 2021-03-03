// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;

namespace Magick.NET.Samples
{
    public static class WatermarkSamples
    {
        public static void CreateWatermark()
        {
            // Read image that needs a watermark
            using (var image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg))
            {
                // Read the watermark that will be put on top of the image
                using (var watermark = new MagickImage(SampleFiles.SnakewarePng))
                {
                    // Draw the watermark in the bottom right corner
                    image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);

                    // Optionally make the watermark more transparent
                    watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);

                    // Or draw the watermark at a specific location
                    image.Composite(watermark, 200, 50, CompositeOperator.Over);
                }

                // Save the result
                image.Write(SampleFiles.OutputDirectory + "FujiFilmFinePixS1Pro.watermark.jpg");
            }
        }
    }
}
