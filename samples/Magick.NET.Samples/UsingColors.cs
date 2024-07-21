// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;

namespace Magick.NET.Samples;

public static class UsingColorsSamples
{
    public static void UsingColors()
    {
        using var image = new MagickImage(SampleFiles.SnakewarePng);

        image.TransparentChroma(MagickColors.Black, MagickColors.Blue);
        image.BackgroundColor = ColorMono.Black.ToMagickColor();

        // Q16 (Blue):
        image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, Quantum.Max));
        image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, 0, Quantum.Max).ToMagickColor());
        image.BackgroundColor = new MagickColor("#00f");
        image.BackgroundColor = new MagickColor("#0000ff");
        image.BackgroundColor = new MagickColor("#00000000ffff");

        // With transparency (Red):
        image.BackgroundColor = new MagickColor(0, 0, Quantum.Max, 0);
        image.BackgroundColor = new MagickColor("#0000ff80");

        // Q8 (Green):
        image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, Quantum.Max, 0));
        image.TransparentChroma(new ColorRGB(0, 0, 0).ToMagickColor(), new ColorRGB(0, Quantum.Max, 0).ToMagickColor());
        image.BackgroundColor = new MagickColor("#0f0");
        image.BackgroundColor = new MagickColor("#00ff00");
    }
}
