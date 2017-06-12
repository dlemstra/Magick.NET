//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Linq;
using System.Xml.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Magick.NET.Tests
{
    [TestClass]
    public class EightBimProfileTests
    {
        private static void TestProfileValues(EightBimProfile profile)
        {
            Assert.IsNotNull(profile);

            Assert.AreEqual(25, profile.Values.Count());

            EightBimValue firstValue = profile.Values.First();
            Assert.AreEqual(1061, firstValue.ID);

            byte[] bytes = new byte[16] { 154, 137, 173, 93, 40, 109, 186, 33, 2, 200, 203, 169, 103, 5, 63, 219 };
            CollectionAssert.AreEqual(bytes, firstValue.ToByteArray());

            foreach (EightBimValue value in profile.Values)
            {
                Assert.IsNotNull(value.ToByteArray());
            }
        }

        private static void TestProfile(EightBimProfile profile)
        {
            Assert.IsNotNull(profile);

            Assert.AreEqual(2, profile.ClipPaths.Count());

            ClipPath first = profile.ClipPaths.First();
            Assert.AreEqual("Path 1", first.Name);
            XDocument doc = XDocument.Load(first.Path.CreateNavigator().ReadSubtree());

            Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""M 45 58&#xA;L 80 124&#xA;L 147 147&#xA;L 45 147&#xA;L 45 58 Z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));

            ClipPath second = profile.ClipPaths.Skip(1).First();
            Assert.AreEqual("Path 2", second.Name);
            doc = XDocument.Load(second.Path.CreateNavigator().ReadSubtree());

            Assert.AreEqual(@"<svg width=""200"" height=""200""><g><path fill=""#00000000"" stroke=""#00000000"" stroke-width=""0"" stroke-antialiasing=""false"" d=""M 52 144&#xA;L 130 57&#xA;L 157 121&#xA;L 131 106&#xA;L 52 144 Z&#xA;"" /></g></svg>", doc.ToString(SaveOptions.DisableFormatting));
        }

        private static byte[] ToBytes(params object[] objects)
        {
            List<byte> bytes = new List<byte>();
            foreach (object obj in objects)
            {
                if (obj is byte)
                    bytes.Add((byte)obj);
                else if (obj is char)
                    bytes.Add((byte)(char)obj);
                else if (obj is short)
                    bytes.AddRange(BitConverter.GetBytes((short)obj).Reverse());
                else if (obj is int)
                    bytes.AddRange(BitConverter.GetBytes((int)obj).Reverse());
            }

            return bytes.ToArray();
        }

        [TestMethod]
        public void Test_ClippingPaths()
        {
            using (IMagickImage image = new MagickImage(Files.EightBimTIF))
            {
                EightBimProfile profile = image.Get8BimProfile();
                TestProfile(profile);

                using (IMagickImage emptyImage = new MagickImage(Files.EightBimTIF))
                {
                    emptyImage.Strip();
                    Assert.IsNull(emptyImage.GetIptcProfile());
                    emptyImage.AddProfile(profile);

                    profile = emptyImage.Get8BimProfile();
                    TestProfile(profile);
                }
            }
        }

        [TestMethod]
        public void Test_Values()
        {
            using (IMagickImage image = new MagickImage(Files.EightBimTIF))
            {
                EightBimProfile profile = image.Get8BimProfile();
                TestProfileValues(profile);

                using (IMagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
                {
                    Assert.IsNull(emptyImage.Get8BimProfile());
                    emptyImage.AddProfile(profile);

                    profile = emptyImage.Get8BimProfile();
                    TestProfileValues(profile);
                }
            }
        }

        [TestMethod]
        public void Test_CorruptProfile()
        {
            byte[] bytes = ToBytes('8', 'B', 'I', 'M', (short)42, (byte)0, 1);

            EightBimProfile profile = new EightBimProfile(bytes);
            Assert.AreEqual(0, profile.Values.Count());

            bytes = ToBytes('8', 'B', 'I', 'M', (short)42, (short)0, -1);

            profile = new EightBimProfile(bytes);
            Assert.AreEqual(0, profile.Values.Count());

            bytes = ToBytes('8', 'B', 'I', 'M', (short)2000, (short)0, 1, (byte)0);

            profile = new EightBimProfile(bytes);
            Assert.AreEqual(1, profile.Values.Count());
            Assert.AreEqual(0, profile.ClipPaths.Count());
        }
    }
}
