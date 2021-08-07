// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
#if !NETSTANDARD
using System.Security.Permissions;
#endif

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
#if !NETSTANDARD
[assembly: SecurityPermission(SecurityAction.RequestMinimum, UnmanagedCode = true)]
#endif
