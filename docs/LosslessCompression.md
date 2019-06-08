# Lossless Compression

## Lossless compress JPEG logo

```C#
FileInfo snakewareLogo = new FileInfo("Snakeware.jpg");

Console.WriteLine("Bytes before: " + snakewareLogo.Length);

ImageOptimizer optimizer = new ImageOptimizer();
optimizer.LosslessCompress(snakewareLogo);

snakewareLogo.Refresh();
Console.WriteLine("Bytes after:  " + snakewareLogo.Length);
```