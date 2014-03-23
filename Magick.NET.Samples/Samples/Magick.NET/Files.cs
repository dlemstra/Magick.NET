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

namespace RootNamespace.Samples.MagickNET
{
	public static class SampleFiles
	{
		private const string _FilesDirectory = @"$fullpath$Samples\Magick.NET\Files\";

		public static string SnakewareGif
		{
			get
			{
				return _FilesDirectory + "Snakeware.gif";
			}
		}

		public static string SnakewareJpg
		{
			get
			{
				return _FilesDirectory + "Snakeware.jpg";
			}
		}

		public static string SnakewarePdf
		{
			get
			{
				return _FilesDirectory + "Snakeware.pdf";
			}
		}

		public static string SnakewarePng
		{
			get
			{
				return _FilesDirectory + "Snakeware.png";
			}
		}
	}
}
