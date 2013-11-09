//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

namespace Magick.NET.Tests
{
	//==============================================================================================
	public static class Files
	{
		//===========================================================================================
		private static string _Root = @"..\..\..\Magick.NET.Tests\";
		//===========================================================================================
		public static string CollectionScript
		{
			get
			{
				return _Root + @"Script\Collection.msl";
			}
		}
		//===========================================================================================
		public static string DrawScript
		{
			get
			{
				return _Root + @"Script\Draw.msl";
			}
		}
		//===========================================================================================
		public static string EightBimTIF
		{
			get
			{
				return _Root + @"Images\8Bim.tif";
			}
		}
		//===========================================================================================
		public static string EventsScript
		{
			get
			{
				return _Root + @"Script\Events.msl";
			}
		}
		//===========================================================================================
		public static string FujiFilmFinePixS1ProJPG
		{
			get
			{
				return _Root + @"Images\FujiFilmFinePixS1Pro.jpg";
			}
		}
		//===========================================================================================
		public static string ImageMagickJPG
		{
			get
			{
				return _Root + @"Images\ImageMagick.jpg";
			}
		}
		//===========================================================================================
		public static string ImageProfileScript
		{
			get
			{
				return _Root + @"Script\ImageProfile.msl";
			}
		}
		//===========================================================================================
		public static string InvalidScript
		{
			get
			{
				return _Root + @"Script\Invalid.msl";
			}
		}
		//===========================================================================================
		public static string MagickNETIconPNG
		{
			get
			{
				return _Root + @"Images\Magick.NET.icon.png";
			}
		}
		//===========================================================================================
		public static string Missing
		{
			get
			{
				return @"C:\Foo\Bar.png";
			}
		}
		//===========================================================================================
		public static string RedPng
		{
			get
			{
				return _Root + @"Images\Red.png";
			}
		}
		//===========================================================================================
		public static string ResizeScript
		{
			get
			{
				return _Root + @"Script\Resize.msl";
			}
		}
		//===========================================================================================
		public static string RoseSparkleGIF
		{
			get
			{
				return _Root + @"Images\RöseSparkle.gif";
			}
		}
		//===========================================================================================
		public static string SnakewarePNG
		{
			get
			{
				return _Root + @"Images\Snakeware.png";
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}