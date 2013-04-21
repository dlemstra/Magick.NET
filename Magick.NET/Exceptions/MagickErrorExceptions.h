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
	/// Encapsulation of the ImageMagick Error exception object.
	///</summary>
	[Serializable]
	public ref class MagickErrorException : MagickException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickErrorException(String^ message) : MagickException(message) {};
		//===========================================================================================
		static MagickErrorException^ Create(const Magick::Error& exception);
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorBlob exception object.
	///</summary>
	[Serializable]
	public ref class MagickBlobErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickBlobErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorCache exception object.
	///</summary>
	[Serializable]
	public ref class MagickCacheErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCacheErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorCoder exception object.
	///</summary>
	[Serializable]
	public ref class MagickCoderErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCoderErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorConfigure exception object.
	///</summary>
	[Serializable]
	public ref class MagickConfigureErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickConfigureErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorUndefined exception object.
	///</summary>
	[Serializable]
	public ref class MagickUndefinedErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickUndefinedErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorCorruptImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickCorruptImageErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickCorruptImageErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickDelegateErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDelegateErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorDraw exception object.
	///</summary>
	[Serializable]
	public ref class MagickDrawErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickDrawErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorFileOpen exception object.
	///</summary>
	[Serializable]
	public ref class MagickFileOpenErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickFileOpenErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorImage exception object.
	///</summary>
	[Serializable]
	public ref class MagickImageErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickImageErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorMissingDelegate exception object.
	///</summary>
	[Serializable]
	public ref class MagickMissingDelegateErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickMissingDelegateErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorModule exception object.
	///</summary>
	[Serializable]
	public ref class MagickModuleErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickModuleErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorOption exception object.
	///</summary>
	[Serializable]
	public ref class MagickOptionErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickOptionErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorRegistry exception object.
	///</summary>
	[Serializable]
	public ref class MagickRegistryErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickRegistryErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorResourceLimit exception object.
	///</summary>
	[Serializable]
	public ref class MagickResourceLimitErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickResourceLimitErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorStream exception object.
	///</summary>
	[Serializable]
	public ref class MagickStreamErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickStreamErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick ErrorType exception object.
	///</summary>
	[Serializable]
	public ref class MagickTypeErrorException sealed : MagickErrorException
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickTypeErrorException(String^ message) : MagickErrorException(message) {};
		//===========================================================================================
	};
	//==============================================================================================
}
