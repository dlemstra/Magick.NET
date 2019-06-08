# Exif data

## Read exif data

#### C#
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

#### VB.NET
```VB.NET
' Read image from file
Using image As New MagickImage("FujiFilmFinePixS1Pro.jpg")
    ' Retrieve the exif information
    Dim profile As ExifProfile = image.GetExifProfile()

    ' Check if image contains an exif profile
    If profile Is Nothing Then
        Console.WriteLine("Image does not contain exif information.")
    Else
        ' Write all values to the console
        For Each value As ExifValue In profile.Values
            Console.WriteLine("{0}({1}): {2}", value.Tag, value.DataType, value.ToString())
        Next
    End If
End Using
```

## Create thumbnail from exif data

#### C#
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

#### VB.NET
```VB.NET
' Read image from file
Using image As New MagickImage("FujiFilmFinePixS1Pro.jpg")
    ' Retrieve the exif information
    Dim profile As ExifProfile = image.GetExifProfile()

    ' Create thumbnail from exif information
    Using thumbnail As MagickImage = profile.CreateThumbnail()
        ' Check if exif profile contains thumbnail and save it
        If thumbnail IsNot Nothing Then
            thumbnail.Write("FujiFilmFinePixS1Pro.thumb.jpg")
        End If
    End Using
End Using
```