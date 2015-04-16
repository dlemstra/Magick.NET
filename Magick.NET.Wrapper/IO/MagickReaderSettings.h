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
#pragma once

using namespace System;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class MagickReaderSettings sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			MagickReadSettings^ _Settings;
			//========================================================================================
			property String^ Scenes
			{
				String^ get();
			}
			//========================================================================================
			void ApplyColorSpace(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyDefines(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyDensity(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyDimensions(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyFormat(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyFrame(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			void ApplyUseMonochrome(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			static String^ GetDefineKey(MagickFormat format, String^ name);
			//========================================================================================
			static void SetOption(MagickCore::ImageInfo *imageInfo, String^ key, String^ value);
			//========================================================================================
		internal:
			//========================================================================================
			bool IgnoreWarnings;
			//========================================================================================
			property Nullable<int> Height
			{
				Nullable<int> get();
			}
			//========================================================================================
			bool Ping;
			//========================================================================================
			property PixelStorageSettings^ PixelStorage
			{
				PixelStorageSettings^ get();
			}
			//========================================================================================
			property Nullable<int> Width
			{
				Nullable<int> get();
			}
			//========================================================================================
			void Apply(Magick::Image* image);
			//========================================================================================
			void Apply(MagickCore::ImageInfo *imageInfo);
			//========================================================================================
			MagickReaderSettings();
			//========================================================================================
			MagickReaderSettings(MagickReadSettings^ settings);
			//========================================================================================
		};
		//===========================================================================================
	}
}