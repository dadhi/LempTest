﻿// <auto-generated />
//
// The code is auto-generated from the *.ecs file
// If you change something here it will be lost on the next generate
//
//---------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System;
namespace CompileTimeDI
{
    public interface IResolver
    {
        object Resolve(Type serviceType);
        TService Resolve<TService>();
    }

    public partial class DI : IResolver
    {
        public ConcurrentDictionary<Type, Expression<Func<IResolver, object>>> Registrations = new ConcurrentDictionary<Type, Expression<Func<IResolver, object>>>();
        internal ConcurrentDictionary<Type, Func<IResolver, object>> DelegateCache = new ConcurrentDictionary<Type, Func<IResolver, object>>();
        public void Register<T>(Expression<Func<IResolver, object>> factory)
        {
            Registrations.TryAdd(typeof(T), factory);
        }
    
        public class Exception : InvalidOperationException
        {
            public Exception(string m) : base(m) { }
        }
    
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated);
    
        public object Resolve(Type serviceType)
        {
            var isGenerated = false;
            object service = null;
            TryResolveGenerated(serviceType, ref service, ref isGenerated);
            if (isGenerated)
                return service;
        
            if (DelegateCache.TryGetValue(serviceType, out var facDel))
                return facDel(this);
        
            if (Registrations.TryGetValue(serviceType, out var facExpr))
                return DelegateCache.GetOrAdd(serviceType, _ => facExpr.Compile()).Invoke(this);
        
            throw new Exception($"Unable to resolve `${serviceType}`");
        }
    
        public TService Resolve<TService>() => (TService) Resolve(typeof(TService));
    }
}

namespace CompileTimeDI
{
    using AnotherLib.Experimental;
    using AnotherLib;
    partial class DI
    {
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated)
        {
            if (serviceType == typeof(X)) {
                service = Get_X_0(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(A)) {
                service = Get_A_1(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(B)) {
                service = Get_B_2(this);
                isGenerated = true;
                return;
            }
        }
    
        object Get_X_0(IResolver r) => new X(new A(), new B(), r.Resolve<Y>());
        object Get_A_1(IResolver r) => new A();
        object Get_B_2(IResolver r) => new B();
    }
}