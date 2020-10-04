# Drawing

## Draw text

```C#
using (var image = new MagickImage(new MagickColor("#ff00ff"), 512, 128))
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

## Adding Text To Existing Image

![Example of adding text to existing image](./img/example_addTextToExistingImage.jpg)

```C#
var pathToBackgroundImage = "path/to/background.png";
var pathToNewImage = "path/to/newImage.png";
var textToWrite = "Insert This Text Into Image";

// These settings will create a new caption
// which automatically resizes the text to best
// fit within the box.

var readSettings = new MagickReadSettings
{
    Font = "Calibri",
    TextGravity = Gravity.Center,
    BackgroundColor = MagickColors.Transparent,
    Height = 250, // height of text box
    Width = 680 // width of text box
};

using (var image = new MagickImage(pathToBackgroundImage))
{
    using (var caption = new MagickImage($"caption:{textToWrite}", readSettings))
    {
        // Add the caption layer on top of the background image
        // at position 590,450
        image.Composite(caption, 590, 450, CompositeOperator.Over);

        image.Write(pathToNewImage);
    }
}
```
