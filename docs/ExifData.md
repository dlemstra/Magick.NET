# Exif data

## Read exif data

```C#
// Read image from file
using (MagickImage image = new MagickImage("FujiFilmFinePixS1Pro.jpg"))
{
    // Retrieve the exif information
    ExifProfile profile = image.GetExifProfile();

    // Check if image contains an exif profile
    if (profile == null)
        Console.WriteLine("Image does not contain exif information.");
    else
    {
        // Write all values to the console
        foreach (ExifValue value in profile.Values)
        {
            Console.WriteLine("{0}({1}): {2}", value.Tag, value.DataType, value.ToString());
        }
    }
}
```

## Create thumbnail from exif data

```C#
// Read image from file
using (MagickImage image = new MagickImage("FujiFilmFinePixS1Pro.jpg"))
{
    // Retrieve the exif information
    ExifProfile profile = image.GetExifProfile();

    // Create thumbnail from exif information
    using (MagickImage thumbnail = profile.CreateThumbnail())
    {
        // Check if exif profile contains thumbnail and save it
        if (thumbnail != null)
            thumbnail.Write("FujiFilmFinePixS1Pro.thumb.jpg");
    }
}
```