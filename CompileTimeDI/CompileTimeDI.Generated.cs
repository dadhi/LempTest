﻿// <auto-generated />
//
// The code is auto-generated from the *.ecs file
// All changes done to this .cs file will be lost on the next generate
//
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace CompileTimeDI
{
    public interface IResolver
    {
        object Resolve(Type serviceType);
    }

    public partial class CompileTimeDI : IResolver
    {
        public Dictionary<Type, Expression<Func<IResolver, object>>> Factories;
    
        public CompileTimeDI()
        {
            Factories = new Dictionary<Type, Expression<Func<IResolver, object>>>();
        }
    
        public void Register<T>(Expression<Func<IResolver, object>> factory)
        {
            Factories.Add(typeof(T), factory);
        }
    
        public class Exception : InvalidOperationException
        {
            public Exception(string m) : base(m) { }
        }
    
        public object Resolve(Type serviceType)
        {
            var isGenerated = false;
            object service = null;
            TryResolveGenerated(serviceType, ref service, ref isGenerated);
            if (isGenerated)
                return service;
        
            if (Factories.TryGetValue(serviceType, out var facExpr))
            {
                // todo: @feature insert caching of compiled delegate here
                var fDelegate = facExpr.Compile();
                return fDelegate(this);
            }
        
            throw new Exception($"Unable to resolve `${serviceType}`");
        }
    
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated);
    }
}

	// todo: @note extensions method are not supported in compile time
	// public static class Resolver 
	// {
	//     public static T Resolve<T>(this IResolver r) => (T)r.Resolve(typeof(T));
	// }
namespace CompileTimeDI
{
    using AnotherLib;
    using AnotherLib.Experimental;
    partial class CompileTimeDI
    {
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated)
        {
            if (serviceType == typeof(A)) {
                service = Get_A_0(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(B)) {
                service = Get_B_1(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(X)) {
                service = Get_X_2(this);
                isGenerated = true;
                return;
            }
        }
    
        object Get_A_0(IResolver r) => new A();
        object Get_B_1(IResolver r) => new B();
        object Get_X_2(IResolver r) => new X(new A(), new B());
    }
}