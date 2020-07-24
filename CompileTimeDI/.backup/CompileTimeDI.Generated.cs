﻿using System.Collections.Generic;
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
        public ConcurrentDictionary<Type, Expression<Func<IResolver, object>>> Registrations;
        public ConcurrentDictionary<Type, Func<IResolver, object>> DelegateCache;
    
        public DI()
        {
            Registrations = new ConcurrentDictionary<Type, Expression<Func<IResolver, object>>>();
            DelegateCache = new ConcurrentDictionary<Type, Func<IResolver, object>>();
        }
    
        public void Register<T>(Expression<Func<IResolver, object>> factory)
        {
            Registrations.TryAdd(typeof(T), factory);
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
        
            if (DelegateCache.TryGetValue(serviceType, out var facDel))
                return facDel(this);
        
            if (Registrations.TryGetValue(serviceType, out var facExpr))
            {
                var fDelegate = DelegateCache.GetOrAdd(serviceType, _ => facExpr.Compile());
                return fDelegate(this);
            }
        
            throw new Exception($"Unable to resolve `${serviceType}`");
        }
    
        public TService Resolve<TService>() => (TService) Resolve(typeof(TService));
    
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated);
    }
}
namespace CompileTimeDI
{
    using AnotherLib;
    using AnotherLib.Experimental;
    partial class DI
    {
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated)
        {
            if (serviceType == typeof(B)) {
                service = Get_B_0(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(X)) {
                service = Get_X_1(this);
                isGenerated = true;
                return;
            }
            if (serviceType == typeof(A)) {
                service = Get_A_2(this);
                isGenerated = true;
                return;
            }
        }
    
        object Get_B_0(IResolver r) => new B();
        object Get_X_1(IResolver r) => new X(new A(), new B(), r.Resolve<Y>());
        object Get_A_2(IResolver r) => new A();
    }
}