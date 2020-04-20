// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class IptcProfileTests
    {
        [TestMethod]
        public void Test_SetEncoding()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                TestProfileValues(profile);

                ExceptionAssert.Throws<ArgumentNullException>("encoding", () =>
                {
                    profile.SetEncoding(null);
                });

                profile.SetEncoding(Encoding.UTF8);
                Assert.AreEqual(Encoding.UTF8, profile.Values.First().Encoding);
            }
        }

        [TestMethod]
        public void Test_SetValue()
        {
            using (var memStream = new MemoryStream())
            {
                string credit = null;
                for (int i = 0; i < 255; i++)
                    credit += i.ToString() + ".";

                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    TestProfileValues(profile);

                    var value = profile.GetValue(IptcTag.Title);
                    TestValue(value, "Communications");

                    profile.SetValue(IptcTag.Title, "Magick.NET Title");
                    TestValue(value, "Magick.NET Title");

                    value = profile.GetValue(IptcTag.Title);
                    TestValue(value, "Magick.NET Title");

                    value = profile.Values.FirstOrDefault(val => val.Tag == IptcTag.ReferenceNumber);
                    Assert.IsNull(value);

                    profile.SetValue(IptcTag.ReferenceNumber, "Magick.NET ReferenceNümber");

                    value = profile.GetValue(IptcTag.ReferenceNumber);
                    TestValue(value, "Magick.NET ReferenceNümber");

                    profile.SetValue(IptcTag.Credit, credit);

                    value = profile.GetValue(IptcTag.Credit);
                    TestValue(value, credit);

                    // Remove the 8bim profile so we can overwrite the iptc profile.
                    image.RemoveProfile("8bim");
                    image.SetProfile(profile);

                    image.Write(memStream);
                    memStream.Position = 0;
                }

                using (IMagickImage image = new MagickImage(memStream))
                {
                    var profile = image.GetIptcProfile();
                    TestProfileValues(profile, 19);

                    var value = profile.GetValue(IptcTag.Title);
                    TestValue(value, "Magick.NET Title");

                    value = profile.GetValue(IptcTag.ReferenceNumber);
                    TestValue(value, "Magick.NET ReferenceNümber");

                    value = profile.GetValue(IptcTag.Credit);
                    TestValue(value, credit);

                    ExceptionAssert.Throws<ArgumentNullException>("encoding", () =>
                    {
                        profile.SetValue(IptcTag.Caption, null, "Test");
                    });

                    profile.SetValue(IptcTag.Caption, "Test");
                    value = profile.Values.ElementAt(1);
                    Assert.AreEqual("Test", value.Value);

                    profile.SetValue(IptcTag.Caption, Encoding.UTF32, "Test");
                    Assert.AreEqual(Encoding.UTF32, value.Encoding);
                    Assert.AreEqual("Test", value.Value);

                    Assert.IsTrue(profile.RemoveValue(IptcTag.Caption));
                    Assert.IsFalse(profile.RemoveValue(IptcTag.Caption));
                    Assert.IsNull(profile.GetValue(IptcTag.Caption));
                }
            }
        }

        [TestMethod]
        public void Test_Values()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.GetIptcProfile();
                TestProfileValues(profile);

                using (IMagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
                {
                    Assert.IsNull(emptyImage.GetIptcProfile());
                    emptyImage.SetProfile(profile);

                    profile = emptyImage.GetIptcProfile();
                    TestProfileValues(profile);
                }
            }
        }

        [TestMethod]
        [DataRow(IptcTag.ObjectAttribute)]
        [DataRow(IptcTag.SubjectReference)]
        [DataRow(IptcTag.SupplementalCategories)]
        [DataRow(IptcTag.Keyword)]
        [DataRow(IptcTag.LocationCode)]
        [DataRow(IptcTag.LocationName)]
        [DataRow(IptcTag.ReferenceService)]
        [DataRow(IptcTag.ReferenceDate)]
        [DataRow(IptcTag.ReferenceNumber)]
        [DataRow(IptcTag.Byline)]
        [DataRow(IptcTag.BylineTitle)]
        [DataRow(IptcTag.Contact)]
        [DataRow(IptcTag.LocalCaption)]
        [DataRow(IptcTag.CaptionWriter)]
        public void Test_AddRepeatable(IptcTag tag)
        {
            // arrange
            var profile = new IptcProfile();
            var expectedValue1 = "test";
            var expectedValue2 = "another one";
            profile.SetValue(tag, expectedValue1);

            // act
            profile.SetValue(tag, expectedValue2);

            // assert
            var values = profile.Values.ToList();
            Assert.AreEqual(2, values.Count);
            ContainsIptcValue(values, tag, expectedValue1);
            ContainsIptcValue(values, tag, expectedValue2);
        }

        [TestMethod]
        [DataRow(IptcTag.RecordVersion)]
        [DataRow(IptcTag.ObjectType)]
        [DataRow(IptcTag.Title)]
        [DataRow(IptcTag.EditStatus)]
        [DataRow(IptcTag.EditorialUpdate)]
        [DataRow(IptcTag.Priority)]
        [DataRow(IptcTag.Category)]
        [DataRow(IptcTag.FixtureIdentifier)]
        [DataRow(IptcTag.ReleaseDate)]
        [DataRow(IptcTag.ReleaseTime)]
        [DataRow(IptcTag.ExpirationDate)]
        [DataRow(IptcTag.ExpirationTime)]
        [DataRow(IptcTag.SpecialInstructions)]
        [DataRow(IptcTag.ActionAdvised)]
        [DataRow(IptcTag.CreatedDate)]
        [DataRow(IptcTag.CreatedTime)]
        [DataRow(IptcTag.DigitalCreationDate)]
        [DataRow(IptcTag.DigitalCreationTime)]
        [DataRow(IptcTag.OriginatingProgram)]
        [DataRow(IptcTag.ProgramVersion)]
        [DataRow(IptcTag.ObjectCycle)]
        [DataRow(IptcTag.City)]
        [DataRow(IptcTag.SubLocation)]
        [DataRow(IptcTag.ProvinceState)]
        [DataRow(IptcTag.CountryCode)]
        [DataRow(IptcTag.Country)]
        [DataRow(IptcTag.OriginalTransmissionReference)]
        [DataRow(IptcTag.Headline)]
        [DataRow(IptcTag.Credit)]
        [DataRow(IptcTag.CopyrightNotice)]
        [DataRow(IptcTag.Caption)]
        [DataRow(IptcTag.ImageType)]
        [DataRow(IptcTag.ImageOrientation)]
        public void Test_AddNoneRepeatable_DoesOverrideOldValue(IptcTag tag)
        {
            // arrange
            var profile = new IptcProfile();
            var expectedValue = "another one";
            profile.SetValue(tag, "test");

            // act
            profile.SetValue(tag, expectedValue);

            // assert
            var values = profile.Values.ToList();
            Assert.AreEqual(1, values.Count);
            ContainsIptcValue(values, tag, expectedValue);
        }

        [TestMethod]
        public void Test_RemoveByTag_RemovesAllEntries()
        {
            // arrange
            var profile = new IptcProfile();
            profile.SetValue(IptcTag.Byline, "test");
            profile.SetValue(IptcTag.Byline, "test2");

            // act
            var result = profile.RemoveValue(IptcTag.Byline);

            // assert
            Assert.IsTrue(result, "removed result should be true");
            Assert.AreEqual(0, profile.Values.Count());
        }

        [TestMethod]
        public void Test_RemoveByTagAndValue_Works()
        {
            // arrange
            var profile = new IptcProfile();
            profile.SetValue(IptcTag.Byline, "test");
            profile.SetValue(IptcTag.Byline, "test2");

            // act
            var result = profile.RemoveValue(IptcTag.Byline, "test2");

            // assert
            Assert.IsTrue(result, "removed result should be true");
            ContainsIptcValue(profile.Values.ToList(), IptcTag.Byline, "test");
        }

        [TestMethod]
        public void Test_GetValues_RetrievesAllEntries()
        {
            // arrange
            var profile = new IptcProfile();
            profile.SetValue(IptcTag.Byline, "test");
            profile.SetValue(IptcTag.Byline, "test2");
            profile.SetValue(IptcTag.Caption, "test");

            // act
            List<IptcValue> result = profile.GetValues(IptcTag.Byline);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        private static void ContainsIptcValue(List<IIptcValue> values, IptcTag tag, string value)
        {
            Assert.IsTrue(values.Any(val => val.Tag == tag), $"Missing iptc tag {tag}");
            Assert.IsTrue(values.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(value))),
                $"expected iptc value '{value}' was not found for tag '{tag}'");
        }

        private static void TestProfileValues(IIptcProfile profile) => TestProfileValues(profile, 18);

        private static void TestProfileValues(IIptcProfile profile, int count)
        {
            Assert.IsNotNull(profile);

            Assert.AreEqual(count, profile.Values.Count());

            foreach (IptcValue value in profile.Values)
            {
                Assert.IsNotNull(value.Value);
            }
        }

        private static void TestValue(IIptcValue value, string expected)
        {
            Assert.IsNotNull(value);
            Assert.AreEqual(expected, value.Value);
        }
    }
}
