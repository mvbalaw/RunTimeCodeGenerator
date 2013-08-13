//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Linq;

namespace RunTimeCodeGenerator.ClassGeneration
{
	public class ClassFileGenerator : IClassGenerator
	{
		public void Create(ClassAttributes classAttributes)
		{
			using (IClassFileWriter classFileWriter = new ClassFileWriter(classAttributes.FullName))
			{
				foreach (var usingNamespace in classAttributes.UsingNamespaces)
				{
					AddUsing(classFileWriter, usingNamespace);
				}

				classFileWriter.Write();

				StartNamespace(classFileWriter, classAttributes.Namespace);
				{
					StartClass(classFileWriter, classAttributes.Name);
					{
						foreach (var property in classAttributes.Properties)
						{
							AddProperty(classFileWriter, property);
						}

						foreach (var method in classAttributes.Methods)
						{
							StartMethod(classFileWriter, method);
							{
								AddMethod(classFileWriter, method);
							}
							classFileWriter.CloseBlock();
						}
					}
					classFileWriter.CloseBlock();
				}
				CloseNamespace(classFileWriter, classAttributes.Namespace);
			}
		}

		private static void AddMethod(IClassFileWriter classFileWriter, Method method)
		{
			foreach (var line in method.Body)
			{
				var formattedLine = line;
				var endBlock = line.StartsWith("}");
				var startBlock = line.EndsWith("{");

				if (endBlock)
				{
					if (formattedLine.Length > 1)
					{
						formattedLine = formattedLine.Substring(1).Trim();
					}
					classFileWriter.CloseBlock();
				}
				if (startBlock)
				{
					if (formattedLine.Length > 1)
					{
						formattedLine = formattedLine.Substring(0, formattedLine.Length - 1);
					}
				}

				if (line.Length > 1)
				{
					classFileWriter.Write(formattedLine);
				}
				if (startBlock && formattedLine != "{")
				{
					classFileWriter.StartBlock();
				}
			}
		}

		private static void AddProperty(IClassFileWriter classFileWriter, Property property)
		{
			classFileWriter.Write(String.Format("{0} {1} {2} {{ get; set; }}", AccessLevel.Public.Value, property.Type, property.Name));
		}

		private static void AddUsing(IClassFileWriter classFileWriter, string usingNamespace)
		{
			classFileWriter.Write(String.Format("using {0};", usingNamespace));
		}

		private static void CloseNamespace(IClassFileWriter classFileWriter, string classNamespace)
		{
			if (String.IsNullOrEmpty(classNamespace))
			{
				return;
			}
			classFileWriter.CloseBlock();
		}

		private static void StartClass(IClassFileWriter classFileWriter, string className)
		{
			classFileWriter.Write(String.Format("{0} class {1}", AccessLevel.Public.Value, className));
			classFileWriter.StartBlock();
		}

		private static void StartMethod(IClassFileWriter classFileWriter, Method method)
		{
			var parameters = String.Join(",", method.Parameters.Select(x => x.ToString()).ToArray());
			classFileWriter.Write(String.Format("{0} {1} {2}({3})", method.AccessLevel.Value, method.ReturnType, method.Name, parameters));

			classFileWriter.StartBlock();
		}

		private static void StartNamespace(IClassFileWriter classFileWriter, string classNamespace)
		{
			if (String.IsNullOrEmpty(classNamespace))
			{
				return;
			}
			classFileWriter.Write(String.Format("namespace {0}", classNamespace));
			classFileWriter.StartBlock();
		}
	}
}