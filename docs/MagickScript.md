# MagickScript

## What is MagickScript?

MagickScript is a class that uses an xml file to perform multiple operations on an image. When a script is loaded it will be validated
with the MagickScript.xsd file. With thexsi:noNamespaceSchemaLocation attribute you will get code completion in Visual Studio. Below is
an example script that resizes an image to half its size.

#### Resize.msl
```xml
<?xml version="1.0" encoding="utf-8" ?>
<msl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="MagickScript.xsd">
    <read fileName="snakeware.jpg">
        <resize percentage="50"/>
        <write fileName="snakeware.resized.jpg"/>
    </read>
</msl>
```

```C#
// Load resize script and execute it
MagickScript script = new MagickScript("Resize.msl");
script.Execute();
```

## Reuse the same script

When you want to execute the same script with multiple images you should not specify the `fileName` attribute in the `read` element.
You should provide the image as the first argument of the `Execute` method. And because you probably don't want to write to the same
file every time you should not specify the write element. Below is an example.

#### Wave.msl
```xml
<?xml version="1.0" encoding="utf-8" ?>
<msl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="MagickScript.xsd">
    <read>
      <wave/>
    </read>
</msl>
```

```C#
// Load wave script
var script = new MagickScript("Wave.msl");

// Execute script multiple times
foreach (var fileName in Directory.GetFiles("*.jpg"))
{
    // Read image from file
    using (var image = new MagickImage(fileName))
    {
        // Execute script with the image and write it to a jpg file
        script.Execute(image);
        image.Write(fileName + ".wave.jpg");
    }
}
```

## Read/Write events

When you want to assign your input image dynamically you should attach to the `Read` event. You can also do the same for writing
with the `Write` event. Below is an example of how this work.

#### Crop.msl
```xml
<msl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="MagickScript.xsd">
    <read>
        <crop width="100" height="100"/>
        <write/>
    </read>
</msl>
```

```C#
void OnScriptRead(object sender, ScriptReadEventArgs arguments)
{
    arguments.Image = new MagickImage("Snakeware.jpg");
}

void OnScriptWrite(object sender, ScriptWriteEventArgs arguments)
{
    arguments.Image.Write("Snakeware.png");
}

void ExecuteCropScript()
{
    // Load crop script
    var script = new MagickScript("Crop.msl");
    // Event that will be raised when the script wants to read a file
    script.Read += OnScriptRead;
    // Event that will be raised when the script wants to write a file
    script.Write += OnScriptWrite;
    // Execute the script
    script.Execute();
}
```

## Write multiple output files

With the `clone` element you can create a clone of an image while you are executing a script. Below is an example
that reads one input file and writes two output files.

#### Clone.msl
```xml
<msl xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="MagickScript.xsd">
    <read fileName="Snakeware.jpg">
        <clone>
            <crop width="100" height="100"/>
            <write fileName="Snakeware.cropped.jpg"/>
        </clone>
        <!-- When you specify zero for the height it will be calculated.-->
        <resize width="300" height="0"/>
        <write fileName="Snakeware.resized.jpg"/>
    </read>
</msl>
```

```C#
// Load clone script and execute it
var script = new MagickScript("Clone.msl");
script.Execute();
```