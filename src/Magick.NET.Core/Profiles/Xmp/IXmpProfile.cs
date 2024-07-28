// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ImageMagick;

/// <summary>
/// Class that contains an XMP profile.
/// </summary>
public interface IXmpProfile : IImageProfile
{
    /// <summary>
    /// Creates a XmlReader that can be used to read the data of the profile.
    /// </summary>
    /// <returns>A <see cref="XmlReader"/>.</returns>
    XmlReader? CreateReader();

    /// <summary>
    /// Converts this instance to an IXPathNavigable.
    /// </summary>
    /// <returns>A <see cref="IXPathNavigable"/>.</returns>
    IXPathNavigable? ToIXPathNavigable();

    /// <summary>
    /// Converts this instance to a XDocument.
    /// </summary>
    /// <returns>A <see cref="XDocument"/>.</returns>
    XDocument? ToXDocument();
}
