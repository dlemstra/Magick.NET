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
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// Class that can be used to optimize png files.
	/// </summary>
	public sealed class PngOptimizer
	{
		//===========================================================================================
		private FileInfo _File;
		//===========================================================================================
		private IEnumerable<int> GetQualityList()
		{
			if (OptimizeCompression)
				return new int[] { 91, 94, 95, 97 };
			else
				return new int[] { 90 };
		}
		//===========================================================================================
		private void Initialize(FileInfo file)
		{
			_File = file;
			OptimizeCompression = false;
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PngOptimizer class.
		///</summary>
		/// <param name="file">The png file to optimize</param>
		public PngOptimizer(FileInfo file)
		{
			Throw.IfNull("file", file);

			Initialize(file);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PngOptimizer class.
		///</summary>
		/// <param name="fileName">The png file to optimize</param>
		public PngOptimizer(string fileName)
		{
			string filePath = FileHelper.CheckForBaseDirectory(fileName);

			Throw.IfInvalidFileName(filePath);

			Initialize(new FileInfo(filePath));
		}
		///==========================================================================================
		/// <summary>
		/// Performs lossless compression on the file. If the new file size is not smaller the file
		/// won't be overwritten.
		/// </summary>
		public void LosslessCompress()
		{
			using (MagickImage image = new MagickImage(_File))
			{
				image.Strip();
				image.SetDefine(MagickFormat.Png, "exclude-chunks", "all");
				image.SetDefine(MagickFormat.Png, "include-chunks", "tRNS,gAMA");

				Collection<FileInfo> tempFiles = new Collection<FileInfo>();

				try
				{
					FileInfo bestFile = null;

					foreach (int quality in GetQualityList())
					{
						FileInfo tempFile = new FileInfo(Path.GetTempFileName());
						tempFiles.Add(tempFile);

						image.Quality = quality;
						image.Write(tempFile);
						tempFile.Refresh();

						if (bestFile == null)
							bestFile = tempFile;
						else if (bestFile.Length > tempFile.Length)
							bestFile = tempFile;
						else
							bestFile.Delete();
					}

					if (bestFile.Length < _File.Length)
						bestFile.CopyTo(_File.FullName, true);
				}
				finally
				{
					foreach (FileInfo tempFile in tempFiles)
					{
						if (tempFile.Exists)
							tempFile.Delete();
					}
				}
			}
		}
		///==========================================================================================
		/// <summary>
		/// When set to true various compression types will be used to find the smallest file. This
		/// process will take extra time because the PNG file has to be written multiple times.
		/// </summary>
		public bool OptimizeCompression
		{
			get;
			set;
		}
		//===========================================================================================
	}
	//==============================================================================================
}
