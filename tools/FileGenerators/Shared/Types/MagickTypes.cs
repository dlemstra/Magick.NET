// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
            AssemblyFile = PathHelper.GetFullPath(@"src\Magick.NET\bin\" + folderName + @"\x86\net40\Magick.NET-" + quantumName + @"-x86.dll");
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
