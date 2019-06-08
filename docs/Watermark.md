# Watermark

```C#
// Read image that needs a watermark
using (MagickImage image = new MagickImage("Snakeware.jpg"))
{
    // Read the watermark that will be put on top of the image
    using (MagickImage watermark = new MagickImage("Magick.NET.png"))
    {
        // Draw the watermark in the bottom right corner
        image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);

        // Optionally make the watermark more transparent
        watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);

        // Or draw the watermark at a specific location
        image.Composite(watermark, 200, 50, CompositeOperator.Over);
    }

    // Save the result
    image.Write("Snakeware.watermark.jpg");
}
```