//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if Q8
#if WIN64 && NET20
[assembly: AssemblyTitle("Magick.NET Q8 x64 net20")]
#elif ANYCPU && NET20
[assembly: AssemblyTitle("Magick.NET Q8 AnyCPU net20")]
#elif NET20
[assembly: AssemblyTitle("Magick.NET Q8 x86 net20")]
#elif WIN64
[assembly: AssemblyTitle("Magick.NET Q8 x64 net40-client")]
#elif ANYCPU
[assembly: AssemblyTitle("Magick.NET Q8 AnyCPU net40-client")]
#else
[assembly: AssemblyTitle("Magick.NET Q8 x86 net40-client")]
#endif
#elif Q16
#if WIN64 && NET20
[assembly: AssemblyTitle("Magick.NET Q16 x64 net20")]
#elif ANYCPU && NET20
[assembly: AssemblyTitle("Magick.NET Q16 AnyCPU net20")]
#elif NET20
[assembly: AssemblyTitle("Magick.NET Q16 x86 net20")]
#elif WIN64
[assembly: AssemblyTitle("Magick.NET Q16 x64 net40-client")]
#elif ANYCPU
[assembly: AssemblyTitle("Magick.NET Q16 AnyCPU net40-client")]
#else
[assembly: AssemblyTitle("Magick.NET Q16 x86 net40-client")]
#endif
#elif Q16HDRI
#if WIN64 && NET20
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net20")]
#elif ANYCPU && NET20
[assembly: AssemblyTitle("Magick.NET Q16-HDRI AnyCPU net20")]
#elif NET20
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x86 net20")]
#elif WIN64
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net40-client")]
#elif ANYCPU
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
[assembly: AssemblyFileVersion("7.0.4.100")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
#if NET20
[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode = true)]
#endif

[assembly: InternalsVisibleTo("Magick.NET.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3" +
"fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e42" +
"09fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2" +
"c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1" +
"b44945a7")]