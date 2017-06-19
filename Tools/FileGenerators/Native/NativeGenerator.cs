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

                return magickClass;
            }
        }

        private void CreateClasses()
        {
            var directory = new DirectoryInfo(PathHelper.GetFullPath(@"\Source\Magick.NET\Native"));

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
