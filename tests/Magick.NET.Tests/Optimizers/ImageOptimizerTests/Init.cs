// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests
{
    public partial class ImageOptimizerTests
    {
        private static MemoryStream OpenStream(string fileName)
        {
            var memoryStream = new MemoryStream();
            using (var input = FileHelper.OpenRead(fileName))
            {
                input.CopyTo(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}
