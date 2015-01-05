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

using System.Diagnostics;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class ResourceLimitsTests
	{
		//===========================================================================================
		private const string _Category = "ResourceLimits";
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Values()
		{
			Assert.AreEqual(ulong.MaxValue, ResourceLimits.Disk);
			Assert.IsTrue(ResourceLimits.Memory > uint.MaxValue);
			Assert.AreEqual(10000000U, ResourceLimits.Height);
			Assert.IsTrue(ResourceLimits.Thread > 1U);
			Assert.AreEqual(0U, ResourceLimits.Throttle);
			Assert.AreEqual(10000000U, ResourceLimits.Width);

			ResourceLimits.Disk = 400U;
			Assert.AreEqual(400U, ResourceLimits.Disk);
			ResourceLimits.Disk = ulong.MaxValue;

			ResourceLimits.Height = 1000U;
			Assert.AreEqual(1000U, ResourceLimits.Height);
			ResourceLimits.Height = 10000000U;

			ResourceLimits.Memory = 858U;
			Assert.AreEqual(858U, ResourceLimits.Memory);
			ResourceLimits.Memory = 8585838592U;

			ResourceLimits.Thread = 1U;
			Assert.AreEqual(1U, ResourceLimits.Thread);
			ResourceLimits.Thread = 4U;

			ResourceLimits.Throttle = 6U;
			Assert.AreEqual(6U, ResourceLimits.Throttle);
			ResourceLimits.Throttle = 0U;

			ResourceLimits.Width = 100U;
			Assert.AreEqual(100U, ResourceLimits.Width);
			ResourceLimits.Width = 10000000U;
		}
		//===========================================================================================
	}
	//==============================================================================================
}
