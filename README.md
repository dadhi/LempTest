# Proof-of-concept compile-time Dependency Injection container using LeMP code-generation from the ECSharp

## Why at all

Compile-time dependency injection here is 
the generation of object-graph "creation code" given the service interface-implementation mappings provided.
In a simplest form you may consider "creation code" as a method returning `new A(new B(new C(), new C1(new D()), ...)`.

The idea is already implemented in my another project [DryIoc](https://github.com/dadhi/dryioc) using the T4 templates
but has a number of [problems](https://github.com/dadhi/DryIoc/issues/212).

So in this repository I want to test couple of ideas **without much accent on DI itself**. 
Nevertheless, the DI should be a functional solution to make the test actually usable and verify the different aspects of the problem.
OK, the DryIoc supports both compile-time and runtime registrations, because it not always possible to know everything at compile-time.
Therefore this DI prototype should support this too. 

The typical compile-time service registration will look like this: 
```cs
di.Register<X>(resolver => new X(new A(), resolver.Resolve<B>()));
```

`resolver.Resolver<B>()` here is bridge to other registration which can be either compile-time or runtime-one.
You can image that `X` and `A` are your application services and `B` is the some context or configuration provided by 
framework at runtime. 

## Problems to solve

1. I want the ability to use part of DI to help generate another part of DI at compile-time. It means that I need somehow reference the code (compiled) from compile-time template to generate remaining part of code. But I don't want to split the first part to another library, compile it, then load for the rest of code-generation. So inevitably I am ending-up in chicken-egg problem. For T4 the hack would be to compile the DI project first with the compile-time generation commented out, the uncomment it and load the produced dll to compile it to a new dll with generated code :/

2. I want to publish the DI as a code NuGet package with the template that's supposed to be modified by user - to put the registrations into it. I want the package to be easy (as automated as possible) consumed by modern .NET Core v3.1 applications, like ASP .NET Core MVC/WebAPI. And this combination currently is the real mess. Here is the [one](https://github.com/NuGet/Home/issues/4837) of the many issues. You need to really [hacking around](https://medium.com/@attilah/source-code-only-nuget-packages-8f34a8fb4738) your way to achieve this. 

3. Given that I already invested the time and created the T4 implementation in DryIoc, 
it seems that I need to push it further, probably using the [mono implementation](https://github.com/mono/t4). 
But T4 never felt for me like a good fit to C#, because it manipulates and generates the result like a text and loses the C# semantics.
The template looks like teared apart C# pieces. The errors are cryptic. Assembly loading and references is a quiz. Etc. etc.

So here goes...

## How

### LeMP and ECSharp

The project site http://ecsharp.net/

LeMP compile-time code-generation similar to T4 is supported starting from the [v2.8.1](https://github.com/qwertie/ecsharp/releases/tag/v2.8.1)

LeMP solves the 3rd problem because its compile-time generation capabilities keep the code looking like C# with almost all valid C# syntax.
And ta-ba-dam, tab=-ba-dam, ... it solves the 1st problem too via `compileAndRuntime {}` which makes code available for the compile-time tools and keeps the code in the result runtime binary.

Other goodies are Extended CSharp features, macros and sugar.

### The build

The solution is supposed to be build and tested in VSCode.

- Download LeMP zip, unzip it to some folder and add the folder path to Environment PATH variable. Open any terminal and check that "lemp.exe --help" returns something meaningful.
- Add the folder `.nupkg` as a local MuGet package source: `dotnet nuget add source full\path\to\.nupkg --name Local`
- Go to "LempDotnetTool" project folder and compile it via `dotnet build`. This will produce the LempDotnetTool package in `.nupkg` wrapping the "LeMP.exe". It is done to simplify installing the LeMP together with the CompileTimeDI package.
- Go back to solution folder and built it with `dotnet build`
- For `.ecs` and `.ecs.include` files you may turn On the CSharp syntax highlighting via "Change Language Mode" command (`Ctrl+K,M`) 


### CompileTimeDI project

CompileTimeDI is the .NET Standard 2.0 project with the DI implementation contained in a single `CompileTimeDI.ecs`. 
The project also contains the `ServiceRegistrations.ecs.include` file which is supposed to be edited by DI user.
The latter is included into former, then everything is processed by LeMP macro processor at compile-time and the output 
is stored in `CompileTimeDI.Generated.cs` file. Invoke the `dotnet build` to run the processing.

When build in the Release configuration `dotnet build -c Release`, 
the project is packaged into the content-only NuGet package ([yes, you can do this](https://medium.com/@attilah/source-code-only-nuget-packages-8f34a8fb4738))
and can be found in the ".nupkg" folder.

### AspNetCoreSample project

AspNetCoreSample is the ASP .NET Core v3.1 example application consuming the CompileTimeDI package.
So it installs the "CompileTimeDI" package from the Local feed (together with LempDotnetTool) and 
specifies some test service registrations from the AnotherLib project. Experiment here.
