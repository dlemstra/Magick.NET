//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.Drawing;
using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
	public static class UsingColorsSamples
	{
		public static void UsingColors()
		{
			using (MagickImage image = new MagickImage(SampleFiles.SnakewarePng))
			{
				image.TransparentChroma(Color.Black, Color.Blue);
				image.BackgroundColor = new ColorMono(true);

				// Q16:
				/*
				// This will not work in Q8 and that is why the comment was added.
				image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, 65535));
				image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, 0, 65535));
				image.BackgroundColor = new MagickColor("#00f");
				image.BackgroundColor = new MagickColor("#0000ff");
				image.BackgroundColor = new MagickColor("#00000000ffff");
				*/

				// Q8:
				image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, 255));
				image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, 0, 255));
				image.BackgroundColor = new MagickColor("#00f");
				image.BackgroundColor = new MagickColor("#0000ff");
			}
		}
	}
}
