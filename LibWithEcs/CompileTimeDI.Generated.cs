﻿// <auto-generated />
//
// The code is auto-generated from the *.ecs file
// All changes done to .cs file will be lost on the next generate
//
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LibWithEcs
{
    public class A { }	// todo: @test just for test here, move to another assembly 
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
namespace LibWithEcs
{
    partial class CompileTimeDI
    {
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated)
        {
        	// precompute(
        	// );
         }
    
        object Get_A(IResolver r) => _numresult(new A());
    }

    public static class Resolver
    {
        public static T Resolve<T>(this IResolver r) => (T) r.Resolve(typeof(T));
    }
}