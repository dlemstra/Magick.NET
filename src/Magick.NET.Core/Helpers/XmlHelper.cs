// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;
using System.Xml;

namespace ImageMagick;

internal static partial class XmlHelper
{
    public static XmlDocument CreateDocument()
        => new XmlDocument
        {
            XmlResolver = null,
        };

    public static XmlElement CreateElement(XmlNode node, string name)
    {
        var doc = node.GetType() == typeof(XmlDocument) ? (XmlDocument)node : node.OwnerDocument!;
        var element = doc.CreateElement(name);
        node.AppendChild(element);
        return element;
    }

    public static XmlReaderSettings CreateReaderSettings()
        => new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Ignore,
            XmlResolver = null,
        };

    public static void SetAttribute<TType>(XmlElement element, string name, TType value)
    {
        XmlAttribute attribute;
        if (element.HasAttribute(name))
            attribute = element.Attributes[name]!;
        else
            attribute = element.Attributes.Append(element.OwnerDocument.CreateAttribute(name));

        if (typeof(TType) == typeof(string))
            attribute.Value = (string?)(object?)value;
        else
            attribute.Value = (string?)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
    }
}
