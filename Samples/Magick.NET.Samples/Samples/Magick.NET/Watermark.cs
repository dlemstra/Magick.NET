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
using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
  public static class WatermarkSamples
  {
    public static void CreateWatermark()
    {
      // Read image that needs a watermark
      using (MagickImage image = new MagickImage(SampleFiles.FujiFilmFinePixS1ProJpg))
      {
        // Read the watermark that will be put on top of the image
        using (MagickImage watermark = new MagickImage(SampleFiles.SnakewarePng))
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
