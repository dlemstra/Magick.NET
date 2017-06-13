//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

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
    private XDocument _Comments;

    private static void ModifyComment(XElement comment, string typeName, string className)
    {
      var cref = new XElement("see", new XAttribute("cref", className));

      var summary = comment.Element("summary");
      summary.RemoveAll();
      summary.Add(
        new XText(Environment.NewLine),
        new XText("Adds a new instance of the "),
        new XElement("see",
          new XAttribute("cref", typeName)
        ),
        new XText($" class to the "),
        cref,
        new XText("."),
        new XText(Environment.NewLine)
      );

      comment.Add(
        new XElement("returns",
          new XText("The "),
          cref,
          new XText(" instance.")
        )
      );
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

    private void LoadComments()
    {
      _Comments = XDocument.Load(AssemblyFile.Replace(".dll", ".xml"));
    }

    public DrawableTypes(QuantumDepth depth)
      : base(depth)
    {
      LoadComments();
    }

    public IEnumerable<string> GetCommentLines(ConstructorInfo constructor, string className)
    {
      string memberName = "M:" + constructor.DeclaringType.FullName + ".#" + constructor.ToString().Substring(6);
      memberName = memberName.Replace("()", "");
      memberName = memberName.Replace(", ", ",");
      memberName = memberName.Replace("Boolean", "System.Boolean");
      memberName = memberName.Replace("Double", "System.Double");
      memberName = memberName.Replace("Int32", "System.Int32");
      if (memberName.Contains("IEnumerable"))
      {
        memberName = memberName.Replace("`1[", "{");
        memberName = memberName.Replace("])", "})");
      }

      var comment = _Comments.XPathSelectElement("/doc/members/member[@name='" + memberName + "']");
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
    {
      return GetInterfaceConstructors("IDrawable");
    }

    public IEnumerable<ConstructorInfo[]> GetPaths()
    {
      return GetInterfaceConstructors("IPath");
    }
  }
}
