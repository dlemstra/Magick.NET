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

using System.Collections.Generic;
using System.Reflection;

namespace FileGenerator.MagickScript
{
    internal sealed class MagickImageGenerator : ExecuteCodeGenerator
    {
        protected override string[] CustomMethods
        {
            get
            {
                return new string[] { "clone", "draw", "write" };
            }
        }

        protected override string ExecuteArgument
        {
            get
            {
                return "IMagickImage image";
            }
        }

        protected override string ExecuteName
        {
            get
            {
                return "Image";
            }
        }

        protected override IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return Types.GetMagickImageProperties();
            }
        }

        protected override IEnumerable<MethodBase[]> Methods
        {
            get
            {
                return Types.GetGroupedMagickImageMethods();
            }
        }

        protected override void WriteCall(MethodBase method, ParameterInfo[] parameters)
        {
            Write("image.");
            Write(method.Name);
            Write("(");
            WriteParameters(parameters);
            WriteLine(");");
        }

        protected override void WriteHashtableCall(MethodBase method, ParameterInfo[] parameters)
        {
            Write("image.");
            Write(method.Name);
            Write("(");
            WriteHashtableParameters(parameters);
            WriteLine(");");
        }

        protected override void WriteSet(PropertyInfo property)
        {
            if (property.Name == "Settings")
                WriteLine("ExecuteMagickSettings(element, image);");
            else
            {
                Write("image.");
                Write(property.Name);
                Write(" = ");
                WriteGetValue(property);
            }
        }

        public override string Name
        {
            get
            {
                return "MagickImage";
            }
        }
    }
}
