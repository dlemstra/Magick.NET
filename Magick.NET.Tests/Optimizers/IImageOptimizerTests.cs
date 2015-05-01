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
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	public abstract class IImageOptimizerTests
	{
		//===========================================================================================
		private static FileInfo CreateTemporaryFile(string fileName)
		{
			string tempFile = Path.GetTempPath() + Guid.NewGuid().ToString() + Path.GetExtension(fileName);
			File.Copy(fileName, tempFile, true);

			return new FileInfo(tempFile);
		}
		//===========================================================================================
		protected abstract ILosslessImageOptimizer CreateLosslessImageOptimizer();
		//===========================================================================================
		protected void Test_LosslessCompress(string fileName)
		{
			FileInfo tempFile = CreateTemporaryFile(fileName);
			try
			{
				ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
				Assert.IsNotNull(optimizer);

				long before = tempFile.Length;
				optimizer.LosslessCompress(tempFile);

				tempFile.Refresh();
				long after = tempFile.Length;
				Assert.AreNotEqual(before, after);
			}
			finally
			{
				if (tempFile.Exists)
					tempFile.Delete();
			}
		}
		//===========================================================================================
		protected void Test_LosslessCompress_InvalidFile(string fileName)
		{
			FileInfo tempFile = CreateTemporaryFile(fileName);
			try
			{
				ExceptionAssert.Throws<MagickCorruptImageErrorException>(delegate()
				{
					ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
					Assert.IsNotNull(optimizer);

					optimizer.LosslessCompress(tempFile);
				});
			}
			finally
			{
				if (tempFile.Exists)
					tempFile.Delete();
			}
		}
		//===========================================================================================
		protected void Test_LosslessCompress_InvalidArguments()
		{
			ILosslessImageOptimizer optimizer = CreateLosslessImageOptimizer();
			Assert.IsNotNull(optimizer);

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				optimizer.LosslessCompress((FileInfo)null);
			});

			ExceptionAssert.Throws<ArgumentNullException>(delegate()
			{
				optimizer.LosslessCompress((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				optimizer.LosslessCompress("");
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				optimizer.LosslessCompress(Files.Missing);
			});
		}
		//===========================================================================================
	}
	//==============================================================================================
}
