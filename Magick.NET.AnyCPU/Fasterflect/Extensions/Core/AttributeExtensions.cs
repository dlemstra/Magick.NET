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

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for locating and retrieving attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        #region Attribute Lookup (Single)
        /// <summary>
        /// Gets the first <see href="Attribute"/> associated with the <paramref name="provider"/>.
        /// </summary>
        /// <returns>The first attribute found on the source element.</returns>
        public static Attribute Attribute( this ICustomAttributeProvider provider )
        {
            return provider.Attributes().FirstOrDefault();
        }

        /// <summary>
        /// Gets the first <see href="Attribute"/> of type <paramref name="attributeType"/> associated with the <paramref name="provider"/>.
        /// </summary>
        /// <returns>The first attribute found on the source element.</returns>
        public static Attribute Attribute( this ICustomAttributeProvider provider, Type attributeType )
        {
            return provider.Attributes( attributeType ).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first <see href="Attribute"/> of type <typeparamref name="T"/> associated with the <paramref name="provider"/>.
        /// </summary>
        /// <returns>The first attribute found on the source element.</returns>
        public static T Attribute<T>( this ICustomAttributeProvider provider ) where T : Attribute
        {
            return provider.Attributes<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the first <see href="Attribute"/> of type <typeparamref name="T"/> associated with the 
        /// enumeration value given in the <paramref name="provider"/> parameter.
        /// </summary>
        /// <typeparam name="T">The attribute type to search for.</typeparam>
        /// <param name="provider">An enumeration value on which to search for the attribute.</param>
        /// <returns>The first attribute found on the source.</returns>
        public static T Attribute<T>( this Enum provider ) where T : Attribute
        {
            return provider.Attribute( typeof(T) ) as T;
        }

        /// <summary>
        /// Gets the first <see href="Attribute"/> of type <paramref name="attributeType"/> associated with the 
        /// enumeration value given in the <paramref name="provider"/> parameter.
        /// </summary>
        /// <param name="provider">An enumeration value on which to search for the attribute.</param>
        /// <param name="attributeType">The attribute type to search for.</param>
        /// <returns>The first attribute found on the source.</returns>
        public static Attribute Attribute( this Enum provider, Type attributeType )
        {
            Type type = provider.GetType();
            MemberInfo info = type.Member( provider.ToString(), Flags.StaticAnyVisibility | Flags.DeclaredOnly );
            return info.Attribute( attributeType );
        }
        #endregion

        #region Attribute Lookup (Multiple)
        /// <summary>
        /// Gets the <see href="Attribute"/>s associated with the <paramref name="provider"/>. The resulting
        /// list of attributes can optionally be filtered by suppliying a list of <paramref name="attributeTypes"/>
        /// to include.
        /// </summary>
        /// <returns>A list of the attributes found on the source element. This value will never be null.</returns>
        public static IList<Attribute> Attributes( this ICustomAttributeProvider provider, params Type[] attributeTypes )
        {
			bool hasTypes = attributeTypes != null && attributeTypes.Length > 0;
            return provider.GetCustomAttributes( true ).Cast<Attribute>()
				.Where( attr => ! hasTypes || 
					    attributeTypes.Any( at => { Type type = attr.GetType();
													return at == type || at.IsSubclassOf(type); } ) ).ToList();
		}

        /// <summary>
        /// Gets all <see href="Attribute"/>s of type <typeparamref name="T"/> associated with the <paramref name="provider"/>.
        /// </summary>
        /// <returns>A list of the attributes found on the source element. This value will never be null.</returns>
        public static IList<T> Attributes<T>( this ICustomAttributeProvider provider ) where T : Attribute
        {
            return provider.GetCustomAttributes( typeof(T), true ).Cast<T>().ToList();
        }

        /// <summary>
        /// Gets the <see href="Attribute"/>s associated with the enumeration given in <paramref name="provider"/>. 
        /// </summary>
        /// <typeparam name="T">The attribute type to search for.</typeparam>
        /// <param name="provider">An enumeration on which to search for attributes of the given type.</param>
        /// <returns>A list of the attributes found on the supplied source. This value will never be null.</returns>
        public static IList<T> Attributes<T>( this Enum provider ) where T : Attribute
        {
            return provider.Attributes( typeof(T) ).Cast<T>().ToList();
        }

        /// <summary>
        /// Gets the <see href="Attribute"/>s associated with the enumeration given in <paramref name="provider"/>. 
        /// The resulting list of attributes can optionally be filtered by suppliying a list of <paramref name="attributeTypes"/>
        /// to include.
        /// </summary>
        /// <returns>A list of the attributes found on the supplied source. This value will never be null.</returns>
        public static IList<Attribute> Attributes( this Enum provider, params Type[] attributeTypes )
        {
            Type type = provider.GetType();
            MemberInfo info = type.Member( provider.ToString(), Flags.StaticAnyVisibility | Flags.DeclaredOnly );
            return info.Attributes( attributeTypes );
        }
        #endregion

        #region HasAttribute Lookup (Presence Detection)
        /// <summary>
        /// Determines whether the <paramref name="provider"/> element has an associated <see href="Attribute"/>
        /// of type <paramref name="attributeType"/>.
        /// </summary>
        /// <returns>True if the source element has the associated attribute, false otherwise.</returns>
        public static bool HasAttribute( this ICustomAttributeProvider provider, Type attributeType )
        {
            return provider.Attribute( attributeType ) != null;
        }

        /// <summary>
        /// Determines whether the <paramref name="provider"/> element has an associated <see href="Attribute"/>
        /// of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>True if the source element has the associated attribute, false otherwise.</returns>
        public static bool HasAttribute<T>( this ICustomAttributeProvider provider ) where T : Attribute
        {
            return provider.HasAttribute( typeof(T) );
        }

        /// <summary>
        /// Determines whether the <paramref name="provider"/> element has an associated <see href="Attribute"/>
        /// of any of the types given in <paramref name="attributeTypes"/>.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="attributeTypes">The list of attribute types to look for. If this list is <c>null</c> or
        /// empty an <see href="ArgumentException"/> will be thrown.</param>
        /// <returns>True if the source element has at least one of the specified attribute types, false otherwise.</returns>
        public static bool HasAnyAttribute( this ICustomAttributeProvider provider, params Type[] attributeTypes )
        {
            return provider.Attributes( attributeTypes ).Count() > 0;
        }

        /// <summary>
        /// Determines whether the <paramref name="provider"/> element has an associated <see href="Attribute"/>
        /// of all of the types given in <paramref name="attributeTypes"/>.
        /// </summary>
        /// <returns>True if the source element has all of the specified attribute types, false otherwise.</returns>
        public static bool HasAllAttributes( this ICustomAttributeProvider provider, params Type[] attributeTypes )
        {
			bool hasTypes = attributeTypes != null && attributeTypes.Length > 0;
            return ! hasTypes || attributeTypes.All( at => provider.HasAttribute( at ) );
        }
        #endregion

        #region MembersWith Lookup
        /// <summary>
        /// Gets all public and non-public instance members on the given <paramref name="type"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="memberTypes">The <see href="MemberTypes"/> to include in the search.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all fields and properties
        /// will be included in the result.</param>
        /// <returns>A list of all matching members on the type. This value will never be null.</returns>
		public static IList<MemberInfo> MembersWith( this Type type, MemberTypes memberTypes,
                                                     params Type[] attributeTypes )
        {
            return type.MembersWith( memberTypes, Flags.InstanceAnyVisibility, attributeTypes );
        }

        /// <summary>
        /// Gets all members of the given <paramref name="memberTypes"/> on the given <paramref name="type"/> 
        /// that match the specified <paramref name="bindingFlags"/> and are decorated with an
        /// <see href="Attribute"/> of the given type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="memberTypes">The <see href="MemberTypes"/> to include in the search.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <returns>A list of all matching members on the type. This value will never be null.</returns>
        public static IList<MemberInfo> MembersWith<T>( this Type type, MemberTypes memberTypes, Flags bindingFlags )
        {
            return type.MembersWith( memberTypes, bindingFlags, typeof(T) );
        }

        /// <summary>
        /// Gets all members on the given <paramref name="type"/> that match the specified 
        /// <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="memberTypes">The <see href="MemberTypes"/> to include in the search.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all fields and properties
        /// matching the given <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching members on the type. This value will never be null.</returns>
		public static IList<MemberInfo> MembersWith( this Type type, MemberTypes memberTypes, Flags bindingFlags,
                                                     params Type[] attributeTypes )
        {
			bool hasTypes = attributeTypes != null && attributeTypes.Length > 0;
            var query = from m in type.Members( memberTypes, bindingFlags )
                        where ! hasTypes || m.HasAnyAttribute( attributeTypes )
                        select m;
            return query.ToList();
        }
        #endregion

        #region FieldsAndPropertiesWith, FieldsWith, PropertiesWith
        /// <summary>
        /// Gets all public and non-public instance fields and properties on the given <paramref name="type"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all fields and properties
        /// will be included in the result.</param>
        /// <returns>A list of all matching fields and properties on the type. This value will never be null.</returns>
		public static IList<MemberInfo> FieldsAndPropertiesWith( this Type type, params Type[] attributeTypes )
        {
            return type.MembersWith( MemberTypes.Field | MemberTypes.Property, attributeTypes );
        }
		
        /// <summary>
        /// Gets all fields and properties on the given <paramref name="type"/> that match the specified 
        /// <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all fields and properties
        /// matching the given <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching fields and properties on the type. This value will never be null.</returns>
		public static IList<MemberInfo> FieldsAndPropertiesWith( this Type type, Flags bindingFlags, params Type[] attributeTypes )
        {
            return type.MembersWith( MemberTypes.Field | MemberTypes.Property, bindingFlags, attributeTypes );
        }

        /// <summary>
        /// Gets all fields on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all fields matching the given 
        /// <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching fields on the type. This value will never be null.</returns>
		public static IList<FieldInfo> FieldsWith( this Type type, Flags bindingFlags, params Type[] attributeTypes )
		{
            return type.MembersWith( MemberTypes.Field, bindingFlags, attributeTypes ).Cast<FieldInfo>().ToList();
        }

        /// <summary>
        /// Gets all properties on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all properties matching the given 
        /// <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching properties on the type. This value will never be null.</returns>
		public static IList<PropertyInfo> PropertiesWith( this Type type, Flags bindingFlags, params Type[] attributeTypes )
		{
            return type.MembersWith( MemberTypes.Property, bindingFlags, attributeTypes ).Cast<PropertyInfo>().ToList();
        }
        #endregion

        #region MethodsWith, ConstructorsWith
        /// <summary>
        /// Gets all methods on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all methods matching the given 
        /// <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching methods on the type. This value will never be null.</returns>
		public static IList<MethodInfo> MethodsWith( this Type type, Flags bindingFlags, params Type[] attributeTypes )
		{
            return type.MembersWith( MemberTypes.Method, bindingFlags, attributeTypes ).Cast<MethodInfo>().ToList();
        }

        /// <summary>
        /// Gets all constructors on the given <paramref name="type"/> that match the specified <paramref name="bindingFlags"/>.
        /// The resulting list of members can optionally be filtered by supplying a list of 
        /// <paramref name="attributeTypes"/>, in which case only members decorated with at least one of
        /// these will be included.
        /// </summary>
        /// <param name="type">The type on which to reflect.</param>
        /// <param name="bindingFlags">The <see cref="BindingFlags"/> or <see cref="Flags"/> combination 
        /// used to define the search behavior and result filtering.</param>
        /// <param name="attributeTypes">The optional list of attribute types with which members should
        /// be decorated. If this parameter is <c>null</c> or empty then all constructors matching the given 
        /// <paramref name="bindingFlags"/> will be included in the result.</param>
        /// <returns>A list of all matching constructors on the type. This value will never be null.</returns>
		public static IList<ConstructorInfo> ConstructorsWith( this Type type, Flags bindingFlags, params Type[] attributeTypes )
		{
            return type.MembersWith( MemberTypes.Constructor, bindingFlags, attributeTypes ).Cast<ConstructorInfo>().ToList();
        }
        #endregion

        #region MembersAndAttributes Lookup
        /// <summary>
        /// Gets a dictionary with all public and non-public instance members on the given <paramref name="type"/> 
        /// and their associated attributes. Only members of the given <paramref name="memberTypes"/> will
        /// be included in the result.
        /// The list of attributes associated with each member can optionally be filtered by supplying a list of
        /// <paramref name="attributeTypes"/>, in which case only members with at least one of these will be
        /// included in the result.
        /// </summary>
        /// <returns>An dictionary mapping all matching members to their associated attributes. This value
        /// will never be null. The attribute list associated with each member in the dictionary will likewise
        /// never be null.</returns>
        public static IDictionary<MemberInfo, List<Attribute>> MembersAndAttributes( this Type type,
                                                                                     MemberTypes memberTypes,
                                                                                     params Type[] attributeTypes )
        {
        	return type.MembersAndAttributes( memberTypes, Flags.InstanceAnyVisibility, null );
        }

        /// <summary>
        /// Gets a dictionary with all members on the given <paramref name="type"/> and their associated attributes.
        /// Only members of the given <paramref name="memberTypes"/> and matching <paramref name="bindingFlags"/> will
        /// be included in the result.
        /// The list of attributes associated with each member can optionally be filtered by supplying a list of
        /// <paramref name="attributeTypes"/>, in which case only members with at least one of these will be
        /// included in the result.
        /// </summary>
        /// <returns>An dictionary mapping all matching members to their associated attributes. This value
        /// will never be null. The attribute list associated with each member in the dictionary will likewise
        /// never be null.</returns>
        public static IDictionary<MemberInfo, List<Attribute>> MembersAndAttributes( this Type type,
                                                                                     MemberTypes memberTypes,
                                                                                     Flags bindingFlags,
                                                                                     params Type[] attributeTypes )
        {
            var members = from m in type.Members( memberTypes, bindingFlags )
                          let a = m.Attributes( attributeTypes )
                          where a.Count() > 0
                          select new { Member = m, Attributes = a.ToList() };
            return members.ToDictionary( m => m.Member, m => m.Attributes );
        }
        #endregion
    }
}