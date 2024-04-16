// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.CodeAnalysis;

namespace ImageMagick;

internal static class ISymbolExtensions
{
    public static string[] GetDocumentation(this ISymbol symbol)
    {
        var xml = symbol.GetDocumentationCommentXml();
        if (xml is null || xml.Length == 0)
            throw new InvalidOperationException($"Missing comment xml for: {symbol.Name}");

        var element = XElement.Parse(xml);
        var summary = element.XPathSelectElement("/summary");
        if (summary is null)
            throw new InvalidOperationException($"Missing summary for: {symbol.Name}");

        return summary.Value
            .Split('\n')
            .Select(value => value.Trim().Replace("&", "&amp;"))
            .Where(value => value.Length > 0)
            .ToArray();
    }
}
