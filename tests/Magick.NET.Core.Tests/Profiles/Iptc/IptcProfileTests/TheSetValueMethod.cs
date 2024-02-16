// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class IptcProfileTests
{
    public class TheSetValueMethod
    {
        [Theory]
        [InlineData(IptcTag.ObjectAttribute)]
        [InlineData(IptcTag.SubjectReference)]
        [InlineData(IptcTag.SupplementalCategories)]
        [InlineData(IptcTag.Keyword)]
        [InlineData(IptcTag.LocationCode)]
        [InlineData(IptcTag.LocationName)]
        [InlineData(IptcTag.ReferenceService)]
        [InlineData(IptcTag.ReferenceDate)]
        [InlineData(IptcTag.ReferenceNumber)]
        [InlineData(IptcTag.Byline)]
        [InlineData(IptcTag.BylineTitle)]
        [InlineData(IptcTag.Contact)]
        [InlineData(IptcTag.LocalCaption)]
        [InlineData(IptcTag.CaptionWriter)]
        public void ShouldAllowDuplicateValuesForValuesThatCanBeRepeated(IptcTag tag)
        {
            var profile = new IptcProfile();
            var expectedValue1 = "test";
            var expectedValue2 = "another one";

            profile.SetValue(tag, expectedValue1);
            profile.SetValue(tag, expectedValue2);

            var values = profile.Values.ToList();
            Assert.Equal(2, values.Count);
            Assert.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue1)), values);
            Assert.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue2)), values);
        }

        [Theory]
        [InlineData(IptcTag.RecordVersion)]
        [InlineData(IptcTag.ObjectType)]
        [InlineData(IptcTag.Title)]
        [InlineData(IptcTag.EditStatus)]
        [InlineData(IptcTag.EditorialUpdate)]
        [InlineData(IptcTag.Priority)]
        [InlineData(IptcTag.Category)]
        [InlineData(IptcTag.FixtureIdentifier)]
        [InlineData(IptcTag.ReleaseDate)]
        [InlineData(IptcTag.ReleaseTime)]
        [InlineData(IptcTag.ExpirationDate)]
        [InlineData(IptcTag.ExpirationTime)]
        [InlineData(IptcTag.SpecialInstructions)]
        [InlineData(IptcTag.ActionAdvised)]
        [InlineData(IptcTag.CreatedDate)]
        [InlineData(IptcTag.CreatedTime)]
        [InlineData(IptcTag.DigitalCreationDate)]
        [InlineData(IptcTag.DigitalCreationTime)]
        [InlineData(IptcTag.OriginatingProgram)]
        [InlineData(IptcTag.ProgramVersion)]
        [InlineData(IptcTag.ObjectCycle)]
        [InlineData(IptcTag.City)]
        [InlineData(IptcTag.SubLocation)]
        [InlineData(IptcTag.ProvinceState)]
        [InlineData(IptcTag.CountryCode)]
        [InlineData(IptcTag.Country)]
        [InlineData(IptcTag.OriginalTransmissionReference)]
        [InlineData(IptcTag.Headline)]
        [InlineData(IptcTag.Credit)]
        [InlineData(IptcTag.CopyrightNotice)]
        [InlineData(IptcTag.Caption)]
        [InlineData(IptcTag.ImageType)]
        [InlineData(IptcTag.ImageOrientation)]
        public void ShouldNotAllowDuplicateValuesForValuesThatCannotBeRepeated(IptcTag tag)
        {
            var profile = new IptcProfile();
            var expectedValue = "another one";

            profile.SetValue(tag, "test");
            profile.SetValue(tag, expectedValue);

            var values = profile.Values.ToList();
            Assert.Single(values);
            Assert.Contains(new IptcValue(tag, Encoding.UTF8.GetBytes(expectedValue)), values);
        }

        [Fact]
        public void ShouldThrowExceptionWhenTagIsNotDateOrTime()
        {
            var profile = new IptcProfile();
            var datetime = new DateTimeOffset(new DateTime(1994, 3, 17));

            Assert.Throws<ArgumentException>("tag", () =>
            {
                profile.SetValue(IptcTag.ActionAdvised, datetime);
            });
        }

        [Theory]
        [InlineData(IptcTag.DigitalCreationDate)]
        [InlineData(IptcTag.ExpirationDate)]
        [InlineData(IptcTag.CreatedDate)]
        [InlineData(IptcTag.ReferenceDate)]
        [InlineData(IptcTag.ReleaseDate)]
        public void ShouldFormatTheDate(IptcTag tag)
        {
            var profile = new IptcProfile();
            var datetime = new DateTimeOffset(new DateTime(1994, 3, 17));

            profile.SetValue(tag, datetime);

            var actual = profile.GetValue(tag);
            Assert.Equal("19940317", actual.Value);
            Assert.Equal(8, actual.Length);
        }

        [Theory]
        [InlineData(IptcTag.CreatedTime)]
        [InlineData(IptcTag.DigitalCreationTime)]
        [InlineData(IptcTag.ExpirationTime)]
        [InlineData(IptcTag.ReleaseTime)]
        public void ShouldFormatTheTime(IptcTag tag)
        {
            var profile = new IptcProfile();
            var dateTimeUtc = new DateTime(1994, 3, 17, 14, 15, 16, DateTimeKind.Utc);
            var dateTimeOffset = new DateTimeOffset(dateTimeUtc).ToOffset(TimeSpan.FromHours(2));

            profile.SetValue(tag, dateTimeOffset);

            var actual = profile.GetAllValues(tag).First();
            Assert.Equal("161516+0200", actual.Value);
        }
    }
}
