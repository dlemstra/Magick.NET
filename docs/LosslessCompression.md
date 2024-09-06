# Lossless Compression

## Lossless compress JPEG logo

```C#
var snakewareLogo = new FileInfo(@"c:\path\to\Snakeware.jpg");

Console.WriteLine("Bytes before: " + snakewareLogo.Length);

var optimizer = new ImageOptimizer();
optimizer.LosslessCompress(snakewareLogo);

snakewareLogo.Refresh();
Console.WriteLine("Bytes after:  " + snakewareLogo.Length);
```
