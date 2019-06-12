#!/usr/bin/perl -w
#
# Usage: $prog > ~/.magick/type.xml
#        $prog [-d] font1.ttf font2.ttf ... > type.xml
#        $prog -f ttf_font_file_list > type.xml
#
# Generate an ImageMagick font list "type.xml" file for ALL fonts
# currently on the local linux system. This includes
#     True Type Fonts (ttf)
#     Open Type Fonts (otf)
#     Ghostscript Adobe Fonts (afm)
#
# The output can be saved into files in the ".magick" sub-directory of
# your home, to be referenced by, or replacing the "type.xml" file.
#
# This file informs ImageMagick of the fonts location, font type, name and
# family.  It also trys its best to clean up the name to provide a 'nicer'
# one for you to identify the various fonts.
#
# On Linux system the scritp uses the "locate" command to find the fonts.
# If you recently added fonts you should run "updatedb" first.
#
# On MacOSX only the fonts stored in /Library/Fonts are looked for.
# That is the script is equivelent to doing...
#
#   find /Library/Fonts -type f -name '*.*' | \
#       imagick_type_gen -f - > type.xml
#
# When the "type.xml" font definitions file has been generated and
# installed, should then see a list of the fonts found with...
#    convert -list font
#
# And can use the fonts, by name, with commands like...
#    convert -font Candice -pointsize 72 label:Anthony  x:
#
# Instead of having to specifying TTF font file directly...
#    convert -font ~/lib/font/truetype/favoriate/candice.ttf \
#            -pointsize 72 label:Anthony  x:
#
# Also see the script "show_fonts" which displays a sample image either
# a IM defined font, or the given font files. The "graphics_utf" script
# may also be useful to look at specific UTF character sections of a
# specific font, such as Math symbols.
#
#  Anthony Thyssen  May 2003  - Updated Feburary 2017
#
###
#
# Example use, seperating system from personal fonts so the later
# overrides the former (of the same font exists)
#
# For example...
#
#   # Find personal fonts
#   find $HOME/fonts -type f -name '*.ttf' | \
#      imagick_type_gen -f - > ~/.magick/type-myfonts.xml
#
#   # Find System Fonts - then remove personal fonts from that list
#   sudo updatedb
#   imagick_type_gen > ~/.magick/type-system.xml
#   perl -00 -i -ne "m%glyphs=\"$HOME/% || print" ~/.magick/type-system.xml
#
# You can then include both of these files into a "~/.magick/type.xml"
# so that the person
#
#     <typemap>
#       <include file="type-system.xml" />
#       <include file="type-myfonts.xml" />
#     </typemap>
#
# Note that later defintions will override earlier ones. As such "myfonts"
# will override any "system" font.  However any fonts that the IM system
# configures in /usr/lib/ImageMagick-*/config/type*.xml will override the
# both the above definitions, though that is unlikely.
#
###
# Internal Notes and History
#
# Primary Source
#   http://www.imagemagick.org/Usage/scripts/imagick_type_gen
#
# Originally the script used an external tool to read TTF fonts, but now
# that is built-in thanks to   Peter N Lewis <peter@stairways.com.au>
#
# Before IM v6.1.2-3  the font list file was called "type.mgk" and
# not "type.xml".  And you would use "-list type" instead of "-list font"
#
# The original version of this script was found on
#   http://studio.imagemagick.org/pipermail/magick-users/2003-March/001703.html
# by  raptor <raptor@unacs.bg>, presumaibly around  March 2002
#
# Re-Write by Anthony Thyssen <anthony@cit.griffith.edu.au>, August 2002
# May 2003   Update with TTF family names
# Oct 2005   Update to use "getttinfo" if available
# Jan 2009   updated
# Feb 2017   merge bug report from Kazuyoshi Tlacaelel <kazu.dev@gmail.com>
#
# WARNING: Input arguments are NOT tested for correctness.
# This script represents a security risk if used ONLINE.
# I accept no responsiblity for misuse. Use at own risk.
#
###
use strict;
use FindBin;
my $PROGNAME = $FindBin::Script;
use Fcntl qw( O_RDONLY SEEK_SET );
binmode(STDOUT, ":utf8");
binmode(STDERR, ":utf8");

my $VERBOSE = 0; # verbose output of fonts found
my $DEBUG   = 0; # debug TTF file decoding
my $FILE    = 0; # read a font list from a file (maybe stdin)

# ======================================================================
# Font Handling...
# ======================================================================
#
# True Type fonts Handling
#
my $ttf_template = herefile( q{
  |   <type
  |      format="ttf"
  |      name="%s"
  |      glyphs="%s"
  |      />
  });
my $ttf_template_full = herefile( q{
  |   <type
  |      format="ttf"
  |      name="%s"
  |      fullname="%s"
  |      family="%s"
  |      glyphs="%s"
  |      />
  });

sub ttf_file_parse {
  #
  # Method for Parsing TTF files curtesy of
  #     Peter N Lewis <peter@stairways.com.au>
  #
  my $file = $_[0];
  my ( $font_family, $font_fullname, $font_psname ) = ( '','','','' );

  my ( $fh, $len );
  unless ( sysopen( $fh, $file, O_RDONLY ) ) {
    warn "Cannot open $file: $!\n";
    return;
  }
  my $header;
  unless ( sysread( $fh, $header, 12 ) ) {
    warn "Cant read header: $file";
    close($fh);
    return;
  }
  my ( $sfnt_version, $numTables, $searchRange, $entrySelector, $rangeShift
     ) = unpack( 'Nnnnn', $header );

  my $sfnt_version_code = unpack( 'A4', $header );
  unless (  $sfnt_version == 0x00010000
         || $sfnt_version_code eq 'true'
         || $sfnt_version_code eq 'typ1' ) {
    warn "TTF Version mismatch, not a basic TrueType font file: $file";
    close($fh);
    return;
  }

  print STDERR "TTF Table count: $numTables\n" if $DEBUG>=2;
  foreach ( 1..$numTables ) {
    my $table_entry;
    unless ( sysread( $fh, $table_entry, 16 ) ) {
      warn "Cant read master table $_ from $file";
      last;
    }

    my ( $table_tag, $table_checkSum, $table_offset, $table_length
       ) = unpack( 'A4NNN', $table_entry );
    print STDERR "Table: $table_tag\n" if $DEBUG>=2;
    $table_tag eq 'name' or next;

    my $table_header;
    sysseek( $fh, $table_offset, SEEK_SET ) or die "Can't seek: $file";
    sysread( $fh, $table_header, 6 );
    my ( $table_format, $table_count, $table_stringOffset
       ) = unpack( 'nnn', $table_header );
    print STDERR "Name Table Entries: $table_count\n" if $DEBUG>=2;
    my $table_base = $table_offset + 6;
    my $storage_base = $table_base + $table_count * 12;

    foreach my $index ( 1..$table_count ) {
      my $entry;
      sysseek( $fh, $table_base + ($index-1)*12, SEEK_SET )
          or die "Cant seek: $file";
      sysread( $fh, $entry, 12 );
      my ( $name_platformID, $name_encodingID, $name_languageID,
           $name_id, $name_length, $name_offset
         ) = unpack( 'nnnnnn', $entry );
      print STDERR "Index[$index]: ", join ( ", ",
              $name_platformID, $name_encodingID, $name_languageID,
              $name_id, $name_length, $name_offset ), "\n" if $DEBUG>=2;
      #
      # ID meanings : figured out from getttinfo
      #
      # Platform: 0=Apple  1=macintosh  3=microsoft
      # Encoding: 0=unicode(8) 1=unicode(16)
      # Language: 0=english  1033=English-US  1041=Japanese 2052=Chinese
      #
      next unless $name_languageID == 0
               || $name_languageID == 1033
               ;

      my $name;
      sysseek( $fh, $storage_base + $name_offset, SEEK_SET )
            or die "Cant seek: $file";
      sysread( $fh, $name, $name_length );

      # Decode UTF-16 to UTF-8 if nessary
      $name = pack("U*",unpack("n*", $name)) if $name_encodingID == 1;
      $name =~ s/\0//g;   # clean fonts use UTF-16 when it should be UTF-8
      print STDERR "$name\n" if $DEBUG>=2;

      $font_family = $name       if $name_id == 1;
      #font_subfamily = $name    if $name_id == 2;  # (EG: Regular)
      #font_identifier = $name   if $name_id == 3;  # Unique Name
      $font_fullname = $name     if $name_id == 4;
      #font_version = $name      if $name_id == 5;
      $font_psname = $name       if $name_id == 6;  # Postscipt Name
      #font_trademark = $name    if $name_id == 7;
      #font_manufacturer = $name if $name_id == 8;
      #font_designer = $name     if $name_id == 9;
    }
    last;  # found "name" table -- skip any other tables as irrelevent
  }
  close( $fh );
  return ( $font_family, $font_fullname, $font_psname );
}

sub ttf_name {
  my $file = shift;

  my ( $family, $fullname, $psname ) = &ttf_file_parse( $file );
  print STDERR "$file\n\t==> $family -- $fullname -- $psname\n" if $DEBUG;

  $fullname =~ s/[^\s\w-]//g;        # Check: Pepsi.ttf
  $fullname =~ s/^\s+//;
  $fullname =~ s/\s+$//;
  $fullname =~ s/(^|\s)-/$1/g;
  $fullname =~ s/-(\s|$)/$1/g;

  $family   =~ s/[^\s\w-]//g;        # Check: Pepsi.ttf
  $family   =~ s/^\s*//;
  $family   =~ s/\s*$//;
  $family   =~ s/\s*(MS|ITC)$//;     # font factory ititials
  $family   =~ s/^(MS|ITC)\s*//;
  $family   =~ s/\s*(FB|MT)\s*/ /;   # Check: MaturaScriptCapitals
  $family   =~ s/^Monotype\s*//;     # Check: Corsiva
  $family   =~ s/^AR PL\s*//;        # Check: gkai00mp.ttf
  $family   =~ s/\sBV$//;            # Check: CandyStore.ttf

  # Determine simple font name
  #   Junk/abbr decriptive strings, foundaries, etc
  #   Test with the fonts given
  my $name = ($fullname);
  $name =~ s/-/ /g;
  $name   =~ s/\s*(MS|ITC)$//;       # font factory ititials
  $name   =~ s/^(MS|ITC)\s*//;
  $name   =~ s/\s*(FB|MT)\s*/ /;     # Check: MaturaScriptCapitals
  $name   =~ s/^Monotype\s*//;       # Check: Corsiva
  $name   =~ s/^AR PL\s*//;          # Check: gkai00mp.ttf
  $name   =~ s/^TTF_//;              # Check: TattoEF.tff
  $name   =~ s/^HE_//;               # Check: Terminal.tff
  $name   =~ s/^KR\s//;              # Check: SimpleFleur*.ttf
  $name   =~ s/\sBT$//;              # Check: Amazone.ttf
  $name   =~ s/\sBV$//;              # Check: CandyStore.ttf
  $name   =~ s/\sFM$//;              # Check: CactusSandwich.ttf
  $name   =~ s/\sNFI$//;             # Check: Zreaks.ttf
  $name   =~ s/SSK$//;               # Check: BravoScript.ttf

  $name =~ s/Regular//g;             # Check: Gecko
  $name =~ s/\bPlain\b//g;           # Check: LittleGidding
  $name =~ s/\bReg\b//g;             # Check: agencyr.ttf
  $name =~ s/\bNormal\b//g;
  #$name =~ s/\bSans\b//g;
  $name =~ s/\bDemi\s*[Bb]old\b/Db/g;
  $name =~ s/\bCondensed\b/C/g;
  $name =~ s/\bBold\b/B/g;
  $name =~ s/\bItalic\b/I/g;
  $name =~ s/\bExtra[Bb]old\b/Xb/g;
  $name =~ s/\bBlack\b/Bk/g;
  $name =~ s/\bHeavy\b/H/g;
  $name =~ s/\bMedium\b/M/g;         # Check: gkai00mp.ttf
  $name =~ s/\bLight\b/L/g;
  $name =~ s/\bOblique\b/Ob/g;
  $name =~ s/\bUnregistered\b//g;    # Check: CandyCane.ttf

  $name =~ s/\s+//g;          # Remove all spaces

  # Special Case Renaming
  $name = "Dot" if $name eq "NationalFirstFontDotted";

  $fullname =~ s/\s+/ /g;
  $fullname =~ s/\s$//;
  $fullname =~ s/^\s//;

  # Failed to parse TTF file?
  return( ( $file =~ m/^.*\/(.*?).ttf$/ )[0] ) unless $name;

  return ($name, $fullname, $family);  # return the name if found!
}

sub do_ttf_font {
  my $file = shift;
  my (@ttf) = ttf_name($file);

  print STDERR join( ' - ', @ttf), "\n"  if $VERBOSE;
  printf $ttf_template, @ttf, $file       if @ttf == 1;
  printf $ttf_template_full, @ttf, $file  if @ttf == 3;
}


#---------------------------
#
# Open Type fonts
#
# I do not know how to parse OTF files (yet)
# so we are stuck with just the filebame
#
my $otf_template = herefile( q{
  |   <type
  |      format="otf"
  |      name="%s"
  |      glyphs="%s"
  |      />
  });

sub do_otf_font {
  my $file = shift;

  my $name = $file;
  $name =~ s/^.*\///;
  $name =~ s/\.otf$//;

  $name =~ s/-?Regular//g;
  $name =~ s/-?Bold?/B/g;
  $name =~ s/-?Italic/I/g;
  $name =~ s/-?Ita?/I/g;
  $name =~ s/-?Oblique/Ob/g;

  print STDERR join( ' - ', $name ), "\n"  if $VERBOSE;
  printf $otf_template, $name, $file;
}


#---------------------------
#
# Adobe Type fonts
#
# Get font name from the AFM file
my $afm_template_full = herefile( q{
  |   <type
  |      format="type1"
  |      name="%s"
  |      fullname="%s"
  |      family="%s"
  |      glyphs="%s"
  |      metrics="%s"
  |      />
  });

sub afm_name {
  my $file = shift;

  my( $name, $fullname, $family ) = ('','','');
  if ( open AFM, $file ) {
    while( <AFM> ) {
      chop; last if /^StartCharMetrics/;
      #$name     = $1  if /^FontName (.*)/;
      $fullname = $1  if /^FullName (.*)/;
      $family   = $1  if /^FamilyName (.*)/;
    }
    close AFM;

    $family =~ s/\s*L$//;    # just the stupid 'L'
    $fullname =~ s/\bL\b//;

    $name = $fullname;

    $name =~ s/\bRegular\b//;            # Junk/abbr decriptive strings
    $name =~ s/\bDemi\s*[Bb]old\b/Db/g;
    $name =~ s/\bDemi\s*[Oo]blique\b/Do/g;
    $name =~ s/\bCondensed\b/C/g;
    $name =~ s/\bBold\b/B/g;
    $name =~ s/\bItalic\b/I/g;
    $name =~ s/\bOblique\b/Ob/g;
    $name =~ s/\bExtra[Bb]old\b/Xb/g;
    $name =~ s/\bBlack\b/Bk/g;
    $name =~ s/\bHeavy\b/H/g;
    $name =~ s/\bMedium\b/M/g;
    $name =~ s/\bLight\b/L/g;

    $name =~ s/[-\s]+//g;
    $fullname =~ s/\s+/ /g;
    $fullname =~ s/\s$//g;
    $fullname =~ s/^\s//g;
  } else {
    warn "Cannot open $file";
  }

  return ($name, $fullname, $family ) if $name && $fullname && $family;
}

sub do_afm_fonts {
  my %atf;
  # locate abode font files
  map { my ($k) = m/^(.*?).pfb*$/i; $atf{lc($k)}{pfb} = $_ } locate('pfb');
  map { my ($k) = m/^(.*?).afm*$/i; $atf{lc($k)}{afm} = $_ } locate('afm');

  # for each Abode font where BOTH files were found.
  for my $key (keys %atf) {
    next unless $atf{$key}{pfb} && $atf{$key}{afm};
    my (@afm) = afm_name($atf{$key}{afm});

    #print STDERR join( ' - ', @afm), "\n"   if $VERBOSE;
    printf $afm_template_full, @afm, $atf{$key}{pfb}, $atf{$key}{afm}
                                                         if @afm == 3;
  }
}

# ======================================================================
# Option Handling...
# ======================================================================

sub Usage {
  print STDERR @_, "\n"  if @_;
  @ARGV = ( "$FindBin::Bin/$PROGNAME" );
  while( <> ) {
    next if 1 .. 2;
    last if /^###/;
    last unless /^#/;
    s/^#$//; s/^# //;
    last if /^$/;
    print STDERR $. == 3 ? "Usage: $_"
                         : "       $_";
  }
  print STDERR "For full manual use --help\n";
  exit 10;
}
sub Help {
  @ARGV = ( "$FindBin::Bin/$PROGNAME" );
  while( <> ) {
    next if $. == 1;
    last if /^###/;
    last unless /^#/;
    s/^#$//; s/^# //;
    print STDERR;
  }
  exit 10;
}

sub do_font {
  local $_ = shift;

  if ( /\.ttf$/i ) {
    do_ttf_font($_)
  }
  elsif ( $_ =~ /\.otf$/i ) {
    do_otf_font($_)
  }
  else {
    print STDERR "$PROGNAME: \"$_\" skipped, unknown suffix\n";
  }
}

sub locate {
  # Locate font files with the given suffix
  my $suffix = shift;

  if ( -d "/Library/Fonts" ) {  # We must be on MacOSX!
    # Use a 'find' to discover fonts in that directory only
    # Find method from Kazuyoshi Tlacaelel - Feb 2017
    return grep {  /\.$suffix$/i && -f $_ }
             split( "\n", `find /Library/Fonts -type f -name '*.*'` );
             # map { glob "$_" }  # Use glob to expand '?' in locate output
             #   split "\n", `locate -i '.$_[0]'`;  # alternative
  }
  # All linux system run updatedb and locate - so ask it
  return split('\0', `locate -0ier '\\.$suffix\$'`);

  #return grep {  /\.$_[0]$/i && -f $_ }
  #         map { glob "$_" }
  #         split "\n", `find /Library/Fonts -name '*.*'`;
}

sub herefile {  # Handle a multi-line quoted indented string
  my $string = shift;
  $string =~ s/^\s*//;        # remove start spaces
  $string =~ s/^\s*\| ?//gm;  # remove line starts
  $string =~ s/\s*$/\n/g;     # remove end spaces
  return $string;
}

OPTION:  # Multi-switch option handling
while( @ARGV && $ARGV[0] =~ s/^-(?=.)// ) {
  $_ = shift; {
    m/^$/  && do { next };       # Next option
    m/^-$/ && do { last };       # End of options '--'
    m/^\?/ && do { Help };       # Usage Help     '-?'
    m/^-?(help|doc|man|manual)$/ && Help;  # Print help manual comments

    s/^d// && do { $DEBUG++;   redo };    # \
    s/^v// && do { $VERBOSE++; redo };    # /
    s/^f// && do { $FILE++; redo };    # /

    Usage( "$PROGNAME: Unknown Option \"-$_\"" );
  } continue { next OPTION }; last OPTION;
}

print herefile( q{
  | <?xml version="1.0"?>
  | <typemap>
});

if ( $FILE ) {
  while( <> ) {
    s/#.*$//;         # ignore comments
    s/\s+$//;         # remove end of line spaces
    next if /^$/;     # skip blank lines

    do_font($_);
  }
}
elsif ( @ARGV ) {
  # TTF font filenames as arguments
  for ( @ARGV ) {
    do_font($_);
  }
} else {

  # Generate the "type.xml" file using "locate"
  print STDERR "Doing TTF fonts\n" if $VERBOSE;
  for ( locate('ttf') ) {
    do_ttf_font($_);
  }
  print STDERR "Doing OTF fonts\n" if $VERBOSE;
  for ( locate('otf') ) {
    do_otf_font($_);
  }
  print STDERR "Doing ATM fonts\n" if $VERBOSE;
  do_afm_fonts();
}

print "</typemap>\n";


# ----------------------------------------------------------------------------
