//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Magick.NET.FileGenerator
{
  internal abstract class SwitchCodeGenerator : CodeGenerator
  {
    private int _StartIndent;

    private void WriteLengthCheck(IndentedTextWriter writer, IEnumerable<string> names, int level)
    {
      string shortName = (from name in names
                          where name.Length == level
                          select name).FirstOrDefault();
      if (shortName == null)
        return;

      writer.Write("if (element.Name.Length == ");
      writer.Write(level);
      writer.WriteLine(")");
      WriteStartColon(writer);
      WriteCase(writer, shortName);
      WriteEndColon(writer);
    }

    private void WriteSwitch(IndentedTextWriter writer, IEnumerable<string> names, int level)
    {
      IEnumerable<char> chars = (from name in names
                                 where name.Length > level
                                 select name[level]).Distinct();


      if (chars.Count() == 1 && names.Count() > 1)
      {
        WriteLengthCheck(writer, names, level);
        WriteSwitch(writer, names, ++level);
      }
      else
      {
        WriteLengthCheck(writer, names, level);

        if (chars.Count() > 1)
        {
          writer.Write("switch(element.Name[");
          writer.Write(level);
          writer.WriteLine("])");
          WriteStartColon(writer);
        }

        foreach (char c in chars)
        {
          writer.Write("case '");
          writer.Write(c);
          writer.WriteLine("':");
          WriteStartColon(writer);

          IEnumerable<string> children = from name in names
                                         where name.Length > level && name[level] == c
                                         select name;

          if (children.Count() == 1)
            WriteCase(writer, children.First());
          else
            WriteSwitch(writer, children, level + 1);

          WriteEndColon(writer);
        }

        if (chars.Count() > 1)
          WriteEndColon(writer);

        if (writer.Indent != _StartIndent)
          writer.WriteLine("break;");
      }
    }

    protected void WriteSwitch(IndentedTextWriter writer, IEnumerable<string> names)
    {
      _StartIndent = writer.Indent;
      WriteSwitch(writer, names, 0);
      writer.WriteLine("throw new NotImplementedException(element.Name);");
    }

    protected abstract void WriteCase(IndentedTextWriter writer, string name);
  }
}
