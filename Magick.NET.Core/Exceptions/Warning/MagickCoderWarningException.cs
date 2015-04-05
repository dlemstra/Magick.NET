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

using System;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick WarningCoder exception object.
	///</summary>
	[Serializable]
	public sealed class MagickCoderWarningException : MagickWarningException
	{
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the MagickCoderWarningException class with a specified error
		/// message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a
		/// null reference if no inner exception is specified.</param>
		public MagickCoderWarningException(string message, MagickException innerException)
			: base(message, innerException)
		{
		}
		//===========================================================================================
	}
	//==============================================================================================
}
