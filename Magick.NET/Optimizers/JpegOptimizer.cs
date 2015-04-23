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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// Class that can be used to optimize jpeg files.
	/// </summary>
	public sealed class JpegOptimizer
	{
		//===========================================================================================
		private Wrapper.JpegOptimizer _Instance;
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the JpegOptimizer class.
		///</summary>
		/// <param name="file">The jpeg file to optimize</param>
		public JpegOptimizer(FileInfo file)
		{
			Throw.IfNull("file", file);

			_Instance = new Wrapper.JpegOptimizer(file.FullName);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the JpegOptimizer class.
		///</summary>
		/// <param name="fileName">The jpeg file to optimize</param>
		public JpegOptimizer(string fileName)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);

			_Instance = new Wrapper.JpegOptimizer(filePath);
		}
		///==========================================================================================
		/// <summary>
		/// Performs lossless compression on the file. If the new file size is not smaller the file
		/// won't be overwritten.
		/// </summary>
		public void LosslessCompress()
		{
			_Instance.LosslessCompress();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
