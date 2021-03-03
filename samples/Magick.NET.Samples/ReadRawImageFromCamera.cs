// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;

namespace Magick.NET.Samples
{
    /// <summary>
    /// You need to put the executable dcraw.exe into the directory that contains the Magick.NET dll.
    /// The zip file ImageMagick-6.X.X-X-Q16-x86-windows.zip that you can download from
    /// http://www.imagemagick.org/script/binary-releases.php#windows contains this file.
    /// </summary>
    public static class ReadRawImageFromCameraSamples
    {
        public static void ConvertCR2ToJPG()
        {
            using (var image = new MagickImage(SampleFiles.StillLifeCR2))
            {
                image.Write(SampleFiles.OutputDirectory + "StillLife.jpg");
            }
        }
    }
}
