// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FileGenerator.Native
{
    internal sealed class NativeGenerator
    {
        private DataContractJsonSerializer _Serializer;

        private MagickClass CreateClass(FileInfo file)
        {
            using (FileStream stream = file.OpenRead())
            {
                stream.Position = Encoding.UTF8.GetPreamble().Length;

                MagickClass magickClass = (MagickClass)_Serializer.ReadObject(stream);
                magickClass.Name = file.Name.Replace(".json", "");
                magickClass.FileName = file.Directory.FullName + "\\" + magickClass.Name + ".cs";
                if (string.IsNullOrEmpty(magickClass.ClassName))
                {
                    magickClass.ClassName = magickClass.Name;
                }

                return magickClass;
            }
        }

        private void CreateClasses()
        {
            var directory = new DirectoryInfo(PathHelper.GetFullPath(@"\src\Magick.NET\Native"));

            IEnumerable<MagickClass> classes = CreateClasses(directory.GetFiles("*.json", SearchOption.AllDirectories));

            NativeClassGenerator.Create(classes);
        }

        private IEnumerable<MagickClass> CreateClasses(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
                yield return CreateClass(file);
        }

        public NativeGenerator()
        {
            _Serializer = new DataContractJsonSerializer(typeof(MagickClass));
        }

        public static void Create()
        {
            var nativeMethodsGenerator = new NativeGenerator();
            nativeMethodsGenerator.CreateClasses();
        }
    }
}
