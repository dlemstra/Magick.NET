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

#include "Base\MagickException.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick Warning exception object.
	///</summary>
	[Serializable]
	public ref class MagickWarningException : MagickException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickWarningException(String^ message, MagickException^ innerException)
			: MagickException(message, innerException) {};
		//===========================================================================================
		static MagickWarningException^ Create(const Magick::Warning& exception);
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningBlob exception object.
	///</summary>
	[Serializable]
	public ref class MagickBlobWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickBlobWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCache exception object.
	///</summary>
	[Serializable]
	public ref class MagickCacheWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCacheWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCoder exception object.
	///</summary>
	[Serializable]
	public ref class MagickCoderWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCoderWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningConfigure exception object.
	///</summary>
	[Serializable]
	public ref class MagickConfigureWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickConfigureWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningUndefined exception object.
	///</summary>
	[Serializable]
	public ref class MagickUndefinedWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickUndefinedWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCorruptImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickCorruptImageWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCorruptImageWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickDelegateWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDelegateWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningDraw exception object.
	///</summary>
	[Serializable]
	public ref class MagickDrawWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDrawWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningFileOpen exception object.
	///</summary>
	[Serializable]
	public ref class MagickFileOpenWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickFileOpenWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickImageWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImageWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningMissingDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickMissingDelegateWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickMissingDelegateWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningModule exception object.
	///</summary>
	[Serializable]
	public ref class MagickModuleWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickModuleWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningOption exception object.
	///</summary>
	[Serializable]
	public ref class MagickOptionWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickOptionWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningRegistry exception object.
	///</summary>
	[Serializable]
	public ref class MagickRegistryWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickRegistryWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningResourceLimit exception object.
	///</summary>
	[Serializable]
	public ref class MagickResourceLimitWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickResourceLimitWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningStream exception object.
	///</summary>
	[Serializable]
	public ref class MagickStreamWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickStreamWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningType exception object.
	///</summary>
	[Serializable]
	public ref class MagickTypeWarningException sealed : MagickWarningException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickTypeWarningException(String^ message, MagickException^ innerException)
			: MagickWarningException(message, innerException) {};
		//===========================================================================================
	};
	//==============================================================================================
}
