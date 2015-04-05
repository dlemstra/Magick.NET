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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates the error information.
	///</summary>
	public sealed class MagickErrorInfo
	{
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickErrorInfo class.
		///</summary>
		public MagickErrorInfo()
			: this(0, 0, 0)
		{
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickErrorInfo class using the specified values.
		///</summary>
		///<param name="meanErrorPerPixel">The mean error per pixel computed when an image is color reduced.</param>
		///<param name="normalizedMeanError">The normalized mean error per pixel computed when an image is color reduced.</param>
		///<param name="normalizedMaximumError">The normalized maximum error per pixel computed when an image is color reduced.</param>
		public MagickErrorInfo(double meanErrorPerPixel, double normalizedMeanError, double normalizedMaximumError)
		{
			MeanErrorPerPixel = meanErrorPerPixel;
			NormalizedMeanError = normalizedMeanError;
			NormalizedMaximumError = normalizedMaximumError;
		}
		///==========================================================================================
		///<summary>
		/// The mean error per pixel computed when an image is color reduced.
		///</summary>
		public double MeanErrorPerPixel
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// The normalized maximum error per pixel computed when an image is color reduced.
		///</summary>
		public double NormalizedMaximumError
		{
			get;
			private set;
		}
		///==========================================================================================
		///<summary>
		/// The normalized mean error per pixel computed when an image is color reduced.
		///</summary>
		public double NormalizedMeanError
		{
			get;
			private set;
		}
		//===========================================================================================
	}
	//==============================================================================================
}
