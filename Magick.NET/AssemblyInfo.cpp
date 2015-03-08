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

//=================================================================================================
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
#if (_M_X64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q8 x64 net20")];
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q8 x86 net20")];
#elif (_M_X64)
[assembly: AssemblyTitle("Magick.NET Q8 x64 net40-client")];
#else
[assembly: AssemblyTitle("Magick.NET Q8 x86 net40-client")];
#endif
#elif (MAGICKCORE_QUANTUM_DEPTH == 16 && !defined(MAGICKCORE_HDRI_SUPPORT))
#if (_M_X64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q16 x64 net20")];
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q16 x86 net20")];
#elif (_M_X64)
[assembly: AssemblyTitle("Magick.NET Q16 x64 net40-client")];
#else
[assembly: AssemblyTitle("Magick.NET Q16 x86 net40-client")];
#endif
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
#if (_M_X64 && NET20)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net20")];
#elif (NET20)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x86 net20")];
#elif (_M_X64)
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x64 net40-client")];
#else
[assembly: AssemblyTitle("Magick.NET Q16-HDRI x86 net40-client")];
#endif
#else
#error Not implemented!
#endif
[assembly: AssemblyProduct("Magick.NET")];
[assembly: AssemblyDescription("Magick.NET")];
[assembly: AssemblyCompany("")];
[assembly: AssemblyCopyright("Copyright © 2013-2015 Dirk Lemstra")];
[assembly: AssemblyTrademark("")];
//=================================================================================================
[assembly: AssemblyConfiguration("Release")];
[assembly: AssemblyCulture("")];
[assembly: AssemblyVersion("7.0.0.0")];
[assembly: AssemblyFileVersion("7.0.0.0011")]
[assembly: ComVisible(false)];
[assembly: CLSCompliant(true)];
[assembly: SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = true)];
//=================================================================================================