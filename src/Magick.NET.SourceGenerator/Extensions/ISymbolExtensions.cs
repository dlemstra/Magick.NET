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
        if (xml is not null)
        {
            var element = XElement.Parse(xml);
            var summary = element.XPathSelectElement("/summary");
            if (summary is not null)
            {
                return summary.Value
                    .Split('\n')
                    .Select(value => value.Trim().Replace("&", "&amp;"))
                    .Where(value => value.Length > 0)
                    .ToArray();
            }
        }

        return Array.Empty<string>();
    }
}
