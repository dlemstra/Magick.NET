# Versioning

Magick.NET uses the following versioning strategy:

IMVERSION.MAJOR.MINOR.PATCH

- IMVERSION is the version number of ImageMagick (7)
- MAJOR will be incremented when an incompatible API changes is made
- MINOR will be incremented when a functionality is added in a backwards-compatible manner
- PATCH will be incremented when only the ImageMagick libraries are rebuild with a newer version

The AssemblyVersion will only change when IMVERSION, MAJOR or MINOR are modified.