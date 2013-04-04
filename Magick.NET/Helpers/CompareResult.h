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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates the information from an image comparison.
	///</summary>
	public ref class CompareResult sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _MeanErrorPerPixel;
		double _NormalizedMeanError;
		double _NormalizedMaximumError;
		//===========================================================================================
	internal:
		//===========================================================================================
		CompareResult(double meanErrorPerPixel, double normalizedMaximumError, double normalizedMeanError);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The mean error per pixel computed when an image is color reduced.
		///</summary>
		property double MeanErrorPerPixel
		{
			double get()
			{
				return _MeanErrorPerPixel;
			}
		}
		///==========================================================================================
		///<summary>
		/// The normalized maximum error per pixel computed when an image is color reduced.
		///</summary>
		property double NormalizedMaximumError
		{
			double get()
			{
				return _NormalizedMaximumError;
			}
		}
		///==========================================================================================
		///<summary>
		/// The normalized mean error per pixel computed when an image is color reduced.
		///</summary>
		property double NormalizedMeanError
		{
			double get()
			{
				return _NormalizedMeanError;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}
