# Versioning

Magick.NET uses the following versioning strategy:

IMVERSION.MAJOR.MINOR.PATCH

- IMVERSION is the version number of ImageMagick (7)
- MAJOR will be incremented when an incompatible API change is made
- MINOR will be incremented when a functionality is added in a backwards-compatible manner
- PATCH will be incremented when only the ImageMagick libraries are rebuild with a newer version

The other libraries use normal semver and have the follow version strategy: MAJOR.MINOR.PATCH.0.
And the AssemblyVersion will only change when one of the first three number is modified. 