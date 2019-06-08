# Using colors

```C#
using (MagickImage image = new MagickImage("Snakeware.png"))
{
    image.TransparentChroma(Color.Black, Color.Blue);
    image.BackgroundColor = new ColorMono(true);

    // Q16 (Blue):
    image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, Quantum.Max));
    image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, 0, Quantum.Max));
    image.BackgroundColor = new MagickColor("#00f");
    image.BackgroundColor = new MagickColor("#0000ff");
    image.BackgroundColor = new MagickColor("#00000000ffff");

    // With transparency (Red):
    image.BackgroundColor = new MagickColor(0, 0, Quantum.Max, 0);
    image.BackgroundColor = new MagickColor("#0000ff80");

    // Q8 (Green):
    image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, Quantum.Max, 0));
    image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, Quantum.Max, 0));
    image.BackgroundColor = new MagickColor("#0f0");
    image.BackgroundColor = new MagickColor("#00ff00");
}
```