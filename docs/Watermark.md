# Watermark

#### C#
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

#### VB.NET
```VB.NET
' Read image that needs a watermark
Using image As New MagickImage("Snakeware.jpg")
    ' Read the watermark that will be put on top of the image
    Using watermark As New MagickImage("Magick.NET.png")
        ' Draw the watermark in the bottom right corner
        image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over)

        ' Optionally make the watermark more transparent
        watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4)

        ' Or draw the watermark at a specific location
        image.Composite(watermark, 200, 50, CompositeOperator.Over)
    End Using

    ' Save the result
    image.Write("Snakeware.watermark.jpg")
End Using
```