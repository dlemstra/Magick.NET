//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace FileGenerator.MagickScript
{
  internal abstract class SwitchCodeGenerator : ScriptCodeGenerator
  {
    private int _StartIndent;

    private void WriteLengthCheck(IEnumerable<string> names, int level)
    {
      string shortName = (from name in names
                          where name.Length == level
                          select name).FirstOrDefault();
      if (shortName == null)
        return;

      Write("if (element.Name.Length == ");
      Write(level);
      WriteLine(")");
      WriteStartColon();
      WriteCase(shortName);
      WriteEndColon();
    }

    private void WriteSwitch(IEnumerable<string> names, int level)
    {
      IEnumerable<char> chars = (from name in names
                                 where name.Length > level
                                 select name[level]).Distinct();


      if (chars.Count() == 1 && names.Count() > 1)
      {
        WriteLengthCheck(names, level);
        WriteSwitch(names, ++level);
      }
      else
      {
        WriteLengthCheck(names, level);

        if (chars.Count() > 1)
        {
          Write("switch(element.Name[");
          Write(level);
          WriteLine("])");
          WriteStartColon();
        }

        foreach (char c in chars)
        {
          Write("case '");
          Write(c);
          WriteLine("':");
          WriteStartColon();

          IEnumerable<string> children = from name in names
                                         where name.Length > level && name[level] == c
                                         select name;

          if (children.Count() == 1)
            WriteCase(children.First());
          else
            WriteSwitch(children, level + 1);

          WriteEndColon();
        }

        if (chars.Count() > 1)
          WriteEndColon();

        if (Indent != _StartIndent)
          WriteLine("break;");
      }
    }

    protected static bool HasStaticCreateMethod(string typeName)
    {
      switch (typeName)
      {
        case "Double[]":
        case "PathArc":
        case "IDefines":
        case "IEnumerable<Double>":
        case "IEnumerable<MagickGeometry>":
        case "IEnumerable<IPath>":
        case "IEnumerable<PathArc>":
        case "IEnumerable<PointD>":
        case "IEnumerable<SparseColorArg>":
        case "ImageProfile":
        case "IReadDefines":
        case "MagickImage":
        case "MagickGeometry":
        case "MontageSettings":
        case "PixelStorageSettings":
        case "QuantizeSettings":
          return false;
        case "ColorProfile":
          return true;
        default:
          throw new NotImplementedException("HasStaticCreateMethod: " + typeName);
      }
    }

    protected void WriteSwitch(IEnumerable<string> names)
    {
      _StartIndent = Indent;
      WriteSwitch(names, 0);
      WriteLine("throw new NotImplementedException(element.Name);");
    }

    protected abstract void WriteCase(string name);
  }
}
