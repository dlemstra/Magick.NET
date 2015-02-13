//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.Generic;
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
		private void AddArguments(XElement element, IEnumerable<MethodBase> methods)
		{
			string[] requiredParameters = (from method in methods
													 from parameter in method.GetParameters()
													 group parameter by parameter.Name into g
													 where g.Count() == methods.Count()
													 select g.Key).ToArray();

			ParameterInfo[] parameters = (from method in methods
													from parameter in method.GetParameters()
													orderby parameter.Name
													select parameter).DistinctBy(p => p.Name).ToArray();

			AddParameterElements(element, parameters, requiredParameters);
			AddParameterAttributes(element, parameters, requiredParameters);
		}
		//===========================================================================================
		private void AddClass(XElement parent, string typeName)
		{
			AddClassElements(parent, _MagickNET.GetProperties(typeName), _MagickNET.GetMethods(typeName));
			AddClassAttributes(parent, _MagickNET.GetProperties(typeName));
		}
		//===========================================================================================
		private void AddEnumValues(Type enumType, XElement restriction)
		{
			foreach (string name in Enum.GetNames(enumType).OrderBy(n => n))
			{
				if (name == "Undefined")
					continue;

				restriction.Add(new XElement(_Namespace + "enumeration",
										new XAttribute("value", name)));
			}
		}
		//===========================================================================================
		private void AddMagickImageMethods(XElement annotation)
		{
			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMagickImageMethods())
			{
				annotation.AddBeforeSelf(CreateElement(overloads));
			}
		}
		//===========================================================================================
		private void AddMagickImageCollectionMethods(XElement annotation)
		{
			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMagickImageCollectionMethods())
			{
				annotation.AddBeforeSelf(CreateElement(overloads));
			}
		}
		//===========================================================================================
		private void AddMagickImageCollectionResultMethods(XElement annotation)
		{
			foreach (MethodInfo[] overloads in _MagickNET.GetGroupedMagickImageCollectionResultMethods())
			{
				annotation.AddBeforeSelf(CreateElement(overloads));
			}
		}
		//===========================================================================================
		private void AddMagickImageProperties(XElement annotation)
		{
			foreach (PropertyInfo property in _MagickNET.GetMagickImageProperties())
			{
				annotation.AddBeforeSelf(CreateElement(property));
			}
		}
		//===========================================================================================
		private void AddMethods(XElement element, IEnumerable<MethodBase> methods)
		{
			ParameterInfo[] parameters = (from method in methods
													from parameter in method.GetParameters()
													select parameter).ToArray();
			if (parameters.Length == 0)
			{
				element.Add(new XAttribute("type", "empty"));
			}
			else
			{
				if (methods.Count() == 1 && IsTypedElement(parameters))
				{
					element.Add(new XAttribute("type", _MagickNET.GetXsdName(parameters[0])));
				}
				else
				{
					XElement complexType = new XElement(_Namespace + "complexType");

					AddArguments(complexType, methods);

					element.Add(complexType);
				}
			}
		}
		//===========================================================================================
		private void AddParameterAttributes(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
		{
			foreach (var parameter in from parameter in parameters
											  let typeName = _MagickNET.GetXsdAttributeType(parameter)
											  where typeName != null
											  orderby parameter.Name
											  select new
											  {
												  Name = parameter.Name,
												  TypeName = typeName,
												  IsRequired = requiredParameters.Contains(parameter.Name)
											  })
			{
				XElement attribute = new XElement(_Namespace + "attribute",
												new XAttribute("name", parameter.Name));

				if (parameter.IsRequired)
					attribute.Add(new XAttribute("use", "required"));

				attribute.Add(new XAttribute("type", parameter.TypeName));

				complexType.Add(attribute);
			}
		}
		//===========================================================================================
		private void AddParameterElements(XElement complexType, ParameterInfo[] parameters, string[] requiredParameters)
		{
			XElement sequence = new XElement(_Namespace + "sequence");

			foreach (var parameter in from parameter in parameters
											  let typeName = _MagickNET.GetXsdElementType(parameter)
											  where typeName != null
											  orderby parameter.Name
											  select new
											  {
												  Name = parameter.Name,
												  TypeName = typeName,
												  IsRequired = requiredParameters.Contains(parameter.Name)
											  })
			{
				XElement element = new XElement(_Namespace + "element",
											new XAttribute("name", parameter.Name));

				if (!parameter.IsRequired)
					element.Add(new XAttribute("minOccurs", "0"));

				element.Add(new XAttribute("type", parameter.TypeName));

				sequence.Add(element);
			}

			if (sequence.HasElements)
				complexType.Add(sequence);
		}
		//===========================================================================================
		private void AddClassAttributes(XElement complexType, IEnumerable<PropertyInfo> properties)
		{
			foreach (var property in from property in properties
											 let typeName = _MagickNET.GetXsdAttributeType(property)
											 where typeName != null
											 let name = _MagickNET.GetXsdName(property)
											 orderby name
											 select new
											 {
												 Name = name,
												 TypeName = typeName
											 })
			{
				complexType.Add(new XElement(_Namespace + "attribute",
									new XAttribute("name", property.Name),
									new XAttribute("type", property.TypeName)));
			}
		}
		//===========================================================================================
		private void AddClassElements(XElement complexType, IEnumerable<PropertyInfo> properties, IEnumerable<MethodInfo> methods)
		{
			XElement sequence = new XElement(_Namespace + "sequence");

			foreach (var property in from property in properties
											 let typeName = _MagickNET.GetXsdElementType(property)
											 where typeName != null
											 let name = _MagickNET.GetXsdName(property)
											 orderby name
											 select new
											 {
												 Name = name,
												 TypeName = typeName
											 })
			{
				XElement element = new XElement(_Namespace + "element",
											new XAttribute("name", property.Name),
											new XAttribute("minOccurs", "0"));

				element.Add(new XAttribute("type", property.TypeName));

				sequence.Add(element);
			}

			if (methods.Count() > 0)
			{
				foreach (MethodBase method in methods)
				{
					XElement element = new XElement(_Namespace + "element",
												new XAttribute("name", _MagickNET.GetXsdName(methods.First())),
												new XAttribute("minOccurs", "0"),
												new XAttribute("maxOccurs", "unbounded"));
					AddMethods(element, new MethodBase[] { method });
					sequence.Add(element);
				}
			}

			if (sequence.HasElements)
				complexType.Add(sequence);
		}
		//===========================================================================================
		private object CreateElement(IEnumerable<MethodBase> methods)
		{
			XElement element = new XElement(_Namespace + "element",
										new XAttribute("name", _MagickNET.GetXsdName(methods.First())));

			AddMethods(element, methods);
			return element;
		}
		//===========================================================================================
		private XElement CreateElement(PropertyInfo property)
		{
			string name = _MagickNET.GetXsdName(property);


			string attributeTypeName = _MagickNET.GetXsdAttributeType(property);

			if (attributeTypeName != null)
			{
				XElement complexType = new XElement(_Namespace + "complexType");
				complexType.Add(new XElement(_Namespace + "attribute",
										new XAttribute("name", "value"),
										new XAttribute("use", "required"),
										new XAttribute("type", attributeTypeName)));

				return new XElement(_Namespace + "element",
							new XAttribute("name", name),
							complexType);
			}
			else
			{
				return new XElement(_Namespace + "element",
					new XAttribute("name", name),
					new XAttribute("type", _MagickNET.GetXsdElementType(property)));
			}
		}
		//===========================================================================================
		private XElement CreateEnumElement(Type enumType)
		{
			XElement restriction = new XElement(_Namespace + "restriction", new XAttribute("base", "xs:NMTOKEN"));
			AddEnumValues(enumType, restriction);

			return CreateVarElement(enumType.Name, restriction);
		}
		//===========================================================================================
		private XElement CreateVarElement(string name, XElement restriction)
		{
			return new XElement(_Namespace + "simpleType",
						new XAttribute("name", name),
						new XElement(_Namespace + "union",
							new XElement(_Namespace + "simpleType",
								restriction),
							new XElement(_Namespace + "simpleType",
								new XElement(_Namespace + "restriction",
									new XAttribute("base", "var")))));
		}
		//===========================================================================================
		private static void Generate(QuantumDepth depth)
		{
			XsdGenerator generator = new XsdGenerator(depth);
			generator.WriteDocument();
		}
		//===========================================================================================
		private bool IsTypedElement(ParameterInfo[] parameters)
		{
			if (parameters.Length > 1)
				return false;

			ParameterInfo parameter = parameters[0];

			if (parameter.ParameterType.IsGenericType && parameter.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				return true;

			if (parameter.Name == "profile")
				return true;

			return false;
		}
		//===========================================================================================
		private void RemoveComments()
		{
			_Document = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Xsd\MagickScript.xsd");
			_Document.DescendantNodes().OfType<XComment>().Remove();
		}
		//===========================================================================================
		private void RemoveUnusedSimpleTypes()
		{
			XElement[] simpleTypes = (from element in _Document.Root.XPathSelectElements("./xs:simpleType", _Namespaces)
											  select element).ToArray();

			foreach (XElement simpleType in simpleTypes)
			{
				string name = (string)simpleType.Attribute("name");

				if (name == "var")
					continue;

				if (!_Document.XPathSelectElements("//xs:attribute[@type='" + name + "']", _Namespaces).Any())
					simpleType.Remove();
			}
		}
		//===========================================================================================
		private void ReplaceAnnotations()
		{
			XElement[] annotations = (from element in _Document.XPathSelectElements("//xs:annotation", _Namespaces)
											  select element).ToArray();

			foreach (XElement annotation in annotations)
			{
				string annotationID = annotation.Attribute("id").Value;
				switch (annotationID)
				{
					case "color":
						ReplaceColor(annotation);
						break;
					case "collection-actions":
						ReplaceCollectionActions(annotation);
						break;
					case "collection-results":
						ReplaceCollectionResults(annotation);
						break;
					case "colorProfile":
						ReplaceColorProfile(annotation);
						break;
					case "coordinate":
					case "imageProfile":
					case "pathArc":
					case "pathCurveto":
					case "pathQuadraticCurveto":
					case "sparseColorArg":
						ReplaceWithType(annotation, annotationID);
						break;
					case "defines":
						ReplaceDefines(annotation);
						break;
					case "drawables":
						ReplaceDrawables(annotation);
						break;
					case "enums":
						ReplaceEnums(annotation);
						break;
					case "iDefines":
						ReplaceIDefines(annotation);
						break;
					case "image-actions":
						ReplaceImageActions(annotation);
						break;
					case "iReadDefines":
						ReplaceIReadDefines(annotation);
						break;
					case "magickReadSettings":
					case "montageSettings":
					case "pixelStorageSettings":
					case "quantizeSettings":
						ReplaceWithClass(annotation, annotationID);
						break;
					case "paths":
						ReplacePaths(annotation);
						break;
					case "quantum":
						ReplaceQuantum(annotation);
						break;
					default:
						throw new NotImplementedException(annotationID);
				}
			}
		}
		//===========================================================================================
		private void ReplaceCollectionActions(XElement annotation)
		{
			AddMagickImageCollectionMethods(annotation);

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceCollectionResults(XElement annotation)
		{
			AddMagickImageCollectionResultMethods(annotation);

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceColorProfile(XElement annotation)
		{
			XElement restriction = new XElement(_Namespace + "restriction",
											new XAttribute("base", "xs:NMTOKEN"));
			foreach (string name in _MagickNET.GetColorProfileNames())
			{
				restriction.Add(new XElement(_Namespace + "enumeration",
										new XAttribute("value", name)));
			}

			annotation.ReplaceWith(CreateVarElement("ColorProfile", restriction));
		}
		//===========================================================================================
		private void ReplaceDefines(XElement annotation)
		{
			List<XElement> types = new List<XElement>();

			foreach (Type interfaceType in _MagickNET.GetInterfaceTypes("IDefines"))
			{
				XElement complexType = new XElement(_Namespace + "complexType",
													new XAttribute("name", _MagickNET.GetXsdName(interfaceType)));
				AddClass(complexType, interfaceType.Name);
				types.Add(complexType);
			}

			annotation.ReplaceWith(types.ToArray());
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
		private void ReplaceDrawables(XElement annotation)
		{
			foreach (ConstructorInfo[] constructors in _MagickNET.GetDrawables())
			{
				annotation.AddBeforeSelf(CreateElement(constructors));
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

			restriction.Add(new XElement(_Namespace + "pattern",
						new XAttribute("value", "Transparent")));

			annotation.ReplaceWith(CreateVarElement("color", restriction));
		}
		//===========================================================================================
		private void ReplaceImageActions(XElement annotation)
		{
			AddMagickImageProperties(annotation);
			AddMagickImageMethods(annotation);

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceIDefines(XElement annotation)
		{
			annotation.ReplaceWith(
				from type in _MagickNET.GetInterfaceTypes("IDefines")
				let name = _MagickNET.GetXsdName(type)
				select new XElement(_Namespace + "element",
					new XAttribute("name", name),
					new XAttribute("type", name)));
		}
		//===========================================================================================
		private void ReplaceIReadDefines(XElement annotation)
		{
			annotation.ReplaceWith(
				from type in _MagickNET.GetInterfaceTypes("IReadDefines")
				let name = _MagickNET.GetXsdName(type)
				select new XElement(_Namespace + "element",
					new XAttribute("name", name),
					new XAttribute("type", name)));
		}
		//===========================================================================================
		private void ReplacePaths(XElement annotation)
		{
			foreach (ConstructorInfo[] constructors in _MagickNET.GetPaths())
			{
				annotation.AddBeforeSelf(CreateElement(constructors));
			}

			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceQuantum(XElement annotation)
		{
			string max;
			string baseType;
			switch (_Depth)
			{
				case QuantumDepth.Q8:
					max = "255";
					baseType = "xs:unsignedByte";
					break;
				case QuantumDepth.Q16:
					max = "65535";
					baseType = "xs:unsignedShort";
					break;
				case QuantumDepth.Q16HDRI:
					max = "65535";
					baseType = "xs:float";
					break;
				default:
					throw new NotImplementedException();
			}

			annotation.ReplaceWith(
				CreateVarElement("quantum",
					new XElement(_Namespace + "restriction",
						new XAttribute("base", baseType),
						new XElement(_Namespace + "minInclusive",
							new XAttribute("value", "0")),
						new XElement(_Namespace + "maxInclusive",
							new XAttribute("value", max)))));
		}
		//===========================================================================================
		private void ReplaceWithClass(XElement annotation, string typeName)
		{
			AddClass(annotation.Parent, typeName);
			annotation.Remove();
		}
		//===========================================================================================
		private void ReplaceWithType(XElement annotation, string typeName)
		{
			AddArguments(annotation.Parent, _MagickNET.GetConstructors(typeName));

			annotation.Remove();
		}
		//===========================================================================================
		private void Write()
		{
			string folder = MagickNET.GetFolderName(_Depth);
			string outputFile = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\Resources\" + folder + @"\MagickScript.xsd");
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
			RemoveComments();
			ReplaceAnnotations();
			RemoveUnusedSimpleTypes();

			Write();
		}
		//===========================================================================================
		public static void Generate()
		{
			Generate(QuantumDepth.Q8);
			Generate(QuantumDepth.Q16);
			Generate(QuantumDepth.Q16HDRI);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
