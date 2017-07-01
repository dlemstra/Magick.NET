// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal sealed class MagickImageCollectionGenerator : ExecuteCodeGenerator
    {
        private static bool ReturnsImage(MethodBase method)
        {
            return ((MethodInfo)method).ReturnType.Name == "IMagickImage";
        }

        protected override string ExecuteArgument
        {
            get
            {
                return "IMagickImageCollection collection";
            }
        }

        protected override string ExecuteName
        {
            get
            {
                return "Collection";
            }
        }

        protected override IEnumerable<MethodBase[]> Methods
        {
            get
            {
                return Types.GetGroupedMagickImageCollectionMethods().
                      Concat(Types.GetGroupedMagickImageCollectionResultMethods());
            }
        }

        protected override string ReturnType
        {
            get
            {
                return "IMagickImage";
            }
        }

        protected override void WriteCall(MethodBase method, ParameterInfo[] parameters)
        {
            if (ReturnsImage(method))
                Write("return ");

            Write("collection.");
            Write(method.Name);
            Write("(");
            WriteParameters(parameters);
            WriteLine(");");

            if (!ReturnsImage(method))
                WriteLine("return null;");
        }

        protected override void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters)
        {
            bool returnsImage = ReturnsImage(method);

            if (returnsImage)
                Write("return ");
            else
                WriteStartColon();

            Write("collection.");
            Write(method.Name);
            Write("(");
            WriteHashtableParameters(parameters);
            WriteLine(");");

            if (!ReturnsImage(method))
            {
                WriteLine("return null;");
                WriteEndColon();
            }
        }

        protected override void WriteSet(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get
            {
                return "MagickImageCollection";
            }
        }
    }
}
