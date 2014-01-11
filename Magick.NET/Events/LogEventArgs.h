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
#pragma once
#include "..\Enums\LogEvents.h"
#include "..\Exceptions\Base\MagickException.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// EventArgs for Log events.
	///</summary>
	public ref class LogEventArgs sealed : EventArgs
	{
		//===========================================================================================
	private:
		LogEvents _EventType;
		String^ _Message;
		//===========================================================================================
	internal:
		//===========================================================================================
		LogEventArgs(LogEvents eventType, String^ message);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Type of the log message.
		///</summary>
		property String^ Message
		{
			String^ get();
		}
		///==========================================================================================
		///<summary>
		/// Type of the log message.
		///</summary>
		property LogEvents EventType
		{
			LogEvents get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}