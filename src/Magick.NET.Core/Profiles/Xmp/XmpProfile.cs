// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ImageMagick;

/// <summary>
/// Class that contains an XMP profile.
/// </summary>
public sealed class XmpProfile : ImageProfile, IXmpProfile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XmpProfile"/> class.
    /// </summary>
    /// <param name="data">A byte array containing the profile.</param>
    public XmpProfile(byte[] data)
      : base("xmp", CheckTrailingNULL(data))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmpProfile"/> class.
    /// </summary>
    /// <param name="document">A document containing the profile.</param>
    public XmpProfile(IXPathNavigable document)
      : base("xmp")
    {
        Throw.IfNull(nameof(document), document);

        using var memStream = new MemoryStream();
        using var writer = XmlWriter.Create(memStream);
        document.CreateNavigator().WriteSubtree(writer);
        writer.Flush();
        SetData(memStream.ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmpProfile"/> class.
    /// </summary>
    /// <param name="document">A document containing the profile.</param>
    public XmpProfile(XDocument document)
      : base("xmp")
    {
        Throw.IfNull(nameof(document), document);

        using var memStream = new MemoryStream();
        using var writer = XmlWriter.Create(memStream);
        document.WriteTo(writer);
        writer.Flush();
        SetData(memStream.ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmpProfile"/> class.
    /// </summary>
    /// <param name="stream">A stream containing the profile.</param>
    public XmpProfile(Stream stream)
      : base("xmp", stream)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmpProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public XmpProfile(string fileName)
      : base("xmp", fileName)
    {
    }

    /// <summary>
    /// Creates an instance from the specified IXPathNavigable.
    /// </summary>
    /// <param name="document">A document containing the profile.</param>
    /// <returns>A <see cref="XmpProfile"/>.</returns>
    public static XmpProfile FromIXPathNavigable(IXPathNavigable document)
        => new XmpProfile(document);

    /// <summary>
    /// Creates an instance from the specified IXPathNavigable.
    /// </summary>
    /// <param name="document">A document containing the profile.</param>
    /// <returns>A <see cref="XmpProfile"/>.</returns>
    public static XmpProfile FromXDocument(XDocument document)
        => new XmpProfile(document);

    /// <summary>
    /// Creates a XmlReader that can be used to read the data of the profile.
    /// </summary>
    /// <returns>A <see cref="XmlReader"/>.</returns>
    public XmlReader? CreateReader()
    {
        var data = GetDataProtected();
        if (data is null)
            return null;

        var memStream = new MemoryStream(data, 0, data.Length);
        var settings = XmlHelper.CreateReaderSettings();
        settings.CloseInput = true;

        return XmlReader.Create(memStream, settings);
    }

    /// <summary>
    /// Converts this instance to an IXPathNavigable.
    /// </summary>
    /// <returns>A <see cref="IXPathNavigable"/>.</returns>
    public IXPathNavigable ToIXPathNavigable()
    {
        using var reader = CreateReader();
        var result = XmlHelper.CreateDocument();
        result.Load(reader);
        return result.CreateNavigator();
    }

    /// <summary>
    /// Converts this instance to a XDocument.
    /// </summary>
    /// <returns>A <see cref="XDocument"/>.</returns>
    public XDocument ToXDocument()
    {
        using var reader = CreateReader();
        return XDocument.Load(reader);
    }

    private static byte[] CheckTrailingNULL(byte[] data)
    {
        Throw.IfNull(nameof(data), data);

        var length = data.Length;

        while (length > 2)
        {
            if (data[length - 1] != '\0')
                break;

            length--;
        }

        if (length == data.Length)
            return data;

        var result = new byte[length];
        Buffer.BlockCopy(data, 0, result, 0, length);
        return result;
    }
}
