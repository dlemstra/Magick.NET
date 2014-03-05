#region License
// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fasterflect
{
	/// <summary>
	/// This class encapsulates common <see cref="BindingFlags"/> combinations and provides various
	/// additional Fasterflect-specific flags to further tailor the lookup experience.
	/// </summary>
	public struct Flags
	{
		private readonly long flags;
		private static readonly Dictionary<Flags, string> flagNames = new Dictionary<Flags, string>( 64 );

		#region Constructors
		private Flags( long flags )
		{
			this.flags = flags;
		}

		static Flags()
		{
			foreach( BindingFlags flag in Enum.GetValues( typeof(BindingFlags) ) )
			{
				if( flag != BindingFlags.Default )
				{
					flagNames[ new Flags( (long) flag ) ] = flag.ToString();
				}
			}
			flagNames[ PartialNameMatch ] = "PartialNameMatch"; // new Flags( 1L << 32 );
			flagNames[ TrimExplicitlyImplemented ] = "TrimExplicitlyImplemented"; // new Flags( 1L << 33 );
			flagNames[ ExcludeExplicitlyImplemented ] = "ExcludeExplicitlyImplemented"; // = new Flags( 1L << 34 );
			flagNames[ ExcludeBackingMembers ] = "ExcludeBackingMembers"; // = new Flags( 1L << 35 );
			flagNames[ IgnoreParameterModifiers ] = "IgnoreParameterModifiers"; // = new Flags( 1L << 36 );
			flagNames[ ExcludeHiddenMembers ] = "ExcludeHiddenMembers"; // = new Flags( 1L << 37 );

			// not yet supported:
			//flagNames[ VisibilityMatch ] = "VisibilityMatch"; // = new Flags( 1L << 55 );
			//flagNames[ Private ] = "Private"; //   = new Flags( 1L << 56 );
			//flagNames[ Protected ] = "Protected"; // = new Flags( 1L << 57 );
			//flagNames[ Internal ] = "Internal"; //  = new Flags( 1L << 58 );

			//flagNames[ ModifierMatch ] = "ModifierMatch"; // = new Flags( 1L << 59 );
			//flagNames[ Abstract ] = "Abstract"; //  = new Flags( 1L << 60 );
			//flagNames[ Virtual ] = "Virtual"; //   = new Flags( 1L << 61 );
			//flagNames[ Override ] = "Override"; //  = new Flags( 1L << 62 );
			//flagNames[ New ] = "New"; //      = new Flags( 1L << 63 );
		}
		#endregion

		#region Flags Selectors

		#region BindingFlags
		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.Default"/> value.
		/// </summary>
		public static readonly Flags None = new Flags( (long) BindingFlags.Default );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.IgnoreCase"/> value.
		/// </summary>
		public static readonly Flags IgnoreCase = new Flags( (long) BindingFlags.IgnoreCase );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.DeclaredOnly"/> value.
		/// </summary>
		public static readonly Flags DeclaredOnly = new Flags( (long) BindingFlags.DeclaredOnly );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.ExactBinding"/> value. 
		/// Note that this value is respected even in cases where normal Reflection calls would ignore it.
		/// </summary>
		public static readonly Flags ExactBinding = new Flags( (long) BindingFlags.ExactBinding );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.Public"/> value.
		/// </summary>
		public static readonly Flags Public = new Flags( (long) BindingFlags.Public );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.NonPublic"/> value.
		/// </summary>
		public static readonly Flags NonPublic = new Flags( (long) BindingFlags.NonPublic );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.Instance"/> value.
		/// </summary>
		public static readonly Flags Instance = new Flags( (long) BindingFlags.Instance );

		/// <summary>
		/// This value corresponds to the <see href="BindingFlags.Static"/> value.
		/// </summary>
		public static readonly Flags Static = new Flags( (long) BindingFlags.Static );
		#endregion

		#region FasterflectFlags
		/// <summary>
		/// If this option is specified the search for a named member will perform a partial match instead
		/// of an exact match. If <see href="TrimExplicitlyImplemented"/> is specified the trimmed name is
		/// used instead of the original member name. If <see href="IgnoreCase"/> is specified the 
		/// comparison uses <see href="StringComparison.OrginalIgnoreCase"/> and otherwise
		/// uses <see href="StringComparison.Ordinal"/>.
		/// </summary>
		public static readonly Flags PartialNameMatch = new Flags( 1L << 32 );

		/// <summary>
		/// If this option is specified the search for a named member will strip off the namespace and
		/// interface name from explicitly implemented interface members before applying any comparison
		/// operations.
		/// </summary>
		public static readonly Flags TrimExplicitlyImplemented = new Flags( 1L << 33 );

		/// <summary>
		/// If this option is specified the search for members will exclude explicitly implemented
		/// interface members.
		/// </summary>
		public static readonly Flags ExcludeExplicitlyImplemented = new Flags( 1L << 34 );

		/// <summary>
		/// If this option is specified all members that are backers for another member, such as backing
		/// fields for automatic properties or get/set methods for properties, will be excluded from the 
		/// result.
		/// </summary>
		public static readonly Flags ExcludeBackingMembers = new Flags( 1L << 35 );

		/// <summary>
		/// If this option is specified the search for methods will avoid checking whether parameters
		/// have been declared as ref or out. This allows you to locate a method by its signature
		/// without supplying the exact details for every parameter.
		/// </summary>
		public static readonly Flags IgnoreParameterModifiers = new Flags( 1L << 36 );

		/// <summary>
		/// If this option is specified all members that are have either an override or are being 
		/// shadowed/hidden (by another member declared using the new keyword) will be excluded from the 
		/// result. This is implemented by simple name matching of members, ensuring that only the first 
		/// member with a given name is included in the result. Note that this overlaps partially with
		/// the behavior of <see cref="ExcludeBackingMembers"/>, however, an implementation that excludes 
		/// members based on the presence of the new keyword does not seem to be possible and would
		/// in any case be much slower.
		/// </summary>
		public static readonly Flags ExcludeHiddenMembers = new Flags( 1L << 37 );

		#region For The Future
		///// <summary>
		///// If this option is specified only members with one (or more) of the specified visibility 
		///// flags will be included in the result.
		///// </summary>
		//public static readonly Flags VisibilityMatch = new Flags( 1L << 55 );
		///// <summary>
		///// Visibility flags
		///// </summary>
		//public static readonly Flags Private   = new Flags( 1L << 56 );
		//public static readonly Flags Protected = new Flags( 1L << 57 );
		//public static readonly Flags Internal  = new Flags( 1L << 58 );

		///// <summary>
		///// If this option is specified only members with one (or more) of the specified modifier 
		///// flags will be included in the result.
		///// </summary>
		//public static readonly Flags ModifierMatch = new Flags( 1L << 59 );
		///// <summary>
		///// Modifier flags
		///// </summary>
		//public static readonly Flags Abstract  = new Flags( 1L << 60 );
		//public static readonly Flags Virtual   = new Flags( 1L << 61 );
		//public static readonly Flags Override  = new Flags( 1L << 62 );
		//public static readonly Flags New       = new Flags( 1L << 63 );
		#endregion

		#endregion

		#region Common Selections
		/// <summary>
		/// Search criteria encompassing all public and non-public members, including base members.
		/// Note that you also need to specify either the Instance or Static flag.
		/// </summary>
		public static readonly Flags AnyVisibility = Public | NonPublic;

		/// <summary>
		/// Search criteria encompassing all public instance members, including base members.
		/// </summary>
		public static readonly Flags InstancePublic = Public | Instance;

		/// <summary>
		/// Search criteria encompassing all non-public instance members, including base members.
		/// </summary>
		public static readonly Flags InstancePrivate = NonPublic | Instance;

		/// <summary>
		/// Search criteria encompassing all public and non-public instance members, including base members.
		/// </summary>
		public static readonly Flags InstanceAnyVisibility = AnyVisibility | Instance;

		/// <summary>
		/// Search criteria encompassing all public static members, including base members.
		/// </summary>
		public static readonly Flags StaticPublic = Public | Static;

		/// <summary>
		/// Search criteria encompassing all non-public static members, including base members.
		/// </summary>
		public static readonly Flags StaticPrivate = NonPublic | Static;

		/// <summary>
		/// Search criteria encompassing all public and non-public static members, including base members.
		/// </summary>
		public static readonly Flags StaticAnyVisibility = AnyVisibility | Static;

		/// <summary>
		/// Search criteria encompassing all public instance members, excluding base members.
		/// </summary>
		public static readonly Flags InstancePublicDeclaredOnly = InstancePublic | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all non-public instance members, excluding base members.
		/// </summary>
		public static readonly Flags InstancePrivateDeclaredOnly = InstancePrivate | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all public and non-public instance members, excluding base members.
		/// </summary>
		public static readonly Flags InstanceAnyDeclaredOnly = InstanceAnyVisibility | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all public static members, excluding base members.
		/// </summary>
		public static readonly Flags StaticPublicDeclaredOnly = StaticPublic | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all non-public static members, excluding base members.
		/// </summary>
		public static readonly Flags StaticPrivateDeclaredOnly = StaticPrivate | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all public and non-public static members, excluding base members.
		/// </summary>
		public static readonly Flags StaticAnyDeclaredOnly = StaticAnyVisibility | DeclaredOnly;

		/// <summary>
		/// Search criteria encompassing all members, including base and static members.
		/// </summary>
		public static readonly Flags StaticInstanceAnyVisibility = InstanceAnyVisibility | Static;
		#endregion

		#region Intellisense Convenience Flags
		/// <summary>
		/// Search criteria encompassing all public and non-public instance members, including base members.
		/// </summary>
		public static readonly Flags Default = InstanceAnyVisibility;

		/// <summary>
		/// Search criteria encompassing all members (public and non-public, instance and static), including base members.
		/// </summary>
		public static readonly Flags AllMembers = StaticInstanceAnyVisibility;
		#endregion

		#endregion

		#region Helper Methods
		/// <summary>
		/// Returns true if all values in the given <paramref name="mask"/> are set in the current Flags instance.
		/// </summary>
		public bool IsSet( BindingFlags mask )
		{
			return ((BindingFlags) flags & mask) == mask;
		}

		/// <summary>
		/// Returns true if all values in the given <paramref name="mask"/> are set in the current Flags instance.
		/// </summary>
		public bool IsSet( Flags mask )
		{
			return (flags & mask) == mask;
		}

		/// <summary>
		/// Returns true if at least one of the values in the given <paramref name="mask"/> are set in the current Flags instance.
		/// </summary>
		public bool IsAnySet( BindingFlags mask )
		{
			return ((BindingFlags) flags & mask) != 0;
		}

		/// <summary>
		/// Returns true if at least one of the values in the given <paramref name="mask"/> are set in the current Flags instance.
		/// </summary>
		public bool IsAnySet( Flags mask )
		{
			return (flags & mask) != 0;
		}

		/// <summary>
		/// Returns true if all values in the given <paramref name="mask"/> are not set in the current Flags instance.
		/// </summary>
		public bool IsNotSet( BindingFlags mask )
		{
			return ((BindingFlags) flags & mask) == 0;
		}

		/// <summary>
		/// Returns true if all values in the given <paramref name="mask"/> are not set in the current Flags instance.
		/// </summary>
		public bool IsNotSet( Flags mask )
		{
			return (flags & mask) == 0;
		}

		/// <summary>
		/// Returns a new Flags instance with the union of the values from <paramref name="flags"/> and 
		/// <paramref name="mask"/> if <paramref name="condition"/> is true, and otherwise returns the
		/// supplied <paramref name="flags"/>.
		/// </summary>
		public static Flags SetIf( Flags flags, Flags mask, bool condition )
		{
			return condition ? flags | mask : flags;
		}

		/// <summary>
		/// Returns a new Flags instance with the union of the values from <paramref name="flags"/> and 
		/// <paramref name="mask"/> if <paramref name="condition"/> is true, and otherwise returns a new 
		/// Flags instance with the values from <paramref name="flags"/> that were not in <paramref name="mask"/>.
		/// </summary>
		public static Flags SetOnlyIf( Flags flags, Flags mask, bool condition )
		{
			return condition ? flags | mask : (Flags) (flags & ~mask);
		}

		/// <summary>
		/// Returns a new Flags instance returns a new Flags instance with the values from <paramref name="flags"/> 
		/// that were not in <paramref name="mask"/> if <paramref name="condition"/> is true, and otherwise returns
		/// the supplied <paramref name="flags"/>.
		/// </summary>
		public static Flags ClearIf( Flags flags, Flags mask, bool condition )
		{
			return condition ? (Flags) (flags & ~mask) : flags;
		}
		#endregion

		#region Equals
		/// <summary>
		/// Compares the current Flags instance to the given <paramref name="obj"/>.
		/// Returns true only if <paramref name="obj"/> is a Flags instance representing an identical selection.
		/// </summary>
		public override bool Equals( object obj )
		{
			return obj != null && obj.GetType() == typeof(Flags) && flags == ((Flags) obj).flags;
		}

		/// <summary>
		/// Produces a unique hash code for the current Flags instance.
		/// </summary>
		public override int GetHashCode()
		{
			return flags.GetHashCode();
		}
		#endregion

		#region Operators
		/// <summary>
		/// Produces a new Flags instance with the values from <paramref name="f1"/> that were not in <paramref name="f2"/>.
		/// </summary>
		public static Flags operator -( Flags f1, Flags f2 )
		{
			return new Flags( f1.flags & ~f2.flags );
		}

		/// <summary>
		/// Produces a new Flags instance with the values from the union of <paramref name="f1"/> and <paramref name="f2"/>.
		/// </summary>
		public static Flags operator |( Flags f1, Flags f2 )
		{
			return new Flags( f1.flags | f2.flags );
		}

		/// <summary>
		/// Produces a new Flags instance with the values from the intersection of <paramref name="f1"/> and <paramref name="f2"/>.
		/// </summary>
		public static Flags operator &( Flags f1, Flags f2 )
		{
			return new Flags( f1.flags & f2.flags );
		}

		/// <summary>
		/// Compares two Flags instances and returns true if they represent identical selections.
		/// </summary>
		public static bool operator ==( Flags f1, Flags f2 )
		{
			return f1.flags == f2.flags;
		}

		/// <summary>
		/// Compares two Flags instances and returns true if they represent different selections.
		/// </summary>
		public static bool operator !=( Flags f1, Flags f2 )
		{
			return f1.flags != f2.flags;
		}
		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts from BindingFlags to Flags.
		/// </summary>
		public static implicit operator Flags( BindingFlags m )
		{
			return new Flags( (long) m );
		}

		/// <summary>
		/// Converts from long to Flags.
		/// </summary>
		public static explicit operator Flags( long m )
		{
			return new Flags( m );
		}

		/// <summary>
		/// Converts from Flags to BindingFlags.
		/// </summary>
		public static implicit operator BindingFlags( Flags m )
		{
			return (BindingFlags) m.flags;
		}

		/// <summary>
		/// Converts from Flags to long.
		/// </summary>
		public static implicit operator long( Flags m )
		{
			return m.flags;
		}
		#endregion

		#region ToString
		/// <summary>
		/// Returns a string representation of the Flags values selected by the current instance.
		/// </summary>
		public override string ToString()
		{
			Flags @this = this;
			List<string> names = flagNames.Where( kvp => @this.IsSet( kvp.Key ) )
										  .Select( kvp => kvp.Value )
										  .OrderBy( n => n ).ToList();
			int index = 0;
			var sb = new StringBuilder();
			names.ForEach( n => sb.AppendFormat( "{0}{1}", n, ++index < names.Count ? " | " : "" ) );
			return sb.ToString();
		}
		#endregion
	}
}
