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

    public partial class CompileTimeDI : IResolver
    {
        public ConcurrentDictionary<Type, Expression<Func<IResolver, object>>> Registrations;
        public ConcurrentDictionary<Type, Func<IResolver, object>> DelegateCache;
    
        public CompileTimeDI()
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
    partial class CompileTimeDI
    {
        partial void TryResolveGenerated(Type serviceType, ref object service, ref bool isGenerated)
        { }
    
    }
}