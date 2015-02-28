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

namespace Magick.NET.Tests
{
	//==============================================================================================
	public static class Files
	{
		//===========================================================================================
#if NET20
		private static string _Root = @"..\..\..\..\Magick.NET.Tests\";
#else
		private static string _Root = @"..\..\..\Magick.NET.Tests\";
#endif
		//===========================================================================================
		public static string CirclePNG
		{
			get
			{
				return _Root + @"Images\Circle.png";
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
		public static string InvitationTif
		{
			get
			{
				return _Root + @"Images\Invitation.tif";
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
		public static string RedPNG
		{
			get
			{
				return _Root + @"Images\Red.png";
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
		public static string WireframeTIF
		{
			get
			{
				return _Root + @"Images\Wireframe.tif";
			}
		}
		//===========================================================================================
		public static class Builtin
		{
			//========================================================================================
			public static string Logo
			{
				get
				{
					return "logo:";
				}
			}
			//========================================================================================
			public static string Rose
			{
				get
				{
					return "rose:";
				}
			}
		}
		//===========================================================================================
		public static class Coders
		{
			//========================================================================================
			public static string PlayerPSD
			{
				get
				{
					return _Root + @"Images\Coders\player.psd";
				}
			}
			//========================================================================================
			public static string CartoonNetworkStudiosLogoAI
			{
				get
				{
					return _Root + @"Images\Coders\CN Studios Logo.ai";
				}
			}
			//========================================================================================
		}
		//===========================================================================================
		public static class Scripts
		{
			//========================================================================================
			public static string Collection
			{
				get
				{
					return _Root + @"Script\Collection.msl";
				}
			}
			//========================================================================================
			public static string Draw
			{
				get
				{
					return _Root + @"Script\Draw.msl";
				}
			}
			//========================================================================================
			public static string Defines
			{
				get
				{
					return _Root + @"Script\Defines.msl";
				}
			}
			//========================================================================================
			public static string Distort
			{
				get
				{
					return _Root + @"Script\Distort.msl";
				}
			}
			//===========================================================================================
			public static string Events
			{
				get
				{
					return _Root + @"Script\Events.msl";
				}
			}
			//===========================================================================================
			public static string ImageProfile
			{
				get
				{
					return _Root + @"Script\ImageProfile.msl";
				}
			}
			//===========================================================================================
			public static string Invalid
			{
				get
				{
					return _Root + @"Script\Invalid.msl";
				}
			}
			//===========================================================================================
			public static string Resize
			{
				get
				{
					return _Root + @"Script\Resize.msl";
				}
			}
			//===========================================================================================
			public static string Variables
			{
				get
				{
					return _Root + @"Script\Variables.msl";
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}