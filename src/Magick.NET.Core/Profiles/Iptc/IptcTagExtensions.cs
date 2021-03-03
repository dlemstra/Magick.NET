// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal static class IptcTagExtensions
    {
        public static bool IsRepeatable(this IptcTag tag)
        {
            switch (tag)
            {
                case IptcTag.RecordVersion:
                case IptcTag.ObjectType:
                case IptcTag.Title:
                case IptcTag.EditStatus:
                case IptcTag.EditorialUpdate:
                case IptcTag.Priority:
                case IptcTag.Category:
                case IptcTag.FixtureIdentifier:
                case IptcTag.ReleaseDate:
                case IptcTag.ReleaseTime:
                case IptcTag.ExpirationDate:
                case IptcTag.ExpirationTime:
                case IptcTag.SpecialInstructions:
                case IptcTag.ActionAdvised:
                case IptcTag.CreatedDate:
                case IptcTag.CreatedTime:
                case IptcTag.DigitalCreationDate:
                case IptcTag.DigitalCreationTime:
                case IptcTag.OriginatingProgram:
                case IptcTag.ProgramVersion:
                case IptcTag.ObjectCycle:
                case IptcTag.City:
                case IptcTag.SubLocation:
                case IptcTag.ProvinceState:
                case IptcTag.CountryCode:
                case IptcTag.Country:
                case IptcTag.OriginalTransmissionReference:
                case IptcTag.Headline:
                case IptcTag.Credit:
                case IptcTag.Source:
                case IptcTag.CopyrightNotice:
                case IptcTag.Caption:
                case IptcTag.ImageType:
                case IptcTag.ImageOrientation:
                    return false;
                default:
                    return true;
            }
        }

        public static bool IsDate(this IptcTag tag)
        {
            switch (tag)
            {
                case IptcTag.CreatedDate:
                case IptcTag.DigitalCreationDate:
                case IptcTag.ExpirationDate:
                case IptcTag.ReferenceDate:
                case IptcTag.ReleaseDate:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTime(this IptcTag tag)
        {
            switch (tag)
            {
                case IptcTag.CreatedTime:
                case IptcTag.DigitalCreationTime:
                case IptcTag.ExpirationTime:
                case IptcTag.ReleaseTime:
                    return true;
                default:
                    return false;
            }
        }
    }
}
