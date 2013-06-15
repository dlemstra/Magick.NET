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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Specifies compression methods.
	///</summary>
	public enum class CompressionMethod
	{
		Undefined = MagickCore::UndefinedCompression,
		NoCompression = MagickCore::NoCompression,
		BZip = MagickCore::BZipCompression,
		DXT1 = MagickCore::DXT1Compression,
		DXT3 = MagickCore::DXT3Compression,
		DXT5 = MagickCore::DXT5Compression,
		Faxo = MagickCore::FaxCompression,
		Group4 = MagickCore::Group4Compression,
		JPEG = MagickCore::JPEGCompression,
		JPEG2000 = MagickCore::JPEG2000Compression,
		LosslessJPEG = MagickCore::LosslessJPEGCompression,
		LZW = MagickCore::LZWCompression,
		RLE = MagickCore::RLECompression,
		Zip = MagickCore::ZipCompression,
		ZipS = MagickCore::ZipSCompression,
		Piz = MagickCore::PizCompression,
		Pxr24 = MagickCore::Pxr24Compression,
		B44 = MagickCore::B44Compression,
		B44A = MagickCore::B44ACompression,
		LZMA = MagickCore::LZMACompression,
		JBIG1 = MagickCore::JBIG1Compression,
		JBIG2 = MagickCore::JBIG2Compression
	};
	//==============================================================================================
}