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

using System;
using ImageMagick;

namespace RootNamespace.Samples.MagickNET
{
	public static class DetailedDebugInformationSamples
	{
		private static void WriteLogMessage(string message)
		{
			Console.WriteLine(message);
		}

		public static void MagickNET_Log(object sender, LogEventArgs arguments)
		{
			if (arguments.EventType == LogEvents.Resource) // This is an example.
				return;

			WriteLogMessage(arguments.Message);
		}

		public static void ReadImage()
		{
			ImageMagick.MagickNET.SetLogEvents(LogEvents.All);
			ImageMagick.MagickNET.Log += MagickNET_Log;

			using (MagickImage image = new MagickImage())
			{
				image.Read("Snakeware.png");
			}
		}
	}
}
