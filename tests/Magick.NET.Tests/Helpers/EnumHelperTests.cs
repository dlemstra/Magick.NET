// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class EnumHelperTests
    {
        [Fact]
        public void Test_Parse()
        {
            ExifDataType? dataType = EnumHelper.Parse(4, ExifDataType.Undefined);
            Assert.Equal(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse(42, ExifDataType.Long);
            Assert.Equal(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse(string.Empty, ExifDataType.Byte);
            Assert.Equal(ExifDataType.Byte, dataType);

            dataType = EnumHelper.Parse("Long", ExifDataType.Undefined);
            Assert.Equal(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse("Longer", ExifDataType.Short);
            Assert.Equal(ExifDataType.Short, dataType);

            dataType = EnumHelper.Parse<ExifDataType>(string.Empty);
            Assert.Null(dataType);

            dataType = EnumHelper.Parse<ExifDataType>("Long");
            Assert.Equal(ExifDataType.Long, dataType);

            dataType = EnumHelper.Parse<ExifDataType>("Longer");
            Assert.Null(dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), string.Empty);
            Assert.Null(dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), "Long");
            Assert.Equal(ExifDataType.Long, dataType);

            dataType = (ExifDataType?)EnumHelper.Parse(typeof(ExifDataType), "Longer");
            Assert.Null(dataType);
        }
    }
}
