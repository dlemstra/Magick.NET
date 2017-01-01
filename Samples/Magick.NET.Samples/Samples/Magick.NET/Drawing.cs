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

using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
  public static class DrawSamples
  {
    public static void DrawText()
    {
      using (MagickImage image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
      {
        new Drawables()
          // Draw text on the image
          .FontPointSize(72)
          .Font("Comic Sans")
          .StrokeColor(new MagickColor("yellow"))
          .FillColor(MagickColors.Orange)
          .TextAlignment(TextAlignment.Center)
          .Text(256, 64, "Magick.NET")
          // Add an ellipse
          .StrokeColor(new MagickColor(0, Quantum.Max, 0))
          .FillColor(MagickColors.SaddleBrown)
          .Ellipse(256, 96, 192, 8, 0, 360)
          .Draw(image);
      }
    }
  }
}
