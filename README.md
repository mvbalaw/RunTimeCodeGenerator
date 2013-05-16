RunTimeCodeGenerator ReadMe
===

## DESCRIPTION

RunTimeCodeGenerator is a small library to compile and generate an assembly using reflection.

This project was skimmed from the subversion repository we originally
created on Google code: https://code.google.com/p/runtimecodegenerator/
which was extracted from our ExcelHelper project.

## HOW TO BUILD

The build script requires Ruby with rake installed.

1. Run `InstallGems.bat` to get the ruby dependencies (only needs to be run once per computer)
1. open a command prompt to the root folder and type `rake` to execute rakefile.rb

If you do not have ruby:

1. You need to create a src\CommonAssemblyInfo.cs file. Go.bat will copy src\ 
  * go.bat will copy src\CommonAssemblyInfo.cs.default to src\CommonAssemblyInfo.cs
1. open src\FubuMVC.sln with Visual Studio and Build the solution

## License		

[MIT License][mitlicense]

This project is part of [MVBA Law Commons][mvbalawcommons].

[mvbalawcommons]: http://mvbalaw.github.io
[mitlicense]: http://www.opensource.org/licenses/mit-license.php
