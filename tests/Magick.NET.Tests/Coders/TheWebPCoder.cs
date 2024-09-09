// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class TheWebPCoder
{
    private readonly byte[] _xmpData = Encoding.UTF8.GetBytes(@"
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""XMPTk 2.8"">
    <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
         <rdf:Description rdf:about='uuid:9de4a6be-3c42-11db-b201-a36d0bcc6e1f' xmlns:tiff='http://ns.adobe.com/tiff/1.0/'>
            <tiff:Orientation>4</tiff:Orientation>
            <tiff:XResolution>350/1</tiff:XResolution>
            <tiff:YResolution>350/1</tiff:YResolution>
            <tiff:ResolutionUnit>0</tiff:ResolutionUnit>
         </rdf:Description>
    </rdf:RDF>
</x:xmpmeta>");

    [Fact]
    public void ShouldReadAndWriteTheXmpProfile()
    {
        using var input = new MagickImage(Files.Builtin.Logo);
        input.SetProfile(new XmpProfile(_xmpData));

        var data = input.ToByteArray(MagickFormat.WebP);
        using var output = new MagickImage(data);

        var profile = output.GetXmpProfile();
        Assert.NotNull(profile);

        var expectedProfile = @"
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""XMPTk 2.8"">
    <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
         <rdf:Description rdf:about='uuid:9de4a6be-3c42-11db-b201-a36d0bcc6e1f' xmlns:tiff='http://ns.adobe.com/tiff/1.0/'>
            <tiff:Orientation>0</tiff:Orientation>
            <tiff:XResolution>0/1</tiff:XResolution>
            <tiff:YResolution>0/1</tiff:YResolution>
            <tiff:ResolutionUnit>1</tiff:ResolutionUnit>
         </rdf:Description>
    </rdf:RDF>
</x:xmpmeta>";
        Assert.Equal(expectedProfile, Encoding.UTF8.GetString(profile.ToByteArray()));
    }

    [Fact]
    public void ShouldUseTheUpdatedXmpProfile()
    {
        using var input = new MagickImage(Files.Builtin.Logo);
        input.SetProfile(new XmpProfile(_xmpData));

        input.Density = new Density(1234.5678, 5, DensityUnit.PixelsPerCentimeter);
        input.Orientation = OrientationType.LeftBottom;

        var data = input.ToByteArray(MagickFormat.WebP);
        using var output = new MagickImage(data);

        var profile = output.GetXmpProfile();

        Assert.NotNull(profile);
        Assert.Equal(1234.5678, output.Density.X);
        Assert.Equal(5, output.Density.Y);
        Assert.Equal(DensityUnit.PixelsPerCentimeter, output.Density.Units);
        Assert.Equal(OrientationType.LeftBottom, output.Orientation);

        var expectedProfile = @"
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""XMPTk 2.8"">
    <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
         <rdf:Description rdf:about='uuid:9de4a6be-3c42-11db-b201-a36d0bcc6e1f' xmlns:tiff='http://ns.adobe.com/tiff/1.0/'>
            <tiff:Orientation>8</tiff:Orientation>
            <tiff:XResolution>6172839/5000</tiff:XResolution>
            <tiff:YResolution>5/1</tiff:YResolution>
            <tiff:ResolutionUnit>3</tiff:ResolutionUnit>
         </rdf:Description>
    </rdf:RDF>
</x:xmpmeta>";
        Assert.Equal(expectedProfile, Encoding.UTF8.GetString(profile.ToByteArray()));
    }
}
