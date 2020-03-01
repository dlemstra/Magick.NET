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
using System.IO;

namespace Magick.NET.Tests
{
    public static class Files
    {
        public static string CirclePNG => Root + @"Images/Circle.png";

        public static string CMYKJPG => Root + @"Images/CMYK.jpg";

        public static string CorruptPNG => Root + @"Images/Corrupt.png";

        public static string ConnectedComponentsPNG => Root + @"Images/ConnectedComponents.png";

        public static string EightBimTIF => Root + @"Images/8Bim.tif";

        public static string FujiFilmFinePixS1ProGIF => Root + @"Images/FujiFilmFinePixS1Pro.gif";

        public static string FujiFilmFinePixS1ProJPG => Root + @"Images/FujiFilmFinePixS1Pro.jpg";

        public static string ExifUndefTypeJPG => Root + @"Images/ExifUndefType.jpg";

        public static string FujiFilmFinePixS1ProPNG => Root + @"Images/FujiFilmFinePixS1Pro.png";

        public static string ImageMagickJPG => Root + @"Images/ImageMagick.jpg";

        public static string InvitationTIF => Root + @"Images/Invitation.tif";

        public static string ImageMagickTXT => Root + @"Images/ImageMagick.txt";

        public static string ImageMagickICO => Root + @"Images/ImageMagick.ico";

        public static string LetterJPG => Root + @"Images/Letter.jpg";

        public static string MagickNETIconPNG => Root + @"Images/Magick.NET.icon.png";

        public static string Missing => @"/Foo/Bar.png";

        public static string NoisePNG => Root + @"Images/Noise.png";

        public static string PictureJPG => Root + @"Images/Picture.jpg";

        public static string RedPNG => Root + @"Images/Red.png";

        public static string RoseSparkleGIF => Root + @"Images/RöseSparkle.gif";

        public static string Root { get; } = GetRoot();

        public static string SnakewarePNG => Root + @"Images/Snakeware.png";

        public static string VicelandPNG => Root + "Images/viceland.png";

        public static string WandICO => Root + @"Images/wand.ico";

        public static string WhiteJPG => Root + @"Images/white.jpg";

        public static string WireframeTIF => Root + @"Images/wireframe.tif";

        public static string TestPNG => Root + @"Images/Test.png";

        [ExcludeFromCodeCoverage]
        private static string GetRoot()
        {
            string[] paths =
            {
                @"../../../../",
                @"../../../../../Tests/Magick.NET.Tests/", // Code coverage
            };

            foreach (string path in paths)
            {
                string directory = Path.GetFullPath(path).Replace('\\', '/');
                if (Directory.Exists(directory + "Images"))
                    return directory;
            }

            throw new InvalidOperationException("Unable to find the images folder, current directory is: " + Path.GetFullPath("."));
        }

        public static class Builtin
        {
            public static string Logo => "logo:";

            public static string Rose => "rose:";

            public static string Wizard => "wizard:";
        }

        public static class Coders
        {
            public static string AnimatedPNGexampleBouncingBeachBallPNG => Root + "Images/Coders/Animated_PNG_example_bouncing_beach_ball.png";

            public static string CartoonNetworkStudiosLogoAI => Root + @"Images/Coders/CN Studios Logo.ai";

            public static string GrimJP2 => Root + @"Images/Coders/grim.jp2";

            public static string IgnoreTagTIF => Root + @"Images/Coders/IgnoreTag.tif";

            public static string LayerStylesSamplePSD => Root + @"Images/Coders/layer-styles-sample.psd";

            public static string PageTIF => Root + @"Images/Coders/Page.tif";

            public static string PdfExamplePasswordOriginalPDF => Root + @"Images/Coders/pdf-example-password.original.pdf";

            public static string PlayerPSD => Root + @"Images/Coders/Player.psd";

            public static string RowsPerStripTIF => Root + @"Images/Coders/RowsPerStrip.tif";

            public static string TestDDS => Root + @"Images/Coders/Test.dds";

            public static string TestMNG => Root + @"Images/Coders/Test.mng";
        }

        public static class Fonts
        {
            public static string Arial => Root + @"Fonts/arial.ttf";

            public static string CourierNew => Root + @"Fonts/cour.ttf";
        }

        public static class Logos
        {
            public static string MagickNETSVG => Root + @"../../logo/Magick.NET.svg";
        }

        public static class Patterns
        {
            public static string Checkerboard => "pattern:checkerboard";
        }

        public static class Scripts
        {
            public static string Collection => Root + @"Shared/Script/Collection.msl";

            public static string Draw => Root + @"Shared/Script/Draw.msl";

            public static string Defines => Root + @"Shared/Script/Defines.msl";

            public static string Distort => Root + @"Shared/Script/Distort.msl";

            public static string Events => Root + @"Shared/Script/Events.msl";

            public static string ImageProfile => Root + @"Shared/Script/ImageProfile.msl";

            public static string Invalid => Root + @"Framework/Script/Invalid.msl";

            public static string PixelReadSettings => Root + @"Shared/Script/PixelReadSettings.msl";

            public static string Resize => Root + @"Shared/Script/Resize.msl";

            public static string Variables => Root + @"Shared/Script/Variables.msl";
        }
    }
}