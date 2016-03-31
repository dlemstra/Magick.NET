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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

#if (Q8)
#if (WIN64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q8 x64 net20")]
#elif (ANYCPU && NET20)
[assembly: AssemblyTitle("Magick.NET Q8 AnyCPU net20")]
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q8 x86 net20")]
#elif (WIN64)
[assembly: AssemblyTitle("Magick.NET Q8 x64 net40-client")]
#elif (ANYCPU)
[assembly: AssemblyTitle("Magick.NET Q8 AnyCPU net40-client")]
#else
[assembly: AssemblyTitle("Magick.NET Q8 x86 net40-client")]
#endif
#elif (Q16)
#if (WIN64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q16 x64 net20")]
#elif (ANYCPU && NET20)
[assembly: AssemblyTitle("Magick.NET Q16 AnyCPU net20")]
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q16 x86 net20")]
#elif (WIN64)
[assembly: AssemblyTitle("Magick.NET Q16 x64 net40-client")]
#elif (ANYCPU)
[assembly: AssemblyTitle("Magick.NET Q16 AnyCPU net40-client")]
#else
[assembly: AssemblyTitle("Magick.NET Q16 x86 net40-client")]
#endif
#elif (Q16HDRI)
#if (WIN64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net20")]
#elif (ANYCPU && NET20)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI AnyCPU net20")]
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x86 net20")]
#elif (WIN64)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net40-client")]
#elif (ANYCPU)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI AnyCPU net40-client")]
#else
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x86 net40-client")]
#endif
#else
#error Not implemented!
#endif

[assembly: AssemblyProduct("Magick.NET")]
[assembly: AssemblyDescription("Magick.NET")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyCopyright("Copyright © 2013-2016 Dirk Lemstra")]
[assembly: AssemblyTrademark("")]

[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.0.0104")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
#if (NET20)
[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode = true)]
#endif