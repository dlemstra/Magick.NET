# Magick.NET.Web

Magick.NET.Web is an extra library for Magick.NET that contains an IHttpModule that uses various IHttphandlers to script/optimize/compress images.
This page describes how to configure and use this module.

## Configure the module in your web.config

Below is an example of how the MagickModule can be configured.
```xml
<configuration>
    <configSections>
        <section name="magick.net.web" type="ImageMagick.Web.MagickWebSettings" requirePermission="false" />
    </configSections>
    <magick.net.web cacheDirectory="c:\ImageCache\">
        <urlResolvers>
            <urlResolver type="YourLibrary.UrlResolver, YourLibrary" />
        </urlResolvers>
    </magick.net.web>
    <system.webServer>
        <modules>
            <add name="MagickModule" type="ImageMagick.Web.MagickModule"/>
        </modules>
    </system.webServer>
    <!-- or -->
    <system.web>
        <httpModules>
            <add name="MagickModule" type="ImageMagick.Web.MagickModule"/>
        </httpModules>
    </system.web>
</configuration>
```

The MagickModule uses url resolvers to handle each request. At least one urlResolvers should be configured. The configured class needs to implement
the `IFileUrlResolver` or the `IStreamUrlResolver` interface. The next section will describe how to implement your own `IFileUrlResolver` and the last
section will describe all the possible configuration options in detail.

## Create your own IFileUrlResolver

This interface contains the following members:

```C#
public interface IUrlResolver
{
    MagickFormat Format { get; }

    bool Resolve(Uri url);
}

public interface IFileUrlResolver : IUrlResolver
{
    string FileName { get; }
}
```

Each request will create a new instance of the configured `IFileUrlResolver` class. The `Resolve` method will be called for each request and should
return true if the specified `Url` could be resolved. If the method returns false the request will not be handled by the` MagickModule`. The `FileName`
property should contain the full path to the image that should be send to the `Response`. The `Format` property specifies the format of the image.
You can extend the url resolver by implementing the `IScriptData` interface:

```C#
public interface IScriptData
{
    MagickFormat OutputFormat { get; }

    IXPathNavigable Script { get; }
}
```

The `Script` property can be used to set the `MagickScript` that should be executed before the image is send to the response. The script can be used
to resize the image. This is optional, so it is okay to return null for this property. The resulting image of the script will be saved in the format
of the `OutputFormat` property, this can be different from the input format.

## Advanced configuration

Advanced configuration
```xml
<magick.net.web cacheDirectory="c:\ImageCache\" canCreateDirectories="true" enableGzip="true" showVersion="false" tempDirectory="" useOpenCL="false">
    <optimization enabled=""true"" lossless=""true"" optimalCompression=""false""/>
    <clientCache cacheControlMode="UseMaxAge" cacheControlMaxAge="1.00:00:00"/>
    <resourceLimits width="10000" height="10000"/>
    <urlResolvers>
        <urlResolver type="YourLibrary.UrlResolver, YourLibrary" />
    </urlResolvers>
</magick.net.web>
```
- `cacheDirectory`: The directory that contains the optimized/compressed or scripted images.
- `canCreateDirectories`: When set to false the cache and temp directory won't be created when they don't exist.
- `enableGzip`: When set to true the output file will compressed with gzip. This only happens when the output format is SVG.
- `showVersion`: Will add the version of Magick.NET as an http header when set to true.
- `tempDirectory`: The directory that can be used to create temporary files, the default value is Path.GetTempDirectory().
- `useOpenCL`: This option can be used to enable OpenCL acceleration. This is disabled by default at the moment. 
- `optimization`: These settings can be used to controle image optimization.
  - `enabled`: When set to true the ImageOptimizer class of Magick.NET will be used to compress the image before sending it to the output. This will also
     be done for images that were created with a Script
  - `lossless`: If this setting is set to true only lossless compression will be allowed.
  - `optimalCompression`: When set to true the ImageOptimizer will try more options to find the best compression.
- `clientCache`: These settings can be used to control the cache settings for the client.
  - `cacheControlMode`: Options are NoControl and UseMaxAge. The latter will add a MaxAge header to the response and sets the cache-ability to Public.
  - `cacheControlMaxAge`: Specifies the HTTP 1.1 cache control maximum age value.
- `resourceLimits`: These settings can be used to limit the resources being used (the default is unlimited).
  - `width`: The maximum width of an image.
  - `height`: The maximum height of an image.
- `urlResolvers`: A list of IUrlResolvers that can handle requests. When multiple urlResolvers are specified the request will be handled by the first
  instance that returns true when the Resolve method is called. The order from the web.config will be used.
  - `urlResolver`: The type name of a class that implements the IFileUrlResolver or IStreamUrlResolver interface.