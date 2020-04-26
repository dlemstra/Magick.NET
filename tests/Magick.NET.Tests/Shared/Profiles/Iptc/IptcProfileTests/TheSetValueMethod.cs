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
using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        [TestClass]
        public class TheSetValueMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenEncodingIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();

                    ExceptionAssert.Throws<ArgumentNullException>("encoding", () =>
                    {
                        profile.SetValue(IptcTag.Title, null, string.Empty);
                    });
                }
            }

            [TestMethod]
            public void ShouldChangeTheValue()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.Title);

                    profile.SetValue(IptcTag.Title, "Magick.NET Title");

                    Assert.AreEqual("Magick.NET Title", value.Value);

                    value = profile.GetValue(IptcTag.Title);

                    Assert.AreEqual("Magick.NET Title", value.Value);
                }
            }

            [TestMethod]
            public void ShouldAddValueThatDoesNotExist()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    var value = profile.GetValue(IptcTag.ReferenceNumber);

                    Assert.IsNull(value);

                    profile.SetValue(IptcTag.Title, "Magick.NET ReferenceNümber");

                    value = profile.GetValue(IptcTag.Title);

                    Assert.AreEqual("Magick.NET ReferenceNümber", value.Value);
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
            public void ShouldAllowDuplicateValuesForValuesThatCanBeRepated(IptcTag tag)
            {
                var profile = new IptcProfile();
                var expectedValue1 = "test";
                var expectedValue2 = "another one";

                profile.SetValue(tag, expectedValue1);
                profile.SetValue(tag, expectedValue2);

                var values = profile.Values.ToList();
                Assert.AreEqual(2, values.Count);
                Assert.IsTrue(values.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue1))));
                Assert.IsTrue(values.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue2))));
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
            public void ShoulNotdAllowDuplicateValuesForValuesThatCannotBeRepated(IptcTag tag)
            {
                var profile = new IptcProfile();
                var expectedValue = "another one";

                profile.SetValue(tag, "test");
                profile.SetValue(tag, expectedValue);

                var values = profile.Values.ToList();
                Assert.AreEqual(1, values.Count);
                Assert.IsTrue(values.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue))));
            }

            [TestMethod]
            [DataRow(IptcTag.DigitalCreationDate)]
            [DataRow(IptcTag.ExpirationDate)]
            [DataRow(IptcTag.CreatedDate)]
            [DataRow(IptcTag.ReferenceDate)]
            [DataRow(IptcTag.ReleaseDate)]
            public void ShouldFormatTheDate(IptcTag tag)
            {
                var profile = new IptcProfile();
                var datetime = new DateTimeOffset(new DateTime(1994, 3, 17));

                profile.SetValue(tag, datetime);

                var actual = profile.GetValue(tag);
                Assert.AreEqual("19940317", actual.Value);
            }

            [TestMethod]
            [DataRow(IptcTag.CreatedTime)]
            [DataRow(IptcTag.DigitalCreationTime)]
            [DataRow(IptcTag.ExpirationTime)]
            [DataRow(IptcTag.ReleaseTime)]
            public void ShouldFormatTheTime(IptcTag tag)
            {
                var profile = new IptcProfile();
                var dateTimeUtc = new DateTime(1994, 3, 17, 14, 15, 16, DateTimeKind.Utc);
                var dateTimeOffset = new DateTimeOffset(dateTimeUtc).ToOffset(TimeSpan.FromHours(2));

                profile.SetValue(tag, dateTimeOffset);

                var actual = profile.GetAllValues(tag).First();
                Assert.AreEqual("161516+0200", actual.Value);
            }
        }
    }
}
