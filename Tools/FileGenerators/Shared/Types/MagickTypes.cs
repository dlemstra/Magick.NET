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
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileGenerator
{
  public class MagickTypes
  {
    private static string GetFolderName(QuantumDepth depth)
    {
      switch (depth)
      {
        case QuantumDepth.Q8:
          return "ReleaseQ8";
        case QuantumDepth.Q16:
          return "ReleaseQ16";
        case QuantumDepth.Q16HDRI:
          return "ReleaseQ16-HDRI";
        default:
          throw new NotImplementedException();
      }
    }

    private static string GetQuantumName(QuantumDepth depth)
    {
      switch (depth)
      {
        case QuantumDepth.Q8:
        case QuantumDepth.Q16:
          return depth.ToString();
        case QuantumDepth.Q16HDRI:
          return "Q16-HDRI";
        default:
          throw new NotImplementedException();
      }
    }

    private Assembly LoadAssembly()
    {
      if (!File.Exists(AssemblyFile))
        throw new ArgumentException("Unable to find file: " + AssemblyFile, "fileName");

      return Assembly.ReflectionOnlyLoad(File.ReadAllBytes(AssemblyFile));
    }

    protected QuantumDepth Depth
    {
      get;
      private set;
    }

    protected Assembly MagickNET
    {
      get;
      private set;
    }

    protected string AssemblyFile
    {
      get;
      private set;
    }

    protected IEnumerable<Type> GetTypes()
    {
      return MagickNET.GetTypes();
    }

    public MagickTypes(QuantumDepth depth)
    {
      string folderName = GetFolderName(depth);
      string quantumName = GetQuantumName(depth);
      AssemblyFile = PathHelper.GetFullPath(@"Magick.NET\bin\" + folderName + @"\x86\Magick.NET-" + quantumName + @"-x86.dll");
      MagickNET = LoadAssembly();
      Depth = depth;
    }

    public IEnumerable<Type> GetInterfaceTypes(string interfaceName)
    {
      return from type in MagickNET.GetTypes()
             from interfaceType in type.GetInterfaces()
             where interfaceType.Name == interfaceName && type.IsPublic && !type.IsInterface && !type.IsAbstract
             orderby type.Name
             select type;
    }
  }
}
