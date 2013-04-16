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
#include "MagickException.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick Warning exception object.
	///</summary>
	[Serializable]
	public ref class MagickWarningException : public MagickException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickWarningException(String^ message) : MagickException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningBlob exception object.
	///</summary>
	[Serializable]
	public ref class MagickBlobWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickBlobWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCache exception object.
	///</summary>
	[Serializable]
	public ref class MagickCacheWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCacheWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCoder exception object.
	///</summary>
	[Serializable]
	public ref class MagickCoderWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCoderWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningConfigure exception object.
	///</summary>
	[Serializable]
	public ref class MagickConfigureWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickConfigureWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningUndefined exception object.
	///</summary>
	[Serializable]
	public ref class MagickUndefinedWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickUndefinedWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCorruptImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickCorruptImageWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCorruptImageWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickDelegateWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDelegateWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningDraw exception object.
	///</summary>
	[Serializable]
	public ref class MagickDrawWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDrawWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningFileOpen exception object.
	///</summary>
	[Serializable]
	public ref class MagickFileOpenWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickFileOpenWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickImageWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImageWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningMissingDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickMissingDelegateWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickMissingDelegateWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningModule exception object.
	///</summary>
	[Serializable]
	public ref class MagickModuleWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickModuleWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningOption exception object.
	///</summary>
	[Serializable]
	public ref class MagickOptionWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickOptionWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningRegistry exception object.
	///</summary>
	[Serializable]
	public ref class MagickRegistryWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickRegistryWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningResourceLimit exception object.
	///</summary>
	[Serializable]
	public ref class MagickResourceLimitWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickResourceLimitWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningStream exception object.
	///</summary>
	[Serializable]
	public ref class MagickStreamWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickStreamWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningType exception object.
	///</summary>
	[Serializable]
	public ref class MagickTypeWarningException sealed : public MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickTypeWarningException(String^ message) : MagickWarningException(message) {};
		//===========================================================================================
	};
	//==============================================================================================
}
