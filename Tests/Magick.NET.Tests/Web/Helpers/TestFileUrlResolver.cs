//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

#if !NETCOREAPP1_1

using System;
using ImageMagick;
using ImageMagick.Web;
using System.Xml.XPath;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestFileUrlResolver : IFileUrlResolver, IScriptData
    {
        public static TestFileUrlResolverResult Result;
        public static TestScriptData ScriptResult;

        public string FileName
        {
            get;
            set;
        }

        public MagickFormat Format
        {
            get;
            set;
        }

        public MagickFormat OutputFormat
        {
            get;
            set;
        }

        public IXPathNavigable Script
        {
            get;
            set;
        }

        public bool Resolve(Uri url)
        {
            if (Result == null)
                return false;

            FileName = Result.FileName;
            Format = Result.Format;

            if (ScriptResult != null)
            {
                OutputFormat = ScriptResult.OutputFormat;
                Script = ScriptResult.Script;
            }

            return true;
        }
    }
}


#endif