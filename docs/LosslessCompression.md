# Lossless Compression

## Lossless compress JPEG logo

#### C#
```C#
FileInfo snakewareLogo = new FileInfo("Snakeware.jpg");

Console.WriteLine("Bytes before: " + snakewareLogo.Length);

ImageOptimizer optimizer = new ImageOptimizer();
optimizer.LosslessCompress(snakewareLogo);

snakewareLogo.Refresh();
Console.WriteLine("Bytes after:  " + snakewareLogo.Length);
```

#### VB.NET
```VB.NET
Dim snakewareLogo As New FileInfo("Snakeware.jpg")

Console.WriteLine("Bytes before: " + snakewareLogo.Length)

Dim optimizer As New ImageOptimizer()
optimizer.LosslessCompress(snakewareLogo)

snakewareLogo.Refresh()
Console.WriteLine("Bytes after:  " + snakewareLogo.Length)
```