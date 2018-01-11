﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        private static readonly string _Root = GetRoot();

        public static string CirclePNG
        {
            get
            {
                return _Root + @"Images/Circle.png";
            }
        }

        public static string CMYKJPG
        {
            get
            {
                return _Root + @"Images/CMYK.jpg";
            }
        }

        public static string CorruptPNG
        {
            get
            {
                return _Root + @"Images/Corrupt.png";
            }
        }

        public static string ConnectedComponentsPNG
        {
            get
            {
                return _Root + @"Images/ConnectedComponents.png";
            }
        }

        public static string EightBimTIF
        {
            get
            {
                return _Root + @"Images/8Bim.tif";
            }
        }

        public static string FujiFilmFinePixS1ProGIF
        {
            get
            {
                return _Root + @"Images/FujiFilmFinePixS1Pro.gif";
            }
        }

        public static string FujiFilmFinePixS1ProJPG
        {
            get
            {
                return _Root + @"Images/FujiFilmFinePixS1Pro.jpg";
            }
        }

        public static string ExifUndefType
        {
            get
            {
                return _Root + @"Images/ExifUndefType.jpg";
            }
        }

        public static string FujiFilmFinePixS1ProPNG
        {
            get
            {
                return _Root + @"Images/FujiFilmFinePixS1Pro.png";
            }
        }

        public static string ImageMagickJPG
        {
            get
            {
                return _Root + @"Images/ImageMagick.jpg";
            }
        }

        public static string InvitationTif
        {
            get
            {
                return _Root + @"Images/Invitation.tif";
            }
        }

        public static string ImageMagickTXT
        {
            get
            {
                return _Root + @"Images/ImageMagick.txt";
            }
        }

        public static string LetterJPG
        {
            get
            {
                return _Root + @"Images/Letter.jpg";
            }
        }

        public static string MagickNETIconPNG
        {
            get
            {
                return _Root + @"Images/Magick.NET.icon.png";
            }
        }

        public static string Missing
        {
            get
            {
                return @"/Foo/Bar.png";
            }
        }

        public static string NoisePNG
        {
            get
            {
                return _Root + @"Images/Noise.png";
            }
        }

        public static string PictureJPG
        {
            get
            {
                return _Root + @"Images/Picture.jpg";
            }
        }

        public static string RedPNG
        {
            get
            {
                return _Root + @"Images/Red.png";
            }
        }

        public static string RoseSparkleGIF
        {
            get
            {
                return _Root + @"Images/RöseSparkle.gif";
            }
        }

        public static string Root
        {
            get
            {
                return _Root;
            }
        }

        public static string SnakewarePNG
        {
            get
            {
                return _Root + @"Images/Snakeware.png";
            }
        }

        public static string VicelandPNG
        {
            get
            {
                return _Root + "Images/Viceland.png";
            }
        }

        public static string WireframeTIF
        {
            get
            {
                return _Root + @"Images/wireframe.tif";
            }
        }

        public static string TestPNG
        {
            get
            {
                return _Root + @"Images/Test.png";
            }
        }

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
                string directory = Path.GetFullPath(path);
                if (Directory.Exists(directory + "Images"))
                    return directory;
            }

            throw new InvalidOperationException("Unable to find the images folder, current directory is: " + Path.GetFullPath("."));
        }

        public static class Builtin
        {
            public static string Logo
            {
                get
                {
                    return "logo:";
                }
            }

            public static string Rose
            {
                get
                {
                    return "rose:";
                }
            }

            public static string Wizard
            {
                get
                {
                    return "wizard:";
                }
            }
        }

        public static class Coders
        {
            public static string CartoonNetworkStudiosLogoAI
            {
                get
                {
                    return _Root + @"Images/Coders/CN Studios Logo.ai";
                }
            }

            public static string GrimJp2
            {
                get
                {
                    return _Root + @"Images/Coders/grim.jp2";
                }
            }

            public static string IgnoreTagTIF
            {
                get
                {
                    return _Root + @"Images/Coders/IgnoreTag.tif";
                }
            }

            public static string LayerStylesSamplePSD
            {
                get
                {
                    return _Root + @"Images/Coders/layer-styles-sample.psd";
                }
            }

            public static string PageTIF
            {
                get
                {
                    return _Root + @"Images/Coders/Page.tif";
                }
            }

            public static string PlayerPSD
            {
                get
                {
                    return _Root + @"Images/Coders/Player.psd";
                }
            }

            public static string TestDds
            {
                get
                {
                    return _Root + @"Images/Coders/Test.dds";
                }
            }
        }

        public static class Logos
        {
            public static string MagickNETSVG
            {
                get
                {
                    return _Root + @"../../Logo/Magick.NET.svg";
                }
            }
        }

        public static class Patterns
        {
            public static string Checkerboard
            {
                get
                {
                    return "pattern:checkerboard";
                }
            }
        }

        public static class Scripts
        {
            public static string Collection
            {
                get
                {
                    return _Root + @"Shared/Script/Collection.msl";
                }
            }

            public static string Draw
            {
                get
                {
                    return _Root + @"Shared/Script/Draw.msl";
                }
            }

            public static string Defines
            {
                get
                {
                    return _Root + @"Shared/Script/Defines.msl";
                }
            }

            public static string Distort
            {
                get
                {
                    return _Root + @"Shared/Script/Distort.msl";
                }
            }

            public static string Events
            {
                get
                {
                    return _Root + @"Shared/Script/Events.msl";
                }
            }

            public static string ImageProfile
            {
                get
                {
                    return _Root + @"Shared/Script/ImageProfile.msl";
                }
            }

            public static string Invalid
            {
                get
                {
                    return _Root + @"Framework/Script/Invalid.msl";
                }
            }

            public static string Resize
            {
                get
                {
                    return _Root + @"Shared/Script/Resize.msl";
                }
            }

            public static string Variables
            {
                get
                {
                    return _Root + @"Shared/Script/Variables.msl";
                }
            }
        }
    }
}