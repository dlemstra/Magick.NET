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
using System.Reflection;

namespace FileGenerator.MagickScript
{
  internal sealed class PathsGenerator : ExecuteCodeGenerator
  {
    protected override string ExecuteArgument
    {
      get
      {
        return "Collection<IPath> paths";
      }
    }

    protected override string ExecuteName
    {
      get
      {
        return "IPath";
      }
    }

    protected override IEnumerable<MethodBase[]> Methods
    {
      get
      {
        return Types.GetPaths();
      }
    }

    protected override void WriteCall(MethodBase method, ParameterInfo[] parameters)
    {
      Write("paths.Add(new ");
      Write(method.DeclaringType.Name);
      Write("(");
      WriteParameters(parameters);

      WriteLine("));");
    }

    protected override void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters)
    {
      Write("paths.Add(new ");
      Write(method.DeclaringType.Name);
      Write("(");
      WriteHashtableParameters(parameters);
      WriteLine("));");
    }

    protected override void WriteSet(PropertyInfo property)
    {
      throw new NotImplementedException();
    }
  }
}
