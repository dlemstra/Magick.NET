# Drawing

## Draw text

```C#
using (MagickImage image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
{
    new Drawables()
      // Draw text on the image
      .FontPointSize(72)
      .Font("Comic Sans")
      .StrokeColor(new MagickColor("yellow"))
      .FillColor(MagickColors.Orange)
      .TextAlignment(TextAlignment.Center)
      .Text(256, 64, "Magick.NET")
      // Add an ellipse
      .StrokeColor(new MagickColor(0, Quantum.Max, 0))
      .FillColor(MagickColors.SaddleBrown)
      .Ellipse(256, 96, 192, 8, 0, 360)
      .Draw(image);
}
```