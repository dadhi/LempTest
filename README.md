# Proof-of-concept compile-time Dependency Injection container using LeMP code-generation from the ECSharp

## LeMP and ECSharp

The project site http://ecsharp.net/

LeMP compile-time code-generation similar to T4 is supported starting from the [v2.8.1](https://github.com/qwertie/ecsharp/releases/tag/v2.8.1)


## How to build

The solution is supposed to be build and tested in VSCode.

- Download LeMP zip, unzip it to some folder and add the folder path to Environment PATH variable. Open any terminal and check that "lemp.exe --help" returns something meaningful.
- Add the folder `.nupkg` as a local MuGet package source: `dotnet nuget add source full\path\to\.nupkg --name Local`
- Go to "LempDotnetTool" project folder and compile it via `dotnet build`. This will produce the LempDotnetTool package in `.nupkg` wrapping the "LeMP.exe". It is done to simplify installing the LeMP together with the CompileTimeDI package.
- Go back to solution folder and built it with `dotnet build`

## CompileTimeDI

CompileTimeDI is the .NET Standard 2.0 project with the DI implementation contained in a single `CompileTimeDI.ecs`. 
The project also contains the `ServiceRegistrations.ecs.include` file which is supposed to be edited by DI user.
The latter is included into former, then everything is processed by LeMP macro processor at compile-time and the output 
is stored in `CompileTimeDI.Generated.cs` file. Invoke the `dotnet build` to run the processing.

When build in the Release configuration `dotnet build -c Release`, 
the project is packaged into the content-only NuGet package ([yes, you can do this](https://medium.com/@attilah/source-code-only-nuget-packages-8f34a8fb4738))
and can be found in the ".nupkg" folder.

## AspNetCoreSample

AspNetCoreSample is the .NET Core 3. example application consuming the CompileTimeDI package.
So it installs the "CompileTimeDI" package from the Local feed (together with LempDotnetTool) and 
specifies some test service registrations from the AnotherLib project. 

## References

- Content-based only package in csproj not in nuspec: https://medium.com/@attilah/source-code-only-nuget-packages-8f34a8fb4738
- Publishing T4 files as part of NuGet package http://diegogiacomelli.com.br/deploying-a-t4-template-with-dotnet-pack/
- LeMP C# code-generation similar to T4 https://github.com/qwertie/ecsharp/issues/112