// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheConnectedComponentsMethod
    {
        [Fact]
        public void ShouldReturnTheConnectedComponents()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            using var temp = image.Clone();
            temp.Blur(0, 10);
            temp.Threshold((Percentage)50);

            var components = temp.ConnectedComponents(4).OrderBy(component => component.X).ToArray();
            Assert.Equal(7, components.Length);

            var color = MagickColors.Black;

#if Q8
            AssertComponent(image, components[1], 2, 94, 297, 128, 151, 11783, color, 157, 371);
            AssertComponent(image, components[2], 5, 99, 554, 128, 150, 11772, color, 162, 628);
            AssertComponent(image, components[3], 4, 267, 432, 89, 139, 11792, color, 310, 501);
            AssertComponent(image, components[4], 1, 301, 202, 148, 143, 11801, color, 374, 272);
            AssertComponent(image, components[5], 6, 341, 622, 136, 150, 11793, color, 408, 696);
            AssertComponent(image, components[6], 3, 434, 411, 88, 139, 11835, color, 477, 480);
#else
            AssertComponent(image, components[1], 2, 94, 297, 128, 151, 11737, color, 157, 371);
            AssertComponent(image, components[2], 5, 99, 554, 128, 150, 11734, color, 162, 628);
            AssertComponent(image, components[3], 4, 267, 432, 89, 139, 11749, color, 310, 501);
            AssertComponent(image, components[4], 1, 301, 202, 148, 143, 11755, color, 374, 272);
            AssertComponent(image, components[5], 6, 341, 622, 136, 150, 11746, color, 408, 696);
            AssertComponent(image, components[6], 3, 434, 411, 88, 139, 11793, color, 477, 480);
#endif
        }

        [Fact]
        public void ShouldUseTheSettings()
        {
#if !Q8
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            using var temp = image.Clone();
            var settings = new ConnectedComponentsSettings
            {
                Connectivity = 4,
                MeanColor = true,
                AreaThreshold = new Threshold(400),
            };

            var components = temp.ConnectedComponents(settings).OrderBy(component => component.X).ToArray();
            Assert.Equal(12, components.Length);

            var color = new MagickColor("#010101010101");

            AssertComponent(image, components[1], 597, 90, 293, 136, 162, 11624, color, 157, 372);
            AssertComponent(image, components[2], 3439, 96, 550, 138, 162, 11739, color, 162, 628);
            AssertComponent(image, components[3], 4122, 103, 604, 4, 2, 4, new MagickColor("#0B0B0B0B0B0B"), 104, 606);
            AssertComponent(image, components[4], 4157, 107, 612, 3, 1, 4, new MagickColor("#080808080808"), 108, 613);
            AssertComponent(image, components[5], 4233, 111, 620, 3, 1, 4, new MagickColor("#020202020202"), 112, 621);
            AssertComponent(image, components[6], 5085, 150, 698, 3, 1, 4, new MagickColor("#424242424242"), 150, 698);
            AssertComponent(image, components[7], 5132, 152, 702, 3, 1, 4, new MagickColor("#262626262626"), 153, 703);
            AssertComponent(image, components[8], 2105, 268, 433, 89, 139, 11645, color, 311, 502);
            AssertComponent(image, components[9], 17, 298, 198, 155, 151, 11622, color, 375, 273);
            AssertComponent(image, components[10], 4202, 337, 618, 144, 158, 11675, color, 409, 696);
            AssertComponent(image, components[11], 1703, 435, 412, 87, 138, 11629, color, 478, 481);
#endif
        }

        private void AssertComponent(IMagickImage<QuantumType> image, IConnectedComponent<QuantumType> component, int id, int x, int y, uint width, uint height, int area, IMagickColor<QuantumType> color, int centroidX, int centroidY)
        {
            var delta = 2;

            Assert.Equal(id, component.Id);
            Assert.InRange(component.X, x - delta, x + delta);
            Assert.InRange(component.Y, y - delta, y + delta);
            Assert.InRange(component.Width, width - delta, width + delta);
            Assert.InRange(component.Height, height - delta, height + delta);
            Assert.InRange(area, component.Area - delta, component.Area + delta);
            ColorAssert.Equal(color, component.Color);
            Assert.InRange(component.Centroid.X, centroidX - delta, centroidX + delta);
            Assert.InRange(component.Centroid.Y, centroidY - delta, centroidY + delta);

            using var componentImage = image.Clone();
            componentImage.Crop(component.ToGeometry(10));

            Assert.InRange(componentImage.Width, width + 20 - delta, width + 20 + delta);
            Assert.InRange(componentImage.Height, height + 20 - delta, height + 20 + delta);
        }
    }
}
