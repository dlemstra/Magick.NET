//=================================================================================================
// Copyright 2013 Dirk Lemstra
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
#include "stdafx.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

//=================================================================================================
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
#if _M_X64
[assembly:AssemblyTitle("Magick.NET Q8 x64")];
#else
[assembly:AssemblyTitle("Magick.NET Q8 x86")];
#endif
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
#if _M_X64
[assembly:AssemblyTitle("Magick.NET Q16 x64")];
#else
[assembly:AssemblyTitle("Magick.NET Q16 x86")];
#endif
#else
Not implemented!
#endif
[assembly:AssemblyProduct("Magick.NET")];
[assembly:AssemblyDescription("Magick.NET")];
[assembly:AssemblyCompany("")];
[assembly:AssemblyCopyright("Copyright © Dirk Lemstra 2013")];
[assembly:AssemblyTrademark("")];
//=================================================================================================
[assembly:AssemblyConfiguration("Release")];
[assembly:AssemblyCulture("")];
[assembly:AssemblyVersion("6.0.0.0")];
[assembly:AssemblyFileVersion("6.8.5.4")]
[assembly:ComVisible(false)];
[assembly:CLSCompliant(true)];
[assembly:SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = true)];
//=================================================================================================