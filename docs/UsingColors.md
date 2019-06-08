# Using colors

#### C#
```C#
using (MagickImage image = new MagickImage("Snakeware.png"))
{
    image.TransparentChroma(Color.Black, Color.Blue);
    image.BackgroundColor = new ColorMono(true);

    // Q16 (Blue):
    image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 0, 65535));
    image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, 0, 65535));
    image.BackgroundColor = new MagickColor("#00f");
    image.BackgroundColor = new MagickColor("#0000ff");
    image.BackgroundColor = new MagickColor("#00000000ffff");

    // With transparency (Red):
    image.BackgroundColor = new MagickColor(65535, 0, 0, 32767);
    image.BackgroundColor = new MagickColor("#ff000080");

    // Q8 (Green):
    image.TransparentChroma(new MagickColor(0, 0, 0), new MagickColor(0, 255, 0));
    image.TransparentChroma(new ColorRGB(0, 0, 0), new ColorRGB(0, 255, 0));
    image.BackgroundColor = new MagickColor("#0f0");
    image.BackgroundColor = new MagickColor("#00ff00");
}
```

#### VB.NET
```VB.NET
Using image As New MagickImage("Snakeware.png")
    image.TransparentChroma(Color.Black, Color.Blue)
    image.BackgroundColor = New ColorMono(True)

    ' Q16 (Blue):
    image.TransparentChroma(New MagickColor(0, 0, 0), New MagickColor(0, 0, 65535))
    image.TransparentChroma(New ColorRGB(0, 0, 0), New ColorRGB(0, 0, 65535))
    image.BackgroundColor = New MagickColor("#00f")
    image.BackgroundColor = New MagickColor("#0000ff")
    image.BackgroundColor = New MagickColor("#00000000ffff")

    ' With transparency (Red):
    image.BackgroundColor = New MagickColor(65535, 0, 0, 32767)
    image.BackgroundColor = New MagickColor("#ff000080")

    ' Q8 (Green):
    image.TransparentChroma(New MagickColor(0, 0, 0), New MagickColor(0, 255, 0))
    image.TransparentChroma(New ColorRGB(0, 0, 0), New ColorRGB(0, 255, 0))
    image.BackgroundColor = New MagickColor("#0f0")
    image.BackgroundColor = New MagickColor("#00ff00")
End Using
```