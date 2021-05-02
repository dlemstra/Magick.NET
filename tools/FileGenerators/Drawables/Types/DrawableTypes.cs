// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FileGenerator.Drawables
{
    public sealed class DrawableTypes : MagickTypes
    {
        private XDocument _comments;

        public DrawableTypes()
          : base()
        {
            _comments = XDocument.Load(AssemblyFile.Replace(".dll", ".xml"));
        }

        public IEnumerable<string> GetCommentLines(ConstructorInfo constructor, string className)
        {
            string memberName = "M:" + constructor.DeclaringType!.FullName + ".#" + constructor.ToString()!.Substring(6);
            memberName = memberName.Replace("()", string.Empty);
            memberName = memberName.Replace(", ", ",");
            memberName = memberName.Replace("Boolean", "System.Boolean");
            memberName = memberName.Replace("Double", "System.Double");
            memberName = memberName.Replace("Int32", "System.Int32");
            if (memberName.Contains("`1["))
            {
                memberName = memberName.Replace("`1[", "{");
                memberName = memberName.Replace("])", "})");
            }

            var comment = _comments.XPathSelectElement("/doc/members/member[@name='" + memberName + "']");
            if (comment == null)
                throw new NotImplementedException(memberName);

            ModifyComment(comment, constructor.DeclaringType.Name, className);

            foreach (var node in comment.Nodes())
            {
                foreach (string line in node.ToString().Split('\n'))
                    yield return "/// " + line.Trim();
            }
        }

        public IEnumerable<ConstructorInfo[]> GetDrawables()
            => GetInterfaceConstructors("IDrawable");

        public IEnumerable<ConstructorInfo[]> GetPaths()
            => GetInterfaceConstructors("IPath");

        private static void ModifyComment(XElement comment, string typeName, string className)
        {
            var cref = new XElement("see", new XAttribute("cref", className));

            var summary = comment.Element("summary");
            if (summary == null)
                throw new InvalidOperationException();

            summary.RemoveAll();
            summary.Add(
              new XText(Environment.NewLine),
              new XText("Applies the "),
              new XText(typeName),
              new XText($" operation to the "),
              cref,
              new XText("."),
              new XText(Environment.NewLine));

            comment.Add(
              new XElement("returns",
                new XText("The "),
                cref,
                new XText(" instance.")));
        }

        private IEnumerable<ConstructorInfo[]> GetInterfaceConstructors(string interfaceName)
        {
            return from type in GetInterfaceTypes(interfaceName)
                   let constructors = GetTypeConstructors(type.Name).ToArray()
                   where constructors.Length > 0
                   orderby type.Name
                   select constructors;
        }

        private IEnumerable<ConstructorInfo> GetTypeConstructors(string typeName)
        {
            return from type in GetTypes()
                   where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
                   let constructors = type.GetConstructors()
                   from constructor in constructors
                   orderby constructor.GetParameters().Count()
                   select constructor;
        }
    }
}
