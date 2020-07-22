# Proof-of-concept compile-time Dependency Injection container using LeMP code-generation from the ECSharp

## LeMP and ECSharp

The project site http://ecsharp.net/

LeMP compile-time code-generation similar to T4 is supported starting from the [v2.8.1](https://github.com/qwertie/ecsharp/releases/tag/v2.8.1)


## How to build

The solution is supposed to be build and tested in VSCode.

- Download LeMP zip, unzip it to some folder and add the folder path to Environment PATH variable. Open any terminal and check that "lemp.exe --help" returns something meaningful.
- Go to "LempDotnetTool" project folder and compile it via `dotnet build` - this will produce the NuGet package with dotnet CLI tool in solution ".nupkg" folder
- Add the the folder with package to the NuGet sources: `dotnet nuget add source "full\path\.nupkg" --name Local`
- Go back to solution folder and built it `dotnet build`

## CompileTimeDI

File `CompileTimeDI.ecs` contains the DI library prototype supporting both compile-time and runtime registration. 

The compile-time registrations supposed to be done by User in `ServiceRegistrations.ecs.include` file.

The generation happens when you `dotnet build` the CompileTimeDI project or the whole solution, and result code can be found in `CompileTimeDI.Generated.cs` file. Check the compilation errors to find if something goes off in generation. Check the generated file to find how your registrations are come to life.

CompileTimeDI is packaged to the content-only NuGet package (it is hard).

## AspNetCoreSample

AspNetCoreSample is an example of consuming application of CompileTimeDI package.

## References

- Content-based only package in csproj not in nuspec https://medium.com/@attilah/source-code-only-nuget-packages-8f34a8fb4738
- Publishing T4 files as part of NuGet package http://diegogiacomelli.com.br/deploying-a-t4-template-with-dotnet-pack/
- LeMP C# code-generation similar to T4 https://github.com/qwertie/ecsharp/issues/112