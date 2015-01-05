//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
			// Read file that will raise a warning.
			using (MagickImage image = new MagickImage(SampleFiles.FileWithWarningJpg))
			{
				// Check if warning was set and write to console
				if (image.ReadWarning != null)
					Console.WriteLine(image.ReadWarning.Message);
			}

			using (MagickImage image = new MagickImage())
			{
				// Read file that will raise a warning.
				MagickWarningException warning = image.Read(SampleFiles.FileWithWarningJpg);
				// Check if warning was returned and write to console
				if (warning != null)
					Console.WriteLine(warning.Message);
			}

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
