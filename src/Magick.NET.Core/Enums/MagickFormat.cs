// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Specifies the different image formats that are supported by ImageMagick.
/// </summary>
public enum MagickFormat
{
    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// Hasselblad CFV/H3D39II Raw Format.
    /// </summary>
    ThreeFr,

    /// <summary>
    /// Media Container.
    /// </summary>
    ThreeG2,

    /// <summary>
    /// Media Container.
    /// </summary>
    ThreeGp,

    /// <summary>
    /// Raw alpha samples.
    /// </summary>
    A,

    /// <summary>
    /// AAI Dune image.
    /// </summary>
    Aai,

    /// <summary>
    /// Adobe Illustrator CS2.
    /// </summary>
    Ai,

    /// <summary>
    /// Animated Portable Network Graphics.
    /// </summary>
    APng,

    /// <summary>
    /// PFS: 1st Publisher Clip Art.
    /// </summary>
    Art,

    /// <summary>
    /// Sony Alpha Raw Format.
    /// </summary>
    Arw,

    /// <summary>
    /// Image sequence laid out in continuous irregular courses (Unknown).
    /// </summary>
    Ashlar,

    /// <summary>
    /// Microsoft Audio/Visual Interleaved.
    /// </summary>
    Avi,

    /// <summary>
    /// AV1 Image File Format (Heic).
    /// </summary>
    Avif,

    /// <summary>
    /// AVS X image.
    /// </summary>
    Avs,

    /// <summary>
    /// Raw blue samples.
    /// </summary>
    B,

    /// <summary>
    /// Raw mosaiced samples.
    /// </summary>
    Bayer,

    /// <summary>
    /// Raw mosaiced and alpha samples.
    /// </summary>
    Bayera,

    /// <summary>
    /// Raw blue, green, and red samples.
    /// </summary>
    Bgr,

    /// <summary>
    /// Raw blue, green, red, and alpha samples.
    /// </summary>
    Bgra,

    /// <summary>
    /// Raw blue, green, red, and opacity samples.
    /// </summary>
    Bgro,

    /// <summary>
    /// Microsoft Windows bitmap image.
    /// </summary>
    Bmp,

    /// <summary>
    /// Microsoft Windows bitmap image (V2).
    /// </summary>
    Bmp2,

    /// <summary>
    /// Microsoft Windows bitmap image (V3).
    /// </summary>
    Bmp3,

    /// <summary>
    /// BRF ASCII Braille format.
    /// </summary>
    Brf,

    /// <summary>
    /// Raw cyan samples.
    /// </summary>
    C,

    /// <summary>
    /// Continuous Acquisition and Life-cycle Support Type 1.
    /// </summary>
    Cal,

    /// <summary>
    /// Continuous Acquisition and Life-cycle Support Type 1.
    /// </summary>
    Cals,

    /// <summary>
    /// Constant image uniform color.
    /// </summary>
    Canvas,

    /// <summary>
    /// Caption.
    /// </summary>
    Caption,

    /// <summary>
    /// Cineon Image File.
    /// </summary>
    Cin,

    /// <summary>
    /// Cisco IP phone image format.
    /// </summary>
    Cip,

    /// <summary>
    /// Image Clip Mask.
    /// </summary>
    Clip,

    /// <summary>
    /// The system clipboard.
    /// </summary>
    Clipboard,

    /// <summary>
    /// Raw cyan, magenta, yellow, and black samples.
    /// </summary>
    Cmyk,

    /// <summary>
    /// Raw cyan, magenta, yellow, black, and alpha samples.
    /// </summary>
    Cmyka,

    /// <summary>
    /// Canon Digital Camera Raw Format.
    /// </summary>
    Cr2,

    /// <summary>
    /// Canon Digital Camera Raw Format.
    /// </summary>
    Cr3,

    /// <summary>
    /// Canon Digital Camera Raw Format.
    /// </summary>
    Crw,

    /// <summary>
    /// Cube color lookup table image.
    /// </summary>
    Cube,

    /// <summary>
    /// Microsoft icon.
    /// </summary>
    Cur,

    /// <summary>
    /// DR Halo.
    /// </summary>
    Cut,

    /// <summary>
    /// Base64-encoded inline images.
    /// </summary>
    Data,

    /// <summary>
    /// Digital Imaging and Communications in Medicine image.
    /// </summary>
    Dcm,

    /// <summary>
    /// Kodak Digital Camera Raw Format.
    /// </summary>
    Dcr,

    /// <summary>
    /// Raw Photo Decoder (dcraw).
    /// </summary>
    Dcraw,

    /// <summary>
    /// ZSoft IBM PC multi-page Paintbrush.
    /// </summary>
    Dcx,

    /// <summary>
    /// Microsoft DirectDraw Surface.
    /// </summary>
    Dds,

    /// <summary>
    /// Multi-face font package.
    /// </summary>
    Dfont,

    /// <summary>
    /// Microsoft Windows 3.X Packed Device-Independent Bitmap.
    /// </summary>
    Dib,

    /// <summary>
    /// Digital Negative Raw Format.
    /// </summary>
    Dng,

    /// <summary>
    /// SMPTE 268M-2003 (DPX 2.0).
    /// </summary>
    Dpx,

    /// <summary>
    /// Microsoft DirectDraw Surface.
    /// </summary>
    Dxt1,

    /// <summary>
    /// Microsoft DirectDraw Surface.
    /// </summary>
    Dxt5,

    /// <summary>
    /// Windows Enhanced Meta File.
    /// </summary>
    Emf,

    /// <summary>
    /// Encapsulated Portable Document Format.
    /// </summary>
    Epdf,

    /// <summary>
    /// Encapsulated PostScript Interchange format.
    /// </summary>
    Epi,

    /// <summary>
    /// Encapsulated PostScript.
    /// </summary>
    Eps,

    /// <summary>
    /// Level II Encapsulated PostScript.
    /// </summary>
    Eps2,

    /// <summary>
    /// Level III Encapsulated PostScript.
    /// </summary>
    Eps3,

    /// <summary>
    /// Encapsulated PostScript.
    /// </summary>
    Epsf,

    /// <summary>
    /// Encapsulated PostScript Interchange format.
    /// </summary>
    Epsi,

    /// <summary>
    /// Encapsulated PostScript with TIFF preview.
    /// </summary>
    Ept,

    /// <summary>
    /// Encapsulated PostScript Level II with TIFF preview.
    /// </summary>
    Ept2,

    /// <summary>
    /// Encapsulated PostScript Level III with TIFF preview.
    /// </summary>
    Ept3,

    /// <summary>
    /// Epson Raw Format.
    /// </summary>
    Erf,

    /// <summary>
    /// High Dynamic-range (HDR).
    /// </summary>
    Exr,

    /// <summary>
    /// Farbfeld.
    /// </summary>
    Farbfeld,

    /// <summary>
    /// Group 3 FAX.
    /// </summary>
    Fax,

    /// <summary>
    /// Farbfeld.
    /// </summary>
    Ff,

    /// <summary>
    /// Hasselblad CFV/H3D39II Raw Format.
    /// </summary>
    Fff,

    /// <summary>
    /// Uniform Resource Locator (file://).
    /// </summary>
    File,

    /// <summary>
    /// Flexible Image Transport System.
    /// </summary>
    Fits,

    /// <summary>
    /// FilmLight.
    /// </summary>
    Fl32,

    /// <summary>
    /// Flash Video Stream.
    /// </summary>
    Flv,

    /// <summary>
    /// Plasma fractal image.
    /// </summary>
    Fractal,

    /// <summary>
    /// Uniform Resource Locator (ftp://).
    /// </summary>
    Ftp,

    /// <summary>
    /// Flexible Image Transport System.
    /// </summary>
    Fts,

    /// <summary>
    /// Formatted text image.
    /// </summary>
    Ftxt,

    /// <summary>
    /// Raw green samples.
    /// </summary>
    G,

    /// <summary>
    /// Group 3 FAX.
    /// </summary>
    G3,

    /// <summary>
    /// Group 4 FAX.
    /// </summary>
    G4,

    /// <summary>
    /// CompuServe graphics interchange format.
    /// </summary>
    Gif,

    /// <summary>
    /// CompuServe graphics interchange format.
    /// </summary>
    Gif87,

    /// <summary>
    /// Gradual linear passing from one shade to another.
    /// </summary>
    Gradient,

    /// <summary>
    /// Raw gray samples.
    /// </summary>
    Gray,

    /// <summary>
    /// Raw gray and alpha samples.
    /// </summary>
    Graya,

    /// <summary>
    /// Raw CCITT Group4.
    /// </summary>
    Group4,

    /// <summary>
    /// Identity Hald color lookup table image.
    /// </summary>
    Hald,

    /// <summary>
    /// Radiance RGBE image format.
    /// </summary>
    Hdr,

    /// <summary>
    /// High Efficiency Image Format.
    /// </summary>
    Heic,

    /// <summary>
    /// High Efficiency Image Format.
    /// </summary>
    Heif,

    /// <summary>
    /// Histogram of the image.
    /// </summary>
    Histogram,

    /// <summary>
    /// Slow Scan TeleVision.
    /// </summary>
    Hrz,

    /// <summary>
    /// Hypertext Markup Language and a client-side image map.
    /// </summary>
    Htm,

    /// <summary>
    /// Hypertext Markup Language and a client-side image map.
    /// </summary>
    Html,

    /// <summary>
    /// Uniform Resource Locator (http://).
    /// </summary>
    Http,

    /// <summary>
    /// Uniform Resource Locator (https://).
    /// </summary>
    Https,

    /// <summary>
    /// Truevision Targa image.
    /// </summary>
    Icb,

    /// <summary>
    /// Microsoft icon.
    /// </summary>
    Ico,

    /// <summary>
    /// Microsoft icon.
    /// </summary>
    Icon,

    /// <summary>
    /// Phase One Raw Format.
    /// </summary>
    Iiq,

    /// <summary>
    /// The image format and characteristics.
    /// </summary>
    Info,

    /// <summary>
    /// Base64-encoded inline images.
    /// </summary>
    Inline,

    /// <summary>
    /// IPL Image Sequence.
    /// </summary>
    Ipl,

    /// <summary>
    /// ISO/TR 11548-1 format.
    /// </summary>
    Isobrl,

    /// <summary>
    /// ISO/TR 11548-1 format 6dot.
    /// </summary>
    Isobrl6,

    /// <summary>
    /// JPEG-2000 Code Stream Syntax.
    /// </summary>
    J2c,

    /// <summary>
    /// JPEG-2000 Code Stream Syntax.
    /// </summary>
    J2k,

    /// <summary>
    /// JPEG Network Graphics.
    /// </summary>
    Jng,

    /// <summary>
    /// Garmin tile format.
    /// </summary>
    Jnx,

    /// <summary>
    /// JPEG-2000 File Format Syntax.
    /// </summary>
    Jp2,

    /// <summary>
    /// JPEG-2000 Code Stream Syntax.
    /// </summary>
    Jpc,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format.
    /// </summary>
    Jpe,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format.
    /// </summary>
    Jpeg,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format.
    /// </summary>
    Jpg,

    /// <summary>
    /// JPEG-2000 File Format Syntax.
    /// </summary>
    Jpm,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format.
    /// </summary>
    Jps,

    /// <summary>
    /// JPEG-2000 File Format Syntax.
    /// </summary>
    Jpt,

    /// <summary>
    /// The image format and characteristics.
    /// </summary>
    Json,

    /// <summary>
    /// JPEG XL Lossless JPEG1 Recompression.
    /// </summary>
    Jxl,

    /// <summary>
    /// Raw black samples.
    /// </summary>
    K,

    /// <summary>
    /// Kodak Digital Camera Raw Format.
    /// </summary>
    K25,

    /// <summary>
    /// Kodak Digital Camera Raw Format.
    /// </summary>
    Kdc,

    /// <summary>
    /// Image label.
    /// </summary>
    Label,

    /// <summary>
    /// Raw magenta samples.
    /// </summary>
    M,

    /// <summary>
    /// MPEG Video Stream.
    /// </summary>
    M2v,

    /// <summary>
    /// Raw MPEG-4 Video.
    /// </summary>
    M4v,

    /// <summary>
    /// MAC Paint.
    /// </summary>
    Mac,

    /// <summary>
    /// Colormap intensities and indices.
    /// </summary>
    Map,

    /// <summary>
    /// Image Clip Mask.
    /// </summary>
    Mask,

    /// <summary>
    /// MATLAB level 5 image format.
    /// </summary>
    Mat,

    /// <summary>
    /// MATTE format.
    /// </summary>
    Matte,

    /// <summary>
    /// Minolta Digital Camera Raw Format.
    /// </summary>
    Mdc,

    /// <summary>
    /// Mamiya Raw Format.
    /// </summary>
    Mef,

    /// <summary>
    /// Magick Image File Format.
    /// </summary>
    Miff,

    /// <summary>
    /// Multimedia Container.
    /// </summary>
    Mkv,

    /// <summary>
    /// Multiple-image Network Graphics.
    /// </summary>
    Mng,

    /// <summary>
    /// Raw bi-level bitmap.
    /// </summary>
    Mono,

    /// <summary>
    /// Aptus Leaf Raw Format.
    /// </summary>
    Mos,

    /// <summary>
    /// MPEG Video Stream.
    /// </summary>
    Mov,

    /// <summary>
    /// MPEG-4 Video Stream.
    /// </summary>
    Mp4,

    /// <summary>
    /// Magick Persistent Cache image format.
    /// </summary>
    Mpc,

    /// <summary>
    /// MPEG Video Stream.
    /// </summary>
    Mpeg,

    /// <summary>
    /// MPEG Video Stream.
    /// </summary>
    Mpg,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format (Jpeg).
    /// </summary>
    Mpo,

    /// <summary>
    /// Sony (Minolta) Raw Format.
    /// </summary>
    Mrw,

    /// <summary>
    /// Magick Scripting Language.
    /// </summary>
    Msl,

    /// <summary>
    /// ImageMagick's own SVG internal renderer.
    /// </summary>
    Msvg,

    /// <summary>
    /// MTV Raytracing image format.
    /// </summary>
    Mtv,

    /// <summary>
    /// Magick Vector Graphics.
    /// </summary>
    Mvg,

    /// <summary>
    /// Nikon Digital SLR Camera Raw Format.
    /// </summary>
    Nef,

    /// <summary>
    /// Nikon Digital SLR Camera Raw Format.
    /// </summary>
    Nrw,

    /// <summary>
    /// Constant image of uniform color.
    /// </summary>
    Null,

    /// <summary>
    /// Raw opacity samples.
    /// </summary>
    O,

    /// <summary>
    /// OpenRaster format.
    /// </summary>
    Ora,

    /// <summary>
    /// Olympus Digital Camera Raw Format.
    /// </summary>
    Orf,

    /// <summary>
    /// On-the-air bitmap.
    /// </summary>
    Otb,

    /// <summary>
    /// Open Type font.
    /// </summary>
    Otf,

    /// <summary>
    /// 16bit/pixel interleaved YUV.
    /// </summary>
    Pal,

    /// <summary>
    /// Palm pixmap.
    /// </summary>
    Palm,

    /// <summary>
    /// Common 2-dimensional bitmap format.
    /// </summary>
    Pam,

    /// <summary>
    /// Pango Markup Language.
    /// </summary>
    Pango,

    /// <summary>
    /// Predefined pattern.
    /// </summary>
    Pattern,

    /// <summary>
    /// Portable bitmap format (black and white).
    /// </summary>
    Pbm,

    /// <summary>
    /// Photo CD.
    /// </summary>
    Pcd,

    /// <summary>
    /// Photo CD.
    /// </summary>
    Pcds,

    /// <summary>
    /// Printer Control Language.
    /// </summary>
    Pcl,

    /// <summary>
    /// Apple Macintosh QuickDraw/PICT.
    /// </summary>
    Pct,

    /// <summary>
    /// ZSoft IBM PC Paintbrush.
    /// </summary>
    Pcx,

    /// <summary>
    /// Palm Database ImageViewer Format.
    /// </summary>
    Pdb,

    /// <summary>
    /// Portable Document Format.
    /// </summary>
    Pdf,

    /// <summary>
    /// Portable Document Archive Format.
    /// </summary>
    Pdfa,

    /// <summary>
    /// Pentax Electronic Raw Format.
    /// </summary>
    Pef,

    /// <summary>
    /// Embrid Embroidery Format.
    /// </summary>
    Pes,

    /// <summary>
    /// Postscript Type 1 font (ASCII).
    /// </summary>
    Pfa,

    /// <summary>
    /// Postscript Type 1 font (binary).
    /// </summary>
    Pfb,

    /// <summary>
    /// Portable float format.
    /// </summary>
    Pfm,

    /// <summary>
    /// JPEG 2000 uncompressed format.
    /// </summary>
    Pgx,

    /// <summary>
    /// Portable graymap format (gray scale).
    /// </summary>
    Pgm,

    /// <summary>
    /// Portable half float format.
    /// </summary>
    Phm,

    /// <summary>
    /// Personal Icon.
    /// </summary>
    Picon,

    /// <summary>
    /// Apple Macintosh QuickDraw/PICT.
    /// </summary>
    Pict,

    /// <summary>
    /// Alias/Wavefront RLE image format.
    /// </summary>
    Pix,

    /// <summary>
    /// Joint Photographic Experts Group JFIF format.
    /// </summary>
    Pjpeg,

    /// <summary>
    /// Plasma fractal image.
    /// </summary>
    Plasma,

    /// <summary>
    /// Portable Network Graphics.
    /// </summary>
    Png,

    /// <summary>
    /// PNG inheriting bit-depth and color-type from original.
    /// </summary>
    Png00,

    /// <summary>
    /// opaque or binary transparent 24-bit RGB.
    /// </summary>
    Png24,

    /// <summary>
    /// opaque or transparent 32-bit RGBA.
    /// </summary>
    Png32,

    /// <summary>
    /// opaque or binary transparent 48-bit RGB.
    /// </summary>
    Png48,

    /// <summary>
    /// opaque or transparent 64-bit RGBA.
    /// </summary>
    Png64,

    /// <summary>
    /// 8-bit indexed with optional binary transparency.
    /// </summary>
    Png8,

    /// <summary>
    /// Portable anymap.
    /// </summary>
    Pnm,

    /// <summary>
    /// Pocketmod Personal Organizer (Pdf).
    /// </summary>
    Pocketmod,

    /// <summary>
    /// Portable pixmap format (color).
    /// </summary>
    Ppm,

    /// <summary>
    /// PostScript.
    /// </summary>
    Ps,

    /// <summary>
    /// Level II PostScript.
    /// </summary>
    Ps2,

    /// <summary>
    /// Level III PostScript.
    /// </summary>
    Ps3,

    /// <summary>
    /// Adobe Large Document Format.
    /// </summary>
    Psb,

    /// <summary>
    /// Adobe Photoshop bitmap.
    /// </summary>
    Psd,

    /// <summary>
    /// Pyramid encoded TIFF.
    /// </summary>
    Ptif,

    /// <summary>
    /// Seattle Film Works.
    /// </summary>
    Pwp,

    /// <summary>
    /// Quite OK image format.
    /// </summary>
    Qoi,

    /// <summary>
    /// Raw red samples.
    /// </summary>
    R,

    /// <summary>
    /// Gradual radial passing from one shade to another.
    /// </summary>
    RadialGradient,

    /// <summary>
    /// Fuji CCD-RAW Graphic File.
    /// </summary>
    Raf,

    /// <summary>
    /// SUN Rasterfile.
    /// </summary>
    Ras,

    /// <summary>
    /// Raw.
    /// </summary>
    Raw,

    /// <summary>
    /// Raw red, green, and blue samples.
    /// </summary>
    Rgb,

    /// <summary>
    /// Raw red, green, blue samples in 565 format.
    /// </summary>
    Rgb565,

    /// <summary>
    /// Raw red, green, blue, and alpha samples.
    /// </summary>
    Rgba,

    /// <summary>
    /// Raw red, green, blue, and opacity samples.
    /// </summary>
    Rgbo,

    /// <summary>
    /// LEGO Mindstorms EV3 Robot Graphic Format (black and white).
    /// </summary>
    Rgf,

    /// <summary>
    /// Alias/Wavefront image.
    /// </summary>
    Rla,

    /// <summary>
    /// Utah Run length encoded image.
    /// </summary>
    Rle,

    /// <summary>
    /// Raw Media Format.
    /// </summary>
    Rmf,

    /// <summary>
    /// Rsvg.
    /// </summary>
    Rsvg,

    /// <summary>
    /// Panasonic Lumix Raw Format.
    /// </summary>
    Rw2,

    /// <summary>
    /// Leica Raw Format.
    /// </summary>
    Rwl,

    /// <summary>
    /// ZX-Spectrum SCREEN$.
    /// </summary>
    Scr,

    /// <summary>
    /// Screen shot.
    /// </summary>
    Screenshot,

    /// <summary>
    /// Scitex HandShake.
    /// </summary>
    Sct,

    /// <summary>
    /// Seattle Film Works.
    /// </summary>
    Sfw,

    /// <summary>
    /// Irix RGB image.
    /// </summary>
    Sgi,

    /// <summary>
    /// Hypertext Markup Language and a client-side image map.
    /// </summary>
    Shtml,

    /// <summary>
    /// DEC SIXEL Graphics Format.
    /// </summary>
    Six,

    /// <summary>
    /// DEC SIXEL Graphics Format.
    /// </summary>
    Sixel,

    /// <summary>
    /// Sparse Color.
    /// </summary>
    SparseColor,

    /// <summary>
    /// Sony Raw Format 2.
    /// </summary>
    Sr2,

    /// <summary>
    /// Sony Raw Format.
    /// </summary>
    Srf,

    /// <summary>
    /// Samsung Raw Format.
    /// </summary>
    Srw,

    /// <summary>
    /// Steganographic image.
    /// </summary>
    Stegano,

    /// <summary>
    /// Sinar CaptureShop Raw Format.
    /// </summary>
    Sti,

    /// <summary>
    /// String to image and back.
    /// </summary>
    StrImg,

    /// <summary>
    /// SUN Rasterfile.
    /// </summary>
    Sun,

    /// <summary>
    /// Scalable Vector Graphics.
    /// </summary>
    Svg,

    /// <summary>
    /// Compressed Scalable Vector Graphics.
    /// </summary>
    Svgz,

    /// <summary>
    /// Text.
    /// </summary>
    Text,

    /// <summary>
    /// Truevision Targa image.
    /// </summary>
    Tga,

    /// <summary>
    /// EXIF Profile Thumbnail.
    /// </summary>
    Thumbnail,

    /// <summary>
    /// Tagged Image File Format.
    /// </summary>
    Tif,

    /// <summary>
    /// Tagged Image File Format.
    /// </summary>
    Tiff,

    /// <summary>
    /// Tagged Image File Format (64-bit).
    /// </summary>
    Tiff64,

    /// <summary>
    /// Tile image with a texture.
    /// </summary>
    Tile,

    /// <summary>
    /// PSX TIM.
    /// </summary>
    Tim,

    /// <summary>
    /// PS2 TIM2.
    /// </summary>
    Tm2,

    /// <summary>
    /// TrueType font collection.
    /// </summary>
    Ttc,

    /// <summary>
    /// TrueType font.
    /// </summary>
    Ttf,

    /// <summary>
    /// Text.
    /// </summary>
    Txt,

    /// <summary>
    /// Unicode Text format.
    /// </summary>
    Ubrl,

    /// <summary>
    /// Unicode Text format 6dot.
    /// </summary>
    Ubrl6,

    /// <summary>
    /// X-Motif UIL table.
    /// </summary>
    Uil,

    /// <summary>
    /// 16bit/pixel interleaved YUV.
    /// </summary>
    Uyvy,

    /// <summary>
    /// Truevision Targa image.
    /// </summary>
    Vda,

    /// <summary>
    /// VICAR rasterfile format.
    /// </summary>
    Vicar,

    /// <summary>
    /// Visual Image Directory.
    /// </summary>
    Vid,

    /// <summary>
    /// Open Web Media.
    /// </summary>
    WebM,

    /// <summary>
    /// Khoros Visualization image.
    /// </summary>
    Viff,

    /// <summary>
    /// VIPS image.
    /// </summary>
    Vips,

    /// <summary>
    /// Truevision Targa image.
    /// </summary>
    Vst,

    /// <summary>
    /// WebP Image Format.
    /// </summary>
    WebP,

    /// <summary>
    /// Wireless Bitmap (level 0) image.
    /// </summary>
    Wbmp,

    /// <summary>
    /// Windows Meta File.
    /// </summary>
    Wmf,

    /// <summary>
    /// Windows Media Video.
    /// </summary>
    Wmv,

    /// <summary>
    /// Word Perfect Graphics.
    /// </summary>
    Wpg,

    /// <summary>
    /// Sigma Camera RAW Format.
    /// </summary>
    X3f,

    /// <summary>
    /// X Windows system bitmap (black and white).
    /// </summary>
    Xbm,

    /// <summary>
    /// Constant image uniform color.
    /// </summary>
    Xc,

    /// <summary>
    /// GIMP image.
    /// </summary>
    Xcf,

    /// <summary>
    /// X Windows system pixmap (color).
    /// </summary>
    Xpm,

    /// <summary>
    /// Microsoft XML Paper Specification.
    /// </summary>
    Xps,

    /// <summary>
    /// Khoros Visualization image.
    /// </summary>
    Xv,

    /// <summary>
    /// Raw yellow samples.
    /// </summary>
    Y,

    /// <summary>
    /// The image format and characteristics.
    /// </summary>
    Yaml,

    /// <summary>
    /// Raw Y, Cb, and Cr samples.
    /// </summary>
    Ycbcr,

    /// <summary>
    /// Raw Y, Cb, Cr, and alpha samples.
    /// </summary>
    Ycbcra,

    /// <summary>
    /// CCIR 601 4:1:1 or 4:2:2.
    /// </summary>
    Yuv,
}
