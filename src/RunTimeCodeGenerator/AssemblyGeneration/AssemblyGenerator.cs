//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.CodeDom.Compiler;
using System.Collections.Generic;

using Microsoft.CSharp;

using RunTimeCodeGenerator.Logging;

namespace RunTimeCodeGenerator.AssemblyGeneration
{
	public interface IAssemblyGenerator
	{
		bool Compile(string[] classNames, AssemblyAttributes assemblyAttributes);
	}

	public class AssemblyGenerator : IAssemblyGenerator
	{
		public bool Compile(string[] classNames, AssemblyAttributes assemblyAttributes)
		{
			var codeProvider = new CSharpCodeProvider(new Dictionary<string, string>
			                                          {
				                                          { "CompilerVersion", "v4.0" }
			                                          });

			var parameters = new CompilerParameters
			                 {
				                 OutputAssembly = assemblyAttributes.FullName
			                 };

			parameters.ReferencedAssemblies.AddRange(assemblyAttributes.References.ToArray());
			parameters.EmbeddedResources.AddRange(assemblyAttributes.Resources.ToArray());

			var compilerResults = codeProvider.CompileAssemblyFromFile(parameters, classNames);

			GenerateErrorReport(compilerResults.Errors);

			return compilerResults.Errors.Count == 0;
		}

// ReSharper disable once SuggestBaseTypeForParameter
		private static void GenerateErrorReport(CompilerErrorCollection errorsCollection)
		{
			if (errorsCollection.Count == 0)
			{
				return;
			}

			foreach (CompilerError error in errorsCollection)
			{
				Log.For(typeof(AssemblyGenerator)).LogError("{0}: {1}", error.FileName, error.ErrorText);
			}
		}
	}
}