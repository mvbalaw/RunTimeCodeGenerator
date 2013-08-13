//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.IO;

using NUnit.Framework;

using RunTimeCodeGenerator.ClassGeneration;

namespace RunTimeCodeGenerator.Tests.ClassGeneration
{
	public class ClassFileGeneratorTests
	{
		internal class FileComparer
		{
			public bool Compare(string file1, string file2)
			{
				using (var fileStream1 = new StreamReader(File.OpenRead(file1)))
				{
					using (var fileStream2 = new StreamReader(File.OpenRead(file2)))
					{
						string line1;
						string line2;
						do
						{
							line1 = fileStream1.ReadLine();
							line2 = fileStream2.ReadLine();
						} while ((line1 != null || line2 != null) && line1 == line2);
						return line1 == null && line2 == null;
					}
				}
			}
		}

		[TestFixture]
		public class When_asked_to_generate_a_class_file
		{
			private ClassAttributes _classAttributes;
			private IClassGenerator _classGenerator;
			private Method _method;

			[SetUp]
			public void SetUp()
			{
				_classAttributes = new ClassAttributes("TestClass");
				_classAttributes.AddUsingNamespaces("System");
				_classAttributes.Namespace = "TestNamespace";

				_method = new Method
				          {
					          Name = "TestMethod",
					          ReturnType = "string",
					          AccessLevel = AccessLevel.Public
				          };
				_method.Body.Add("string variable = \"Test\";");
				_method.Body.Add("Console.WriteLine(\"Hello\");");
				_method.Body.Add("return variable;");
				_classAttributes.Methods.Add(_method);

				_classGenerator = new ClassFileGenerator();
			}

			[TearDown]
			public void TearDown()
			{
				File.Delete(_classAttributes.FullName);
			}

			[Test]
			public void Should_create_a_class_file()
			{
				_classGenerator.Create(_classAttributes);

				var fileName = _classAttributes.FullName;
				Assert.IsTrue(File.Exists(fileName));
				Assert.IsTrue(new FileComparer().Compare(fileName, TestData.ClassWithMethods));
			}

			[Test]
			public void Should_create_a_class_file_with_method_parameters()
			{
				_method.Parameters.Add(new Parameter("string", "param1"));
				_method.Parameters.Add(new Parameter("int", "param2"));

				_classGenerator.Create(_classAttributes);

				var fileName = _classAttributes.FullName;
				Assert.IsTrue(File.Exists(fileName));
				Assert.IsTrue(new FileComparer().Compare(fileName, TestData.ClassWithMethodsWithParameters));
			}
		}

		[TestFixture]
		public class When_asked_to_generate_a_class_file_with_only_properties
		{
			private ClassAttributes _classAttributes;
			private IClassGenerator _classGenerator;

			[SetUp]
			public void SetUp()
			{
				_classAttributes = new ClassAttributes("TestClass");
				_classAttributes.AddUsingNamespaces("System");
				_classAttributes.Properties.Add(new Property("Double", "TestItem1"));
				_classAttributes.Properties.Add(new Property("string", "TestItem2"));

				_classGenerator = new ClassFileGenerator();
			}

			[TearDown]
			public void TearDown()
			{
				File.Delete(_classAttributes.FullName);
			}

			[Test]
			public void Should_create_a_class_file()
			{
				_classGenerator.Create(_classAttributes);

				var fileName = _classAttributes.FullName;
				Assert.IsTrue(File.Exists(fileName));
				Assert.IsTrue(new FileComparer().Compare(fileName, TestData.ClassWithProperties));
			}
		}
	}
}