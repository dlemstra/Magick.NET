//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
#include "Stdafx.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

#if (MAGICKCORE_QUANTUM_DEPTH == 8)
#if (_M_X64 && NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q8 x64 net20")];
#elif (NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q8 x86 net20")];
#elif (_M_X64)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q8 x64 net40-client")];
#else
[assembly:AssemblyTitle("Magick.NET.Wrapper Q8 x86 net40-client")];
#endif
#elif (MAGICKCORE_QUANTUM_DEPTH == 16 && !defined(MAGICKCORE_HDRI_SUPPORT))
#if (_M_X64 && NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16 x64 net20")];
#elif (NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16 x86 net20")];
#elif (_M_X64)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16 x64 net40-client")];
#else
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16 x86 net40-client")];
#endif
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
#if (_M_X64 && NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16-HDRI x64 net20")];
#elif (NET20)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16-HDRI x86 net20")];
#elif (_M_X64)
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16-HDRI x64 net40-client")];
#else
[assembly:AssemblyTitle("Magick.NET.Wrapper Q16-HDRI x86 net40-client")];
#endif
#else
#error Not implemented!
#endif
[assembly:AssemblyProduct("Magick.NET.Wrapper")];
[assembly:AssemblyDescription("Magick.NET.Wrapper")];
[assembly:AssemblyCompany("")];
[assembly:AssemblyCopyright("Copyright © 2013-2015 Dirk Lemstra")];
[assembly:AssemblyTrademark("")];

[assembly:InternalsVisibleTo("Magick.NET-AnyCPU, PublicKey=002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e4209fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1b44945a7")]
[assembly:InternalsVisibleTo("Magick.NET-x64, PublicKey=002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e4209fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1b44945a7")]
[assembly:InternalsVisibleTo("Magick.NET-x86, PublicKey=002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e4209fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1b44945a7")]

[assembly:AssemblyConfiguration("Release")];
[assembly:AssemblyCulture("")];
[assembly:AssemblyVersion("7.0.0.0")];
[assembly:AssemblyFileVersion("7.0.0.0019")]
[assembly:ComVisible(false)];
[assembly:CLSCompliant(true)];