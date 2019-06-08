# Exception handling

## Exception handling

#### C#
```C#
try
{
    // Read invalid jpg file
    using (MagickImage image = new MagickImage("InvalidFile.jpg"))
    {
    }
}
// Catch any MagickException
catch (MagickException exception)
{
    // Write excepion raised when reading the invalid jpg to the console
    Console.WriteLine(exception.Message);
}

try
{
    // Read corrupt jpg file
    using (MagickImage image = new MagickImage("CorruptImage.jpg"))
    {
    }
}
// Catch only MagickCorruptImageErrorException
catch (MagickCorruptImageErrorException exception)
{
    // Write excepion raised when reading the corrupt jpg to the console
    Console.WriteLine(exception.Message);
}
```

#### VB.NET
```VB.NET
Try
    ' Read invalid jpg file
    Using image As New MagickImage("InvalidFile.jpg")
    End Using
' Catch any MagickException
Catch exception As MagickException
    ' Write excepion raised when reading the invalid jpg to the console
    Console.WriteLine(exception.Message)
End Try

Try
    ' Read corrupt jpg file
    Using image As New MagickImage("CorruptImage.jpg")
    End Using
' Catch only MagickCorruptImageErrorException
Catch exception As MagickCorruptImageErrorException
    ' Write excepion raised when reading the corrupt jpg to the console
    Console.WriteLine(exception.Message)
End Try
```

## Obtain warning that occurred during reading

#### C#
```C#
using (MagickImage image = new MagickImage())
{
    // Attach event handler to warning event
    image.Warning += MagickImage_Warning;
    // Read file that will raise a warning.
    image.Read("FileWithWarning.jpg");
}
```

#### VB.NET
```VB.NET
Using image As New MagickImage()
    ' Attach event handler to warning event
    AddHandler image.Warning, AddressOf MagickImage_Warning
    ' Read file that will raise a warning.
    image.Read("FileWithWarning.jpg")
End Using
```