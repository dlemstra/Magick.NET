# Read raw thumbnail

## Read thumbnail from raw image

```C#
// Setup DNG read defines
var defines = new DngReadDefines
{
    ReadThumbnail = true
};

// Create empty image
using var image = new MagickImage();

// Copy the defines to the settings
image.Settings.SetDefines(defines);

// Read only meta data of the image
image.Ping(SampleFiles.StillLifeCR2);

// Get thumbnail data
var thumbnailData = image.GetProfile("dng:thumbnail")?.ToByteArray();

if (thumbnailData != null)
{
    // Read the thumbnail image
    using var thumbnail = new MagickImage(thumbnailData);
}
```
