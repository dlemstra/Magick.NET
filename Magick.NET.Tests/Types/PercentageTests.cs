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
	public class PercentageTests
	{
		//===========================================================================================
		private const string _Category = "Percentage";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			Percentage percentage = new Percentage();
			Assert.AreEqual("0%", percentage.ToString());

			percentage = new Percentage(50);
			Assert.AreEqual("50%", percentage.ToString());

			percentage = new Percentage(200.0);
			Assert.AreEqual("200%", percentage.ToString());

			percentage = new Percentage(-25);
			Assert.AreEqual("-25%", percentage.ToString());
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_IEquatable()
		{
			Percentage first = new Percentage(50.0);
			Percentage second = new Percentage(50);

			Assert.IsTrue(first == second);
			Assert.IsTrue(first.Equals(second));
			Assert.IsTrue(first.Equals((object)second));
		}
		//===========================================================================================
	}
	//==============================================================================================
}
