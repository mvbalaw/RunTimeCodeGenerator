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

            CompilerResults compilerResults = codeProvider.CompileAssemblyFromFile(parameters, classNames);

            GenerateErrorReport(compilerResults.Errors);

            return compilerResults.Errors.Count == 0;
        }

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