//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class XsdGenerator
	{
		//===========================================================================================
		private MagickNET _MagickNET;
		private QuantumDepth _Depth;
		private XDocument _Document;
		private XNamespace _Namespace = "http://www.w3.org/2001/XMLSchema";
		private XmlNamespaceManager _Namespaces;
		//===========================================================================================
		private XsdGenerator(QuantumDepth depth)
		{
			_Depth = depth;
			_MagickNET = new MagickNET(depth);

			_Namespaces = new XmlNamespaceManager(new NameTable());
			_Namespaces.AddNamespace("xs", _Namespace.ToString());
		}
		//===========================================================================================
		private void AddEnumValues(Type enumType, XElement restriction)
		{
			foreach (string name in Enum.GetNames(enumType))
			{
				if (name == "Undefined")
					continue;

				restriction.Add(new XElement(_Namespace + "enumeration",
										new XAttribute("value", name)));
			}
		}
		//===========================================================================================
		private void AddMethods(XElement annotation)
		{
			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMethods("MagickImage"))
			{
				annotation.AddBeforeSelf(CreateElement(overloads));
			}
		}
		//===========================================================================================
		private void AddParameterAttributes(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
		{
			foreach (var paramElem in from parameter in parameters
											  let typeName = GetXsdAttributeType(parameter)
											  where typeName != null
											  select new
											  {
												  Name = parameter.Name,
												  TypeName = typeName,
												  IsRequired = requiredParameters.Contains(parameter.Name)
											  })
			{
				XElement attribute = new XElement(_Namespace + "attribute",
												new XAttribute("name", paramElem.Name));

				if (paramElem.IsRequired)
					attribute.Add(new XAttribute("use", "required"));

				attribute.Add(new XAttribute("type", paramElem.TypeName));

				complexType.Add(attribute);
			}
		}
		//===========================================================================================
		private void AddParameterElements(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
		{
			XElement sequence = new XElement(_Namespace + "sequence");

			foreach (var paramElem in from parameter in parameters
											  let typeName = GetXsdElementType(parameter)
											  where typeName != null
											  select new
											  {
												  Name = parameter.Name,
												  TypeName = typeName,
												  IsRequired = requiredParameters.Contains(parameter.Name)
											  })
			{
				XElement element = new XElement(_Namespace + "element",
											new XAttribute("name", paramElem.Name));

				if (!paramElem.IsRequired)
					element.Add(new XAttribute("minOccurs", "0"));

				element.Add(new XAttribute("type", paramElem.TypeName));

				sequence.Add(element);
			}

			if (sequence.HasElements)
				complexType.Add(sequence);
		}
		//===========================================================================================
		private void AddProperties(XElement annotation)
		{
			foreach (PropertyInfo property in _MagickNET.GetProperties("MagickImage"))
			{
				annotation.AddBeforeSelf(CreateElement(property));
			}
		}
		//===========================================================================================
		private XElement CreateElement(MethodInfo[] overloads)
		{
			XElement element = new XElement(_Namespace + "element",
										new XAttribute("name", GetXsdName(overloads[0])));

			int totalParameters = (from method in overloads
										  from parameter in method.GetParameters()
										  select parameter).Count();

			if (totalParameters > 0)
			{
				XElement complexType = new XElement(_Namespace + "complexType");

				string[] requiredParameters = (from method in overloads
														 from parameter in method.GetParameters()
														 group parameter by parameter.Name into g
														 where g.Count() == overloads.Length
														 select g.Key).ToArray();

				ParameterInfo[] parameters = (from method in overloads
														from parameter in method.GetParameters()
														select parameter).DistinctBy(p => p.Name).ToArray();

				AddParameterElements(complexType, parameters, requiredParameters);
				AddParameterAttributes(complexType, parameters, requiredParameters);

				element.Add(complexType);
			}

			return element;
		}
		//===========================================================================================
		private XElement CreateElement(PropertyInfo property)
		{
			string name = GetXsdName(property);

			XElement complexType = new XElement(_Namespace + "complexType");

			string attributeTypeName = GetXsdAttributeType(property);

			if (attributeTypeName != null)
			{
				complexType.Add(new XElement(_Namespace + "attribute",
										new XAttribute("name", "value"),
										new XAttribute("use", "required"),
										new XAttribute("type", attributeTypeName)));
			}
			else
			{
				string elementTypeName = GetXsdElementType(property);

				complexType.Add(new XElement(_Namespace + "sequence",
										new XElement(_Namespace + "element",
											new XAttribute("name", elementTypeName),
											new XAttribute("type", elementTypeName))));
			}

			return new XElement(_Namespace + "element", new XAttribute("name", name), complexType);
		}
		//===========================================================================================
		private XElement CreateEnumElement(Type enumType)
		{
			XElement restriction = new XElement(_Namespace + "restriction", new XAttribute("base", "xs:NMTOKEN"));
			AddEnumValues(enumType, restriction);

			return new XElement(_Namespace + "simpleType",
						new XAttribute("name", enumType.Name),
						restriction);
		}
		//===========================================================================================
		private static void Generate(QuantumDepth depth)
		{
			XsdGenerator generator = new XsdGenerator(depth);
			generator.WriteDocument();
		}
		//===========================================================================================
		private static string GetXsdAttributeType(Type type)
		{
			string typeName = MagickNET.GetTypeName(type);

			switch (typeName)
			{
				case "Encoding^":
				case "String^":
					return "xs:string";
				case "Percentage":
					return "xs:double";
				case "bool":
					return "xs:boolean";
				case "int":
				case "double":
					return "xs:" + typeName;
				case "Magick::Quantum":
					return "quantum";
				case "MagickColor^":
					return "color";
				case "MagickGeometry^":
				case "MagickImage^":
					return null;
				case "Channels":
				case "ClassType":
				case "ColorSpace":
				case "ColorType":
				case "CompositeOperator":
				case "DistortMethod":
				case "Endian":
				case "EvaluateOperator":
				case "FillRule":
				case "FilterType":
				case "GifDisposeMethod":
				case "Gravity":
				case "LineCap":
				case "LineJoin":
				case "MagickFormat":
				case "NoiseType":
				case "OrientationType":
				case "PaintMethod":
				case "RenderingIntent":
				case "Resolution":
				case "SparseColorMethod":
				case "VirtualPixelMethod":
					return typeName;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		private static string GetXsdElementType(Type type)
		{
			string typeName = MagickNET.GetTypeName(type);

			switch (typeName)
			{
				case "MagickGeometry^":
					return "geometry";
				case "MagickImage^":
					return "read";
				default:
					return null;
			}
		}
		//===========================================================================================
		private static string GetXsdName(string name)
		{
			if (name.ToUpperInvariant() == name)
				return name.ToLowerInvariant();

			return char.ToLowerInvariant(name[0]) + name.Substring(1);
		}
		//===========================================================================================
		private void ReplaceActions(XElement annotation)
		{
			AddProperties(annotation);
			AddMethods(annotation);

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceAnnotations()
		{
			XElement[] annotations = (from a in _Document.XPathSelectElements("//xs:annotation", _Namespaces)
											  select a).ToArray();

			foreach (XElement annotation in annotations)
			{
				switch (annotation.Attribute("id").Value)
				{
					case "actions":
						ReplaceActions(annotation);
						break;
					case "color":
						ReplaceColor(annotation);
						break;
					case "enums":
						ReplaceEnums(annotation);
						break;
					case "quantum":
						ReplaceQuantum(annotation);
						break;
					default:
						throw new NotImplementedException();
				}
			}
		}
		//===========================================================================================
		private void ReplaceEnums(XElement annotation)
		{
			foreach (Type enumType in _MagickNET.Enums)
			{
				annotation.AddBeforeSelf(CreateEnumElement(enumType));
			}

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceColor(XElement annotation)
		{
			XElement restriction = new XElement(_Namespace + "restriction",
											new XAttribute("base", "xs:string"));

			if (_Depth >= QuantumDepth.Q8)
			{
				restriction.Add(new XElement(_Namespace + "pattern",
										new XAttribute("value", "#([0-9a-fA-F]{3,4})")));

				restriction.Add(new XElement(_Namespace + "pattern",
										new XAttribute("value", "#([0-9a-fA-F]{2}){3,4}")));
			}

			if (_Depth >= QuantumDepth.Q16)
				restriction.Add(new XElement(_Namespace + "pattern",
										new XAttribute("value", "#([0-9a-fA-F]{4}){3,4}")));

			annotation.ReplaceWith(new XElement(_Namespace + "simpleType",
											new XAttribute("name", "color"),
											restriction));
		}
		//===========================================================================================
		private void ReplaceQuantum(XElement annotation)
		{
			string typeName;

			switch (_Depth)
			{
				case QuantumDepth.Q16:
					typeName = "xs:unsignedShort";
					break;
				case QuantumDepth.Q8:
					typeName = "xs:unsignedByte";
					break;
				default:
					throw new NotImplementedException();
			}

			annotation.ReplaceWith(new XElement(_Namespace + "simpleType",
											new XAttribute("name", "quantum"),
											new XElement(_Namespace + "restriction", new XAttribute("base", typeName))));
		}
		//===========================================================================================
		private void Write()
		{
			string outputFile = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\Resources\Release" + _Depth + @"\MagickScript.xsd");
			Console.WriteLine("Creating: " + outputFile);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";

			using (XmlWriter writer = XmlWriter.Create(outputFile, settings))
			{
				_Document.Save(writer);
			}
		}
		//===========================================================================================
		private void WriteDocument()
		{
			_Document = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Xsd\MagickScript.xsd");
			_Document.DescendantNodes().OfType<XComment>().Remove();

			ReplaceAnnotations();

			Write();
		}
		//===========================================================================================
		public static void Generate()
		{
			Generate(QuantumDepth.Q8);
			Generate(QuantumDepth.Q16);
		}
		//===========================================================================================
		public static string GetXsdAttributeType(ParameterInfo parameter)
		{
			return GetXsdAttributeType(parameter.ParameterType);
		}
		//===========================================================================================
		public static string GetXsdAttributeType(PropertyInfo property)
		{
			return GetXsdAttributeType(property.PropertyType);
		}
		//===========================================================================================
		public static string GetXsdElementType(ParameterInfo parameter)
		{
			return GetXsdElementType(parameter.ParameterType);
		}
		//===========================================================================================
		public static string GetXsdElementType(PropertyInfo property)
		{
			return GetXsdElementType(property.PropertyType);
		}
		//===========================================================================================
		public static string GetXsdName(MemberInfo member)
		{
			return GetXsdName(member.Name);
		}
		//===========================================================================================
		public static string GetXsdName(ParameterInfo parameter)
		{
			return GetXsdName(parameter.Name);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
