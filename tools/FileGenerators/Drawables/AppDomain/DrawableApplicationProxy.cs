// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace FileGenerator.Drawables
{
    internal sealed class DrawableApplicationProxy : ApplicationProxy
    {
        public void GenerateIDrawables()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            IDrawablesGenerator.Generate();
        }

        public void GenerateDrawables()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            DrawablesGenerator.Generate();
        }

        public void GenerateIPaths()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            IPathsGenerator.Generate();
        }

        public void GeneratePaths()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            PathsGenerator.Generate();
        }
    }
}
