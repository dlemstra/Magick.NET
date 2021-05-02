// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FileGenerator.Native
{
    internal sealed class NativeGenerator
    {
        private readonly DataContractJsonSerializer _serializer;

        public NativeGenerator()
            => _serializer = new DataContractJsonSerializer(typeof(MagickClass));

        public static void Create()
        {
            var nativeMethodsGenerator = new NativeGenerator();
            nativeMethodsGenerator.CreateClasses();
        }

        private MagickClass CreateClass(FileInfo file)
        {
            using (var stream = file.OpenRead())
            {
                stream.Position = Encoding.UTF8.GetPreamble().Length;

                var magickClass = (MagickClass?)_serializer.ReadObject(stream);
                if (magickClass == null || file.Directory == null)
                    throw new InvalidOperationException();

                magickClass.Name = file.Name.Replace(".json", string.Empty);
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

            var classes = CreateClasses(directory.GetFiles("*.json", SearchOption.AllDirectories));

            NativeClassGenerator.Create(classes);
        }

        private IEnumerable<MagickClass> CreateClasses(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
                yield return CreateClass(file);
        }
    }
}
