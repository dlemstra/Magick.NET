// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheCompositeMethod
    {
        public class WithImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!));
            }

            [Fact]
            public void ShouldCompositeTheImage()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var other = new MagickImage("xc:purple", 1, 1);
                image.Composite(other);

                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldPreserveGrayColorSpace()
            {
                using var logo = new MagickImage(Files.Builtin.Logo);
                using var blue = logo.Separate(Channels.Blue).First();
                blue.Composite(logo, CompositeOperator.Modulate);

                Assert.Equal(ColorSpace.Gray, blue.ColorSpace);
            }
        }

        public class WithImageAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Channels.Red));
            }

            [Fact]
            public void ShouldOnlyCompositeTheSpecifiedChannel()
            {
                using var image = new MagickImage("xc:black", 1, 1);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Channels.Green);

                ColorAssert.Equal(MagickColors.Lime, image, 0, 0);
            }
        }

        public class WithImageAndCompositeOperator
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, CompositeOperator.CopyCyan));
            }

            [Fact]
            public void ShouldAddTransparencyWithCopyAlpha()
            {
                using var image = new MagickImage(MagickColors.Red, 2, 1);
                using var alpha = new MagickImage(MagickColors.Black, 1, 1);
                alpha.BackgroundColor = MagickColors.White;
                alpha.Extent(2, 1, Gravity.East);

                image.Composite(alpha, CompositeOperator.CopyAlpha);

                Assert.True(image.HasAlpha);
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
                ColorAssert.Equal(new MagickColor("#f000"), image, 1, 0);
            }

            [Fact]
            public void ShouldCopyTheAlphaChannelWithCopyAlpha()
            {
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.None,
                    FillColor = MagickColors.White,
                    FontPointsize = 100,
                    Font = Files.Fonts.Arial,
                };

                using var image = new MagickImage("label:Test", settings);
                using var alpha = image.Clone();
                alpha.Alpha(AlphaOption.Extract);
                alpha.ShadeGrayscale(130, 30);
                alpha.Composite(image, CompositeOperator.CopyAlpha);

                ColorAssert.Equal(new MagickColor("#7fff7fff7fff0000"), alpha, 0, 0);
                ColorAssert.Equal(new MagickColor("#7fff7fff7fffffff"), alpha, 30, 30);
            }
        }

        public class WithImageAndCompositeOperatorAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, CompositeOperator.CopyCyan, Channels.Red));
            }

            [Fact]
            public void ShouldOnlyCompositeTheSpecifiedChannel()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                using var red = new MagickImage(MagickColors.Red, image.Width, image.Height);
                image.Composite(red, CompositeOperator.Multiply, Channels.Blue);

                ColorAssert.Equal(MagickColors.Yellow, image, 0, 0);
            }
        }

        public class WithImageAndCompositeOperatorAndArguments
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, CompositeOperator.CopyCyan, "3"));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage(MagickColors.Red, image.Width, image.Height);
                image.Composite(red, CompositeOperator.CopyCyan, null);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, CompositeOperator.Blur, "3");
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, CompositeOperator.Blur, "3");

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndCompositeOperatorAndArgumentsAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, CompositeOperator.CopyCyan, "3", Channels.Red));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage(MagickColors.Red, image.Width, image.Height);
                image.Composite(red, CompositeOperator.CopyCyan, null, Channels.Red);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, CompositeOperator.Blur, "3", Channels.Red);
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, CompositeOperator.Blur, "3", Channels.Red);

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndXY
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0));
            }

            [Fact]
            public void ShouldUseTheOffset()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                using var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100);
                image.Composite(yellow, 100, 100);

                ColorAssert.Equal(MagickColors.Yellow, image, 150, 150);
                ColorAssert.Equal(MagickColors.Yellow, image, 199, 109);
                ColorAssert.Equal(MagickColors.White, image, 200, 200);
            }
        }

        public class WithImageAndXYAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0, Channels.Red));
            }

            [Fact]
            public void ShouldUseTheOffset()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                using var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100);
                image.Composite(yellow, 100, 100, Channels.Red);

                ColorAssert.Equal(MagickColors.White, image, 150, 150);
                ColorAssert.Equal(MagickColors.White, image, 199, 109);
                ColorAssert.Equal(MagickColors.White, image, 200, 200);
            }
        }

        public class WithImageAndXYAndCompositeOperator
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0, CompositeOperator.CopyAlpha));
            }

            [Fact]
            public void ShouldUseTheOffset()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                using var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100);
                image.Composite(yellow, 100, 100, CompositeOperator.Copy);

                ColorAssert.Equal(MagickColors.Yellow, image, 150, 150);
                ColorAssert.Equal(MagickColors.Yellow, image, 199, 109);
                ColorAssert.Equal(MagickColors.White, image, 200, 200);
            }
        }

        public class WithImageAndXYAndCompositeOperatorAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0, CompositeOperator.CopyAlpha, Channels.Red));
            }

            [Fact]
            public void ShouldUseTheOffset()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                using var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100);
                image.Composite(yellow, 100, 100, CompositeOperator.Clear, Channels.Red);

                ColorAssert.Equal(MagickColors.Aqua, image, 150, 150);
                ColorAssert.Equal(MagickColors.Aqua, image, 199, 109);
                ColorAssert.Equal(MagickColors.White, image, 200, 200);
            }
        }

        public class WithImageAndXYAndCompositeOperatorAndArguments
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0, CompositeOperator.CopyAlpha, "3"));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);

                image.Composite(red, 0, 0, CompositeOperator.CopyAlpha, null);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);

                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, 0, 0, CompositeOperator.Blur, "3");
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, 0, 0, CompositeOperator.Blur, "3");

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndXYAndCompositeOperatorAndArgumentsAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, 0, 0, CompositeOperator.CopyAlpha, "3", Channels.Red));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);
                image.Composite(red, 0, 0, CompositeOperator.CopyAlpha, null, Channels.Red);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);

                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, 0, 0, CompositeOperator.Blur, "3", Channels.Red);
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, 0, 0, CompositeOperator.Blur, "3", Channels.Red);

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndGravity
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:red", 3, 3);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Gravity.East);

                ColorAssert.Equal(MagickColors.White, image, 2, 1);
            }
        }

        public class WithImageAndGravityAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:red", 3, 3);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Gravity.West, Channels.Green);

                ColorAssert.Equal(MagickColors.Yellow, image, 0, 1);
            }
        }

        public class WithImageAndGravityAndCompositeOperator
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, CompositeOperator.Blend));
            }

            [Fact]
            public void ShouldSetMaskWithChangeMask()
            {
                using var background = new MagickImage("xc:red", 100, 100);
                background.BackgroundColor = MagickColors.White;
                background.Extent(200, 100);

                var drawables = new IDrawable[]
                {
                        new DrawableFontPointSize(50),
                        new DrawableText(135, 70, "X"),
                };

                using var image = background.Clone();
                image.Draw(drawables);
                image.Composite(background, Gravity.Center, CompositeOperator.ChangeMask);

                using var result = new MagickImage(MagickColors.Transparent, 200, 100);
                result.Draw(drawables);

                Assert.InRange(result.Compare(image, ErrorMetric.RootMeanSquared), 0.0603, 0.0604);
            }

            [Fact]
            public void ShouldDrawAtTheCorrectPositionWithWestGravity()
            {
                var backgroundColor = MagickColors.LightBlue;
                var overlayColor = MagickColors.YellowGreen;

                using var background = new MagickImage(backgroundColor, 100, 100);
                using var overlay = new MagickImage(overlayColor, 50, 50);
                background.Composite(overlay, Gravity.West, CompositeOperator.Over);

                ColorAssert.Equal(backgroundColor, background, 0, 0);
                ColorAssert.Equal(overlayColor, background, 0, 25);
                ColorAssert.Equal(backgroundColor, background, 0, 75);

                ColorAssert.Equal(backgroundColor, background, 49, 0);
                ColorAssert.Equal(overlayColor, background, 49, 25);
                ColorAssert.Equal(backgroundColor, background, 49, 75);

                ColorAssert.Equal(backgroundColor, background, 50, 0);
                ColorAssert.Equal(backgroundColor, background, 50, 25);
                ColorAssert.Equal(backgroundColor, background, 50, 75);

                ColorAssert.Equal(backgroundColor, background, 99, 0);
                ColorAssert.Equal(backgroundColor, background, 99, 25);
                ColorAssert.Equal(backgroundColor, background, 99, 75);
            }

            [Fact]
            public void ShouldDrawAtTheCorrectPositionWithEastGravity()
            {
                var backgroundColor = MagickColors.LightBlue;
                var overlayColor = MagickColors.YellowGreen;

                using var background = new MagickImage(backgroundColor, 100, 100);
                using var overlay = new MagickImage(overlayColor, 50, 50);
                background.Composite(overlay, Gravity.East, CompositeOperator.Over);

                ColorAssert.Equal(backgroundColor, background, 0, 0);
                ColorAssert.Equal(backgroundColor, background, 0, 50);
                ColorAssert.Equal(backgroundColor, background, 0, 75);

                ColorAssert.Equal(backgroundColor, background, 49, 0);
                ColorAssert.Equal(backgroundColor, background, 49, 25);
                ColorAssert.Equal(backgroundColor, background, 49, 75);

                ColorAssert.Equal(backgroundColor, background, 50, 0);
                ColorAssert.Equal(overlayColor, background, 50, 25);
                ColorAssert.Equal(backgroundColor, background, 50, 75);

                ColorAssert.Equal(backgroundColor, background, 99, 0);
                ColorAssert.Equal(overlayColor, background, 99, 25);
                ColorAssert.Equal(backgroundColor, background, 99, 75);
            }
        }

        public class WithImageAndGravityAndCompositeOperatorAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, CompositeOperator.Blend, Channels.Red));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:white", 3, 3);
                using var other = new MagickImage("xc:black", 1, 1);
                image.Composite(other, Gravity.South, CompositeOperator.Clear, Channels.Green);

                ColorAssert.Equal(MagickColors.Magenta, image, 1, 2);
            }
        }

        public class WithImageAndGravityAndCompositeOperatorAndArguments
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, CompositeOperator.Blend, "3"));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);
                image.Composite(red, Gravity.East, CompositeOperator.Blend, null);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndGravityAndCompositeOperatorAndArgumentsAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, CompositeOperator.Blend, "3", Channels.Red));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);

                image.Composite(red, Gravity.East, CompositeOperator.Blend, null, Channels.Red);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3", Channels.Red);
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3", Channels.Red);

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndGravityAndXY
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, 0, 0));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:red", 3, 3);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Gravity.Northeast, 1, 1);

                ColorAssert.Equal(MagickColors.White, image, 1, 1);
            }
        }

        public class WithImageAndGravityAndXYAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.West, 0, 0, Channels.Green));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:red", 3, 3);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Gravity.Southwest, 1, 1, Channels.Green);

                ColorAssert.Equal(MagickColors.Yellow, image, 1, 1);
            }
        }

        public class WithImageAndGravityAndXYAndCompositeOperator
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, 0, 0, CompositeOperator.Blend));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:red", 3, 3);
                using var other = new MagickImage("xc:white", 1, 1);
                image.Composite(other, Gravity.Northwest, 1, 1, CompositeOperator.Over);

                ColorAssert.Equal(MagickColors.White, image, 1, 1);
            }
        }

        public class WithImageAndGravityAndXYAndCompositeOperatorAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, 0, 0, CompositeOperator.Blend, Channels.Red));
            }

            [Fact]
            public void ShouldUseTheGravity()
            {
                using var image = new MagickImage("xc:white", 3, 3);
                using var other = new MagickImage("xc:black", 1, 1);
                image.Composite(other, Gravity.Southeast, 1, 1, CompositeOperator.Clear, Channels.Green);

                ColorAssert.Equal(MagickColors.Magenta, image, 1, 1);
            }
        }

        public class WithImageAndGravityAndXYAndCompositeOperatorAndArguments
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, 0, 0, CompositeOperator.Blend, "3"));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);
                image.Composite(red, Gravity.East, 0, 0, CompositeOperator.Blend, null);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, Gravity.Center, 1, 1, CompositeOperator.Blur, "3");
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3");

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }

        public class WithImageAndGravityAndXYAndCompositeOperatorAndArgumentsAndChannels
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);

                Assert.Throws<ArgumentNullException>("image", () => image.Composite(null!, Gravity.East, 0, 0, CompositeOperator.Blend, "3", Channels.Red));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenArgumentIsNull()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var red = new MagickImage("xc:white", 1, 1);
                image.Composite(red, Gravity.East, 0, 0, CompositeOperator.Blend, null, Channels.Red);
            }

            [Fact]
            public void ShouldUseTheArguments()
            {
                using var image = new MagickImage(MagickColors.Red, 10, 10);
                using var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height);
                image.Warning += (object? sender, WarningEventArgs arguments) => throw new XunitException(arguments.Message);
                image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3", Channels.Red);
            }

            [Fact]
            public void ShouldRemoveTheArtifact()
            {
                using var image = new MagickImage("xc:red", 1, 1);
                using var blur = new MagickImage("xc:white", 1, 1);
                image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3", Channels.Red);

                Assert.Null(image.GetArtifact("compose:args"));
            }
        }
    }
}
