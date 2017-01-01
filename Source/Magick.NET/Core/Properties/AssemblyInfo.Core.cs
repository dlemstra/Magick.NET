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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyProduct("Magick.NET.Core")]
[assembly: AssemblyDescription("Magick.NET.Core")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyCopyright("Copyright © 2013-2016 Dirk Lemstra")]
[assembly: AssemblyTrademark("")]

[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.4.100")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

#if Q8
[assembly: InternalsVisibleTo("Magick.NET.Core-Q8.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3" +
"fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e42" +
"09fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2" +
"c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1" +
"b44945a7")]
#elif Q16
[assembly: InternalsVisibleTo("Magick.NET.Core-Q16.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3" +
"fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e42" +
"09fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2" +
"c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1" +
"b44945a7")]
#elif Q16HDRI
[assembly: InternalsVisibleTo("Magick.NET.Core-Q16-HDRI.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3" +
"fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e42" +
"09fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2" +
"c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1" +
"b44945a7")]
#else
#error Not implemented!
#endif