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

#include "MagickImage.h"
#include "Enums\LayerMethod.h"
#include "IO\MagickReaderSettings.h"
#include "Settings\MontageSettings.h"

using namespace System::Collections::Generic;
using namespace System::Drawing::Imaging;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class MagickImageCollection sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			EventHandler<WarningEventArgs^>^ _WarningEvent;
			//========================================================================================
			MagickReaderSettings^ CheckSettings(MagickReadSettings^ readSettings);
			//========================================================================================
			static void Copy(IEnumerable<MagickImage^>^ source, std::vector<Magick::Image>* destination);
			//========================================================================================
			void HandleException(const Magick::Exception& exception);
			//========================================================================================
			void HandleException(MagickException^ exception);
			//========================================================================================
			bool Merge(IEnumerable<MagickImage^>^ images, Magick::Image* image, LayerMethod method);
			//========================================================================================
			IEnumerable<MagickImage^>^ Optimize(IEnumerable<MagickImage^>^ images, LayerMethod optizeMethod);
			//========================================================================================
		internal:
			//========================================================================================
			static IEnumerable<MagickImage^>^ Copy(std::vector<Magick::Image>* images);
			//========================================================================================
		public:
			//========================================================================================
			MagickImageCollection();
			//========================================================================================
			event EventHandler<WarningEventArgs^>^ Warning
			{
				void add(EventHandler<WarningEventArgs^>^ handler);
				void remove(EventHandler<WarningEventArgs^>^ handler);
			}
			//========================================================================================
			MagickImage^ Append(IEnumerable<MagickImage^>^ images, bool vertically);
			//========================================================================================
			IEnumerable<MagickImage^>^ Coalesce(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			MagickImage^ Combine(IEnumerable<MagickImage^>^ images, Channels channels);
			//========================================================================================
			IEnumerable<MagickImage^>^ Deconstruct(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			MagickImage^ Evaluate(IEnumerable<MagickImage^>^ images, EvaluateOperator evaluateOperator);
			//========================================================================================
			MagickImage^ Flatten(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			MagickImage^ Fx(IEnumerable<MagickImage^>^ images, String^ expression);
			//========================================================================================
			IEnumerable<MagickImage^>^ Map(IEnumerable<MagickImage^>^ images, QuantizeSettings^ settings);
			//========================================================================================
			MagickImage^ Merge(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			MagickImage^ Montage(IEnumerable<MagickImage^>^ images, MontageSettings^ settings);
			//========================================================================================
			IEnumerable<MagickImage^>^ Morph(IEnumerable<MagickImage^>^ images, int frames);
			//========================================================================================
			MagickImage^ Mosaic(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			IEnumerable<MagickImage^>^ Optimize(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			IEnumerable<MagickImage^>^ OptimizePlus(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			IEnumerable<MagickImage^>^  OptimizeTransparency(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			IEnumerable<MagickImage^>^ Read(array<Byte>^ data, MagickReadSettings^ readSettings);
			//========================================================================================
			IEnumerable<MagickImage^>^ Read(Stream^ stream, MagickReadSettings^ readSettings);
			//========================================================================================
			IEnumerable<MagickImage^>^ Read(String^ fileName, MagickReadSettings^ readSettings);
			//========================================================================================
			MagickImage^ Smush(IEnumerable<MagickImage^>^ images, int offset, bool vertically);
			//========================================================================================
			array<Byte>^ ToByteArray(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			MagickImage^ TrimBounds(IEnumerable<MagickImage^>^ images);
			//========================================================================================
			void Write(IEnumerable<MagickImage^>^ images, Stream^ stream);
			//========================================================================================
			void Write(IEnumerable<MagickImage^>^ images, String^ fileName);
			//========================================================================================
		};
		//===========================================================================================
	}
}