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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public sealed class PixelTests
	{
		//===========================================================================================
		private const string _Category = "Pixel";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			Pixel first = new Pixel(0, 0, 3);
			first.SetChannel(0, 100);
			first.SetChannel(1, 100);
			first.SetChannel(2, 100);

			Assert.IsFalse(first == null);
			Assert.IsFalse(first.Equals(null));
			Assert.IsTrue(first.Equals(first));
			Assert.IsTrue(first.Equals((object)first));

			Pixel second = new Pixel(10, 10, 3);
			second.SetChannel(0, 100);
			second.SetChannel(1, 0);
			second.SetChannel(2, 100);

			Assert.IsTrue(first != second);
			Assert.IsTrue(!first.Equals(second));
			Assert.IsTrue(!first.Equals((object)second));

			second.SetChannel(1, 100);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));
		}
		//===========================================================================================
	}
	//==============================================================================================
}
