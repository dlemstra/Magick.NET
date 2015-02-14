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

#include "..\Base\DefineCreator.h"
#include "DdsCompression.h"

using namespace ImageMagick::Defines;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class for defines that are used when a dds image is written.
	///</summary>
	public ref class DdsWriteDefines sealed : DefineCreator
	{
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void AddDefines(Collection<IDefine^>^ defines) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the DdsWriteDefines class.
		///</summary>
		DdsWriteDefines() : DefineCreator(MagickFormat::Dds) {};
		///==========================================================================================
		///<summary>
		/// Enables or disables cluser fit (dds:cluster-fit).
		///</summary>
		property Nullable<bool> ClusterFit;
		///==========================================================================================
		///<summary>
		/// Specifies the compression that will be used (dds:compression).
		///</summary>
		property Nullable<DdsCompression> Compression;
		///==========================================================================================
		///<summary>
		/// Specifies the number of mipmaps, zero will disable writing mipmaps (dds:mipmaps).
		///</summary>
		property Nullable<int> Mipmaps;
		///==========================================================================================
		///<summary>
		/// Enables or disables weight by alpha when cluster fit is used (dds:weight-by-alpha).
		///</summary>
		property Nullable<bool> WeightByAlpha;
		//===========================================================================================
	};
	//==============================================================================================
}