// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

#if WINDOWS_BUILD
[assembly: InternalsVisibleTo("Magick.NET.Core.Tests, PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010041848921d7f5c3" +
"fdd251ba0d5e4e18a23ad2c73239a163cfc0f3aabe0b1d3e0bb69a9c6ce8a83b3c9351f1287e42" +
"09fd8b3d7426b848b9715b219fcc28cc63a482a5678ee182d194b5a8f70ebbf65c3624b9920cb2" +
"c483b3f7c428b95b53eeb144e348120377ccb686359114a90273b271ea351835b347b3e38a30d1" +
"b44945a7")]
#else
[assembly: InternalsVisibleTo("Magick.NET.Core.Tests")]
#endif